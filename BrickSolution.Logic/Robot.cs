﻿using BrickSolution.Logic.Enumerations;
using MonoBrickFirmware.Display;
using MonoBrickFirmware.Movement;
using MonoBrickFirmware.Sensors;
using MonoBrickFirmware.UserInput;
using System;
using System.Threading;

namespace BrickSolution.Logic
{
    /// <summary>
    /// the class consists of static members that are responsible
    /// for controlling a EV3 custom build robot
    /// </summary>
    public class Robot
    {
        #region Properties

        #region States

        /// <summary>
        /// Is used to indicate if the robot was initialized
        /// correctly
        /// </summary>
        public static bool IsInitialized { get; private set; }

        /// <summary>
        /// describes the position of the grappler
        /// </summary>
        public static Enumerations.GrapplerPosition GrapplerPosition { get; private set; }

        /// <summary>
        /// describes the state of the grappler
        /// </summary>
        public static Enumerations.GrapplerState GrapplerState { get; private set; }

        /// <summary>
        /// describes if the robot is carrying food or searching currently
        /// </summary>
        public static Enumerations.FoodState FoodState { get; private set; }

        /// <summary>
        /// describes why the robot stopped
        /// </summary>
        public static Enumerations.StopReason LastStopReason { get; private set; }

        #endregion

        #region Motors

        /// <summary>
        /// holds the <see cref="Motor"/> instance to a large
        /// EV3 motor that is responsible for the left track
        /// </summary>
        private static Motor LeftTrack { get; set; }
        /// <summary>
        /// holds the <see cref="Motor"/> instance to a large
        /// EV3 motor that is responsible for the right track
        /// </summary>
        private static Motor RightTrack { get; set; }

        /// <summary>
        /// holds the <see cref="Motor"/> instance to a 
        /// EV3 motor that is responsible for the grappler
        /// </summary>
        private static Motor GrapplerMotor { get; set; }


        #endregion

        #region Sensors

        /// <summary>
        /// holds the <see cref="EV3ColorSensor"/> instance
        /// for the EV3ColorSensor
        /// </summary>
        private static EV3ColorSensor ColorSensor { get; set; }

        /// <summary>
        /// holds the <see cref="EV3TouchSensor"/> instance
        /// for the EV3TouchSensor that's used in the grappler
        /// </summary>
        private static EV3TouchSensor GrapplerTouchSensor { get; set; }

        #endregion

        #endregion

        #region Methods

        #region Public Logic

        /// <summary>
        /// this method initializes all needed motors and sensors
        /// to control the robot accordingly
        /// </summary>
        public static void InitRobot()
        {
            LeftTrack = new Motor(Constants.LeftTrackPort);
            RightTrack = new Motor(Constants.RightTrackPort);
            GrapplerMotor = new Motor(Constants.GrapplerPort);

            ColorSensor = new EV3ColorSensor(Constants.ColorSensorPort);
            GrapplerTouchSensor = new EV3TouchSensor(Constants.GrapplerTouchSensorPort);

            GrapplerState = GrapplerState.Open;
            GrapplerPosition = GrapplerPosition.Down;
            FoodState = FoodState.Searching;

            IsInitialized = true;

            WaitToFullyBootProgram();
            PrintEmptyLine();
            WaitForButtonPress();

            InitializeGrappler();
        }

        /// <summary>
        /// drives into a certain direction until something is detected.
        /// if something is detected, the robot stops and waits for
        /// further instructions
        /// </summary>
        public static void SearchFood()
        {
            Print($"searching for food!");

            SetWheelSpeed(Constants.DriveForwardSpeed, Constants.DriveForwardSpeed);
            
            while (!AbyssDetected()
                && !ObstacleDetected()
                && !FoodplaceDetected()
                && !SingleFoodDetected()
                && !EnclosureDetected())
            {
            }

            HaltTracks();
        }

