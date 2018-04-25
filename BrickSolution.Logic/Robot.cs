using MonoBrickFirmware.Display;
using MonoBrickFirmware.Movement;
using MonoBrickFirmware.Sensors;
using System;

namespace BrickSolution.Logic
{
    /// <summary>
    /// the singleton <see cref="Robot"/> instance holds all necessary methods
    /// to control it correctly through the SPS tasks
    /// </summary>
    public class Robot
    {
       

        #region Properties

        #region States

        /// <summary>
        /// Is used to indicate if the robot was initialized
        /// correctly
        /// </summary>
        public static bool IsInitialized { get; set; }

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

        #endregion

        #endregion

        #region Methods

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

        #region Public Logic

        /// <summary>
        /// this method lets the robot drive in a certain speed until a certain
        /// breakCondition
        /// </summary>
        /// <param name="speed">the speed with that the robot drives</param>
        /// <param name="breakCondition">when the robot should stop driving</param>
        /// <param name="parameter">the parameters for the break condition</param>
        public static void Drive(sbyte speed, Func<bool> breakCondition, object[] parameter)
        {
            SetTracksSpeed(speed, speed);

            if (breakCondition != null && parameter != null)
            {
                HaltTracksWhen(breakCondition);
            }
        }

        /// <summary>
        /// this method lets the robot rotate clockwise
        /// </summary>
        /// <param name="speed">the speed of <see cref="Motor"/>s</param>
        /// <param name="breakCondition">the condition when the rotation should stop</param>
        public static void RotateClockWise(sbyte speed, Func<bool> breakCondition)
        {
            throw new NotImplementedException(nameof(RotateClockWise));
            //this.Rotate(speed, speed, breakCondition);
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

        #endregion

        #region Private Logic

        /// <summary>
        /// privat constructor thats used in the implemented
        /// singleton pattern
        /// </summary>
        public static void InitRobot()
        {
            LeftTrack = new Motor(Constants.leftTrackPort);
            RightTrack = new Motor(Constants.rightTrackPort);
            GrapplerMotor = new Motor(Constants.grapplerPort);

            ColorSensor = new EV3ColorSensor(Constants.colorSensorPort);

            GrapplerState = Enumerations.GrapplerState.Open;
            GrapplerPosition = Enumerations.GrapplerPosition.Down;
            FoodState = Enumerations.FoodState.Searching;

            IsInitialized = true;
        }

        /// <summary>
        /// this method is responsible for halting the Track when a certain
        /// break condition is met
        /// </summary>
        /// <param name="breakCondition">the condition that indicates when
        /// the Track should halt</param>
        /// <param name="parameter">the parameters for the breakCondition</param>
        private static void HaltTracksWhen(Func<bool> breakCondition)
        {
            while (!breakCondition())
            {
            }

            HaltTracks();
        }

        /// <summary>
        /// rotat
        /// </summary>
        /// <param name="leftTrackpeed"></param>
        /// <param name="rightTrackpeed"></param>
        /// <param name="breakCondition"></param>
        private static void Rotate(sbyte leftTrackpeed, sbyte rightTrackpeed, Func<bool> breakCondition)
        {
            throw new NotImplementedException(nameof(Rotate));
        }

        /// <summary>
        /// this methods sets the track speed on the two large motors that
        /// are responsible for rotating the robots track
        /// </summary>
        /// <param name="leftTrackSpeed">the speed for the left Track</param>
        /// <param name="rightTrackSpeed">the speed for the right Track</param>
        private static void SetTracksSpeed(sbyte leftTrackSpeed, sbyte rightTrackSpeed)
        {
            LeftTrack.SetSpeed(leftTrackSpeed);
            RightTrack.SetSpeed(rightTrackSpeed);
        }

        #endregion

        #region Break-Conditions

        /// <summary>
        /// returns a boolean indicating if a abyss is detected in
        /// front of the robot
        /// </summary>
        /// <returns>
        /// true:  abyss was detected
        /// false: no abyss found
        /// </returns>
        public static bool AbyssDetected()
        {
            throw new NotImplementedException(nameof(AbyssDetected));
        }

        /// <summary>
        /// returns a boolean indicating if a obstacle is detected
        /// in front of the robot
        /// </summary>
        /// <returns>
        /// true:  a obstacle is in front of the robot
        /// false: no obstacle found
        /// </returns>
        public static bool ObstacleDetected()
        {
            throw new NotImplementedException(nameof(ObstacleDetected));
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
            throw new NotImplementedException(nameof(FoodplaceDetected));
        }

        /// <summary>
        /// returns a boolean indicating if a single bood brick
        /// is in front of the robot
        /// </summary>
        /// <returns>
        /// true:  a food brick was found in front of the robot
        /// false: no food brick in front of the robot
        /// </returns>
        public static bool SingleFoodDetected()
        {
            throw new NotImplementedException(nameof(SingleFoodDetected));
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
            throw new NotImplementedException(nameof(EnclosureDetected));
        }

        /// <summary>
        /// this method returns an boolean indicating if a certain action
        /// should be stopped
        /// </summary>
        /// <returns>
        /// true: a certain action should be stopped
        /// false: the action should continue
        /// </returns>
        public bool TimerBreakCondition()
        {
            throw new NotImplementedException(nameof(TimerBreakCondition));
        }

        #endregion

        #endregion
    }
}