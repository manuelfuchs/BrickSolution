using BrickSolution.Logic.Enumerations;
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
        /// holds the <see cref="Motor"/> instance to a EV3 motor
        /// that is responsible for the grappler rising
        /// </summary>
        private static Motor GrapplerRiserMotor { get; set; }

        /// <summary>
        /// holds the <see cref="Motor"/> instance to a EV3 motor
        /// that is responsible for the grappler wheel
        /// </summary>
        private static Motor GrapplerWheelMotor { get; set; }

        #endregion

        #region Sensors

        /// <summary>
        /// holds the <see cref="EV3ColorSensor"/> instance
        /// for the EV3ColorSensor
        /// </summary>
        private static EV3ColorSensor ColorSensor { get; set; }

        /// <summary>
        /// holds the <see cref="EV3IRSensor"/> instance
        /// for the EV3IRSensor
        /// </summary>
        private static EV3IRSensor IRSensor { get; set; }

        /// <summary>
        /// holds the <see cref="EV3UltrasonicSensor"/> instance
        /// for the EV3UltrasonicSensor
        /// </summary>
        private static EV3UltrasonicSensor UltraSonicSensor { get; set; }

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
            AddEmergencyStopOption();

            LeftTrack = new Motor(Constants.LEFT_TRACK_PORT);
            RightTrack = new Motor(Constants.RIGHT_TRACK_PORT);
            GrapplerRiserMotor = new Motor(Constants.GRAPPLER_RISER_PORT);
            GrapplerWheelMotor = new Motor(Constants.GRAPPLER_WHEEL_PORT);

            ColorSensor = new EV3ColorSensor(Constants.ULTRASONIC_SENSOR_PORT);
            IRSensor = new EV3IRSensor(Constants.IR_SENSOR_PORT);
            UltraSonicSensor = new EV3UltrasonicSensor(Constants.ULTRASONIC_SENSOR_PORT);
            UltraSonicSensor.Mode = UltraSonicMode.Centimeter;
            
            GrapplerPosition = GrapplerPosition.Down;
            FoodState = FoodState.Searching;

            IsInitialized = true;

            WaitToFullyBootProgram();
            
            CalibrizeGrappler();

            PrintEmptyLine();
            WaitForStartButtonPress();
        }

        public static void DisposeComponents()
        {
            if (IsInitialized)
            { 
                GC.SuppressFinalize(LeftTrack);
                GC.SuppressFinalize(RightTrack);
                GC.SuppressFinalize(GrapplerRiserMotor);
                GC.SuppressFinalize(GrapplerWheelMotor);

                GC.SuppressFinalize(ColorSensor);
                GC.SuppressFinalize(IRSensor);
                GC.SuppressFinalize(UltraSonicSensor);
            }
        }

        /// <summary>
        /// drives into a certain direction until something is detected.
        /// if something is detected, the robot stops and waits for
        /// further instructions
        /// </summary>
        public static void Drive()
        {
            Print($"searching for food!");

            SetTrackSpeed(Constants.DRIVE_FORWARD_SPEED, Constants.DRIVE_FORWARD_SPEED);
            
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

            SetTrackSpeed(Constants.ROTATION_SPEED,
                          Convert.ToSByte(-Constants.ROTATION_SPEED));

            if (rotationMode == RotationMode.TimerMode)
            {
                DateTime startTime = DateTime.Now;

                while (!TimerBreakCondition(startTime, Constants.ROTATION_DURATION))
                {
                }
            }
            else
            {
                while (AbyssDetected() || ObstacleDetected())
                {
                }

                Thread.Sleep(1000);
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

        /// <summary>
        /// halts the two motors that are responsible for rising/lowering
        /// and turning the grappler
        /// </summary>
        public static void TurnOffGrappler()
        {
            GrapplerRiserMotor.Off();
            GrapplerWheelMotor.Off();
            GrapplerPosition = GrapplerPosition.Down;
        }

        /// <summary>
        /// halts all motors of the robot
        /// </summary>
        public static void HaltMotors()
        {
            HaltTracks();
            TurnOffGrappler();
        }

        /// <summary>
        /// contains logic to collect food:
        ///   .) drive a bit back
        ///   .) lower grappler
        ///   .) drive a bit forward with rotating grappler-wheel
        ///   .) rise grappler
        /// </summary>
        public static void CollectFood()
        {
            if (FoodState == FoodState.Searching)
            {
                SetTrackSpeed(Constants.DRIVE_BACKWARD_SPEED, Constants.DRIVE_BACKWARD_SPEED);
                Thread.Sleep(1500);
                TurnOffGrappler();
                GrapplerWheelMotor.SetSpeed(Constants.GRAPPLER_WHEEL_SPEED);
                SetTrackSpeed(Constants.DRIVE_FORWARD_AFT_BACKWARD_SPEED,
                              Constants.DRIVE_FORWARD_AFT_BACKWARD_SPEED);
                Thread.Sleep(1500);
                HaltMotors();
                RiseGrappler();

                FoodState = FoodState.Carrying;
            }
        }

        public static void ReleaseFood()
        {
            if (FoodState == FoodState.Carrying)
            {
                GrapplerWheelMotor.ResetTacho();
                GrapplerWheelMotor.SetSpeed(Constants.GRAPPLER_WHEEL_SPEED);

                while (Math.Abs(GrapplerWheelMotor.GetTachoCount())
                    < Constants.GRAPPLER_WHEEL_BOUNDARY)
                {
                }

                GrapplerWheelMotor.Off();

                FoodState = FoodState.Searching;
            }
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
        /// prints a specific output object (using the toString method)
        /// to the LCD-console of the Lego EV3 brick
        /// </summary>
        /// <param name="output">the output message to the console</param>
        public static void Print(object outputObject)
        {
            Robot.Print(outputObject.ToString());
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
        private static int GetColorId()
        {
            return ColorSensor.Read();
        }

        /// <summary>
        /// returns the current color name of the EV3ColorSensor
        /// </summary>
        /// <returns>
        /// string: the current color name seen by the EV3ColorSensor
        /// </returns>
        private static string GetColorName()
        {
            return ColorSensor.ReadAsString();
        }

        /// <summary>
        /// returns either the current irdistance or a default
        /// value that indicates, that the irsensor is currently
        /// covered by the grappler and can't see in the
        /// distance
        /// </summary>
        /// <returns></returns>
        public static int GetIRDistance()
        {
            if (GrapplerPosition == GrapplerPosition.Up)
            {
                return IRSensor.ReadDistance();
            }
            else
            {
                return Constants.IR_VALUES_NOT_INTERPRETABLE_VALUE;
            }
        }

        /// <summary>
        /// returns the value of the distance that the ultrasonic sensor
        /// currently measures in centimeter
        /// </summary>
        /// <returns></returns>
        public static int GetUltraSonicDistance()
        {
            if (UltraSonicSensor.Mode != UltraSonicMode.Centimeter)
            {
                UltraSonicSensor.Mode = UltraSonicMode.Centimeter;
            }

            return UltraSonicSensor.Read();
        }

        #endregion

        #region Private Logic

        /// <summary>
        /// adds a button event on the escape button that throws
        /// an exception and halts the motors
        /// </summary>
        private static void AddEmergencyStopOption()
        {
            ButtonEvents buttonEvents = new ButtonEvents();

            Action emergencyStopAction = () =>
            {
                Robot.HaltMotors();
                throw new Exception();
            };

            buttonEvents.EscapePressed += emergencyStopAction;
        }

        /// <summary>
        /// puts the grappler in the default-state
        /// </summary>
        private static void CalibrizeGrappler()
        {
            Print("Calibrizing grappler");
            TurnOffGrappler();
            Thread.Sleep(2000);
            RiseGrappler();
        }

        /// <summary>
        /// closes and rises the grappler if its on the ground
        /// </summary>
        private static void RiseGrappler()
        {
            // no need to check more, because the pressure sensor
            // stops either way
            if (GrapplerPosition == GrapplerPosition.Down)
            {
                GrapplerRiserMotor.ResetTacho();
                GrapplerRiserMotor.SetSpeed(Constants.GRAPPLER_RISER_SPEED);

                while (Math.Abs(GrapplerRiserMotor.GetTachoCount())
                    < Constants.GRAPPLER_RISER_TACHO_BOUNDARY)
                {
                }

                GrapplerRiserMotor.Brake();
            }

            GrapplerPosition = GrapplerPosition.Up;
        }

        /// <summary>
        /// puts down the grappler and opens it, if it's in the air
        /// closed
        /// </summary>
        private static void RealeaseBrickOnMeadow()
        {
            if (GrapplerPosition == GrapplerPosition.Up
                && FoodState == FoodState.Carrying
                && EnclosureDetected())
            {
                GrapplerWheelMotor.ResetTacho();
                GrapplerWheelMotor.SetSpeed(Constants.GRAPPLER_WHEEL_SPEED);

                while (Math.Abs(GrapplerWheelMotor.GetTachoCount())
                    < Constants.GRAPPLER_WHEEL_BOUNDARY)
                {
                    //Robot.Print($"tacho = {GrapplerMotor.GetTachoCount().ToString()}");
                }

                GrapplerRiserMotor.Off();
            }
        }

        /// <summary>
        /// sets the wheel speed of the two tracks of the robot
        /// </summary>
        /// <param name="leftTrackSpeed">speed for the left track</param>
        /// <param name="rightTrackWheelSpeed">speed for the right track</param>
        private static void SetTrackSpeed(sbyte leftTrackSpeed, sbyte rightTrackWheelSpeed)
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

            Robot.Print($"Wait for {Constants.PROGRAM_BOOT_DELAY} milliseconds");

            while (!TimerBreakCondition(loadStart, Constants.PROGRAM_BOOT_DELAY))
            {
            }
        }

        /// <summary>
        /// waits until the operator presses the middle-button to
        /// start the competition mode
        /// </summary>
        private static void WaitForStartButtonPress()
        {
            bool continueWithCompetition = false;

            ButtonEvents btnEvents = new ButtonEvents();

            Action btnAction = () => {
                continueWithCompetition = true;
            };

            btnEvents.EnterPressed += btnAction;

            Robot.Print(Constants.COMPETITION_START_USER_MSG_PART1);
            Robot.Print(Constants.COMPETITION_START_USER_MSG_PART2);

            while (!continueWithCompetition)
            {
                Thread.Sleep(Constants.SAMPLING_RATE);
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
            bool result = GetUltraSonicDistance() > Constants.ULTRA_SONIC_TABLE_END_VALUE
                       || GetIRDistance() > Constants.IR_TABLE_END_VALUE;

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