        /// <summary>
        /// lets the robot rotate in a certain speed and a certain duration
        /// </summary>
        /// <param name="rotationMode"></param>
        public static void RotateClockWise(RotationMode rotationMode)
        {
            Print($"rotating clockwise in mode {rotationMode}");

            SetWheelSpeed(Constants.RotationSpeed,
                          Convert.ToSByte(-Constants.RotationSpeed));

            if (rotationMode == RotationMode.TimerMode)
            {
                DateTime startTime = DateTime.Now;

                while (!TimerBreakCondition(startTime, Constants.RotationDuration))
                {
                }
            }
            else
            {
                while (AbyssDetected() || ObstacleDetected())
                {
                }
            }

            HaltTracks();
        }

        /// <summary>
        /// this method halts the two large motors that are responsible
        /// for rotating the two main Track that transport the robot
        /// </summary>
        public static void HaltTracks()
        {
            LeftTrack.Brake();
            RightTrack.Brake();
        }

        public static void HaltMotors()
        {
            HaltTracks();
            GrapplerMotor.Brake();
        }

        #endregion

        #region Public Helper

        /// <summary>
        /// prints a specific output string to the LCD-console of
        /// the Lego EV3 brick
        /// </summary>
        /// <param name="output">the output message to the console</param>
        public static void Print(string output)
        {
            LcdConsole.WriteLine("{0}", output);
        }

        /// <summary>
        /// prints a empty line seperator to the ev3 lcd console
        /// </summary>
        public static void PrintEmptyLine()
        {
            LcdConsole.WriteLine("{0}", "");
        }

        #endregion

        #region Sensor Facades

        /// <summary>
        /// returns the current colorId of the EV3ColorSensor
        /// </summary>
        /// <returns>
        /// int: the current colorId seen by the EV3ColorSensor
        /// </returns>
        public static int GetColorId()
        {
            return ColorSensor.Read();
        }

        /// <summary>
        /// returns the current color name of the EV3ColorSensor
        /// </summary>
        /// <returns>
        /// string: the current color name seen by the EV3ColorSensor
        /// </returns>
        public static string GetColorName()
        {
            return ColorSensor.ReadAsString();
        }

        #endregion

        #region Private Logic

        /// <summary>
        /// puts the grappler in the default-state
        /// </summary>
        private static void InitializeGrappler()
        {
            CloseAndRiseGrappler();
            PutDownAndOpenGrapplerOnMeadow();
        }

        /// <summary>
        /// closes and rises the grappler if its on the ground
        /// </summary>
        private static void CloseAndRiseGrappler()
        {
            // no need to check more, because the pressure sensor
            // stops either way
            if (GrapplerPosition == GrapplerPosition.Down)
            {
                GrapplerMotor.SetSpeed(Constants.GrapplerMotorUpSpeed);

                while (!GrapplerTouchSensor.IsPressed())
                {
                }

                GrapplerMotor.Brake();
            }

            GrapplerPosition = GrapplerPosition.Up;
            GrapplerState = GrapplerState.Closed;
        }

        /// <summary>
        /// puts down the grappler and opens it, if it's in the air
        /// closed
        /// </summary>
        private static void PutDownAndOpenGrapplerOnMeadow()
        {
            if (GrapplerPosition == GrapplerPosition.Up
                && GrapplerState == GrapplerState.Closed
                && !EnclosureDetected())
            {
                GrapplerMotor.ResetTacho();
                GrapplerMotor.SetSpeed(Constants.GrapplerMotorDownSpeed);

                while (GrapplerMotor.GetTachoCount() > Constants.GrapplerUpToDownTachoBoundary)
                {
                    //Robot.Print($"tacho = {GrapplerMotor.GetTachoCount().ToString()}");
                }

                GrapplerMotor.Brake();
            }
        }

