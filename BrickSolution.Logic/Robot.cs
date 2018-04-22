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
        /// <summary>
        /// privat constructor thats used in the implemented
        /// singleton pattern
        /// </summary>
        public static void InitRobot()
        {
            LeftWheel = new Motor(Constants.leftLargeMotorPort);
            RightWheel = new Motor(Constants.rightLargeMotorPort);

            IRSensor = new EV3IRSensor(Constants.iRSensorPort);
            ColorSensor = new EV3ColorSensor(Constants.colorSensorPort);

            GrapplerState = Enumerations.GrapplerState.Open;
            GrapplerPosition = Enumerations.GrapplerPosition.Down;
            FoodState = Enumerations.FoodState.Searching;

            IsInitialized = true;
        }

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
        /// EV3 motor that is responsible for the left wheel
        /// </summary>
        private static Motor LeftWheel { get; set; }
        /// <summary>
        /// holds the <see cref="Motor"/> instance to a large
        /// EV3 motor that is responsible for the right wheel
        /// </summary>
        private static Motor RightWheel { get; set; }

        #endregion

        #region Sensors

        /// <summary>
        /// holds the <see cref="EV3IRSensor"/> instance for
        /// the EV3IRSensor
        /// </summary>
        private static EV3IRSensor IRSensor { get; set; }

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

        /// <summary>
        /// this method is used to smothly generate a object array.
        /// is later used in the action taking methods of the robot
        /// instance.
        /// </summary>
        /// <param name="parameter">
        /// the parameters later needed in the break conditions
        /// </param>
        /// <returns>
        /// object[]: a object array that consists of all passed parameters
        /// </returns>
        public static object[] GenerateParameter(params object[] parameter)
        {
            return parameter;
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

        /// <summary>
        /// returns the current distance between the IRSensor and
        /// the object in front of it
        /// </summary>
        /// <returns>
        /// int: the distance between an object and the IRSensor (max: 100)
        /// </returns>
        public static int GetIRDistance()
        {
            return IRSensor.ReadDistance();
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
        public static void Drive(sbyte speed, Func<object[], bool> breakCondition, object[] parameter)
        {
            SetWheelSpeed(speed, speed);

            if (breakCondition != null && parameter != null)
            {
                HaltWheelsWhen(breakCondition, parameter);
            }
        }

        /// <summary>
        /// this method lets the robot rotate clockwise
        /// </summary>
        /// <param name="speed">the speed of <see cref="Motor"/>s</param>
        /// <param name="breakCondition">the condition when the rotation should stop</param>
        public static void RotateClockWise(sbyte speed, Func<object[], bool> breakCondition)
        {
            //TODO
            throw new NotImplementedException(nameof(RotateClockWise));
            //this.Rotate(speed, speed, breakCondition);
        }

        /// <summary>
        /// this method halts the two large motors that are responsible
        /// for rotating the two main wheels that transport the robot
        /// </summary>
        public static void HaltWheels()
        {
            LeftWheel.Brake();
            RightWheel.Brake();
        }

        #endregion

        #region Private Logic

        /// <summary>
        /// this method is responsible for halting the wheels when a certain
        /// break condition is met
        /// </summary>
        /// <param name="breakCondition">the condition that indicates when
        /// the wheels should halt</param>
        /// <param name="parameter">the parameters for the breakCondition</param>
        private static void HaltWheelsWhen(Func<object[], bool> breakCondition, object[] parameter)
        {
            while (!breakCondition(parameter))
            {
            }

            HaltWheels();
        }

        private static void Rotate(sbyte leftWheelSpeed, sbyte rightWheelSpeed, Func<object[], bool> breakCondition)
        {
            //TODO
            throw new NotImplementedException(nameof(Rotate));
        }

        /// <summary>
        /// this methods sets the wheel speed on the two large motors that
        /// are responsible for rotating the robots wheels
        /// </summary>
        /// <param name="leftWheelSpeed">the speed for the left wheel</param>
        /// <param name="rightWheelSpeed">the speed for the right wheel</param>
        private static void SetWheelSpeed(sbyte leftWheelSpeed, sbyte rightWheelSpeed)
        {
            LeftWheel.SetSpeed(leftWheelSpeed);
            RightWheel.SetSpeed(rightWheelSpeed);
        }

        #endregion

        #region Break-Conditions

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public static bool AbyssDetected(object[] parameter)
        {
            throw new NotImplementedException(nameof(AbyssDetected));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public static bool ObstacleDetected(object[] parameter)
        {
            throw new NotImplementedException(nameof(ObstacleDetected));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public static bool FoodplaceDetected(object[] parameter)
        {
            throw new NotImplementedException(nameof(FoodplaceDetected));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public static bool SingleFoodDetected(object[] parameter)
        {
            throw new NotImplementedException(nameof(SingleFoodDetected));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public static bool EnclosureDetected(object[] parameter)
        {
            throw new NotImplementedException(nameof(EnclosureDetected));
        }

        #endregion

        #endregion
    }
}