        /// <summary>
        /// sets the wheel speed of the two tracks of the robot
        /// </summary>
        /// <param name="leftTrackSpeed">speed for the left track</param>
        /// <param name="rightTrackWheelSpeed">speed for the right track</param>
        private static void SetWheelSpeed(sbyte leftTrackSpeed, sbyte rightTrackWheelSpeed)
        {
            LeftTrack.SetSpeed(leftTrackSpeed);
            RightTrack.SetSpeed(rightTrackWheelSpeed);
        }

        /// <summary>
        /// waits a certain amount of time to let the program fully boot
        /// </summary>
        private static void WaitToFullyBootProgram()
        {
            DateTime loadStart = DateTime.Now;

            Robot.Print($"Wait for {Constants.ProgramBootTime} milliseconds");

            while (!TimerBreakCondition(loadStart, Constants.ProgramBootTime))
            {
            }
        }

        /// <summary>
        /// waits until the operator presses the middle-button to
        /// start the competition mode
        /// </summary>
        private static void WaitForButtonPress()
        {
            bool continueWithCompetition = false;

            ButtonEvents btnEvents = new ButtonEvents();

            Action btnAction = () => {
                continueWithCompetition = true;
            };

            btnEvents.EnterPressed += btnAction;

            Robot.Print(Constants.CompetitionStartUserMessagePart1);
            Robot.Print(Constants.CompetitionStartUserMessagePart2);

            while (!continueWithCompetition)
            {
                Thread.Sleep(Constants.SamplingRate);
            }

            btnEvents.EnterPressed -= btnAction;
        }

        #endregion

        #region Events

        /// <summary>
        /// returns a boolean indicating if an abyss is detected in
        /// front of the robot
        /// </summary>
        /// <returns>
        /// true:  abyss was detected
        /// false: no abyss found
        /// </returns>
        public static bool AbyssDetected()
        {
            bool result = false;

            //TODO

            if (result)
            {
                LastStopReason = StopReason.AbyssDetected;
            }

            return result;
        }

        /// <summary>
        /// returns a boolean indicating if an obstacle (enemy robot
        /// or animal) is detected in front of the robot
        /// </summary>
        /// <returns>
        /// true:  a obstacle is in front of the robot
        /// false: no obstacle found
        /// </returns>
        public static bool ObstacleDetected()
        {
            bool result = false;

            //TODO

            if (result)
            {
                LastStopReason = StopReason.ObstacleDetected;
            }

            return result;
        }

        /// <summary>
        /// returns a boolean indicating if a foodplace was found
        /// in front of the robot
        /// </summary>
        /// <returns>
        /// true:  a food place was found
        /// false: no food place found
        /// </returns>
        public static bool FoodplaceDetected()
        {
            bool result = false;
            
            //TODO

            if (result)
            {
                LastStopReason = StopReason.FoodplaceDetected;
            }

            return result;
        }

        /// <summary>
        /// returns a boolean indicating if a single fwood brick
        /// is in front of the robot
        /// </summary>
        /// <returns>
        /// true:  a food brick was found in front of the robot
        /// false: no food brick in front of the robot
        /// </returns>
        public static bool SingleFoodDetected()
        {
            bool result = false;

            //TODO

            if (result)
            {
                LastStopReason = StopReason.SingleFoodDetected;
            }

            return result;
        }

        /// <summary>
        /// returns a boolean indicating if there's a enclosure in front
        /// of the robot
        /// </summary>
        /// <returns>
        /// true:  there's a enclosure in front of the robot
        /// false: no enclosure in front of the robot
        /// </returns>
        public static bool EnclosureDetected()
        {
            bool result = false;

            //TODO

            if (result)
            {
                LastStopReason = StopReason.EnclosureDetected;
            }

            return result;
        }

        /// <summary>
        /// this method returns an boolean indicating if a certain action
        /// should be stopped
        /// </summary>
        /// <returns>
        /// true: a certain action should be stopped
        /// false: the action should continue
        /// </returns>
        public static bool TimerBreakCondition(DateTime startTime, int duration)
        {
            return (DateTime.Now - startTime).TotalMilliseconds >= duration;
        }

        #endregion

        #endregion
    }
}