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
        #region Constructor

        /// <summary>
        /// privat constructor thats used in the implemented
        /// singleton pattern
        /// </summary>
        private Robot()
        {
            this.LeftWheel = new Motor(Constants.leftLargeMotorPort);
            this.RightWheel = new Motor(Constants.rightLargeMotorPort);

            this.IRSensor = new EV3IRSensor(Constants.iRSensorPort);
        }

        #endregion

        #region Singleton Pattern

        /// <summary>
        /// field that holds the one and only instance (or null before first call)
        /// to the <see cref="Robot"/> class
        /// </summary>
        private static Robot instance = null;

        /// <summary>
        /// returns the instance of the Robot class (singleton implementation)
        /// </summary>
        /// <returns>
        /// an instance of <see cref="Robot"/>
        /// </returns>
        public static Robot GetInstance()
        {
            if (Robot.instance == null)
            {
                instance = new Robot();
            }

            return Robot.instance;
        }

        #endregion

        #region Properties

        #region Motors

        /// <summary>
        /// holds the <see cref="Motor"/> instance to a large
        /// EV3 motor that is responsible for the left wheel
        /// </summary>
        private Motor LeftWheel { get; set; }
        /// <summary>
        /// holds the <see cref="Motor"/> instance to a large
        /// EV3 motor that is responsible for the right wheel
        /// </summary>
        private Motor RightWheel { get; set; }

        #endregion

        #region Sensors

        /// <summary>
        /// holds the <see cref="EV3IRSensor"/> instance for
        /// the IRSensor
        /// </summary>
        private EV3IRSensor IRSensor { get; set; }

        #endregion

        #endregion

        #region Methods

        #region Public Helper

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
        public object[] GenerateParameter(params object[] parameter)
        {
            return parameter;
        }

        #endregion

        #region Methods

        #region Public Logic

        /// <summary>
        /// this method lets the robot drive in a certain speed until a certain
        /// breakCondition
        /// </summary>
        /// <param name="speed">the speed with that the robot drives</param>
        /// <param name="breakCondition">when the robot should stop driving</param>
        /// <param name="parameter">the parameters for the break condition</param>
        public void Drive(sbyte speed, Func<object[], bool> breakCondition, object[] parameter)
        {
            this.SetWheelSpeed(speed, speed);

            if (breakCondition != null && parameter != null)
            {
                this.HaltWheelsWhen(breakCondition, parameter);
            }
        }

        /// <summary>
        /// this method lets the robot rotate clockwise
        /// </summary>
        /// <param name="speed">the speed of <see cref="Motor"/>s</param>
        /// <param name="breakCondition">the condition when the rotation should stop</param>
        public void RotateClockWise(sbyte speed, Func<object[], bool> breakCondition)
        {
            //TODO
            throw new NotImplementedException(nameof(RotateClockWise));
            //this.Rotate(speed, speed, breakCondition);
        }

        /// <summary>
        /// this method halts the two large motors that are responsible
        /// for rotating the two main wheels that transport the robot
        /// </summary>
        public void HaltWheels()
        {
            this.LeftWheel.Brake();
            this.RightWheel.Brake();
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
        private void HaltWheelsWhen(Func<object[], bool> breakCondition, object[] parameter)
        {
            while (!breakCondition(parameter))
            {
            }

            this.HaltWheels();
        }

        private void Rotate(sbyte leftWheelSpeed, sbyte rightWheelSpeed, Func<object[], bool> breakCondition)
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
        private void SetWheelSpeed(sbyte leftWheelSpeed, sbyte rightWheelSpeed)
        {
            this.LeftWheel.SetSpeed(leftWheelSpeed);
            this.RightWheel.SetSpeed(rightWheelSpeed);
        }

        #endregion

        /// <summary>
        /// this method returns a boolean indicating if a certain event has taken
        /// place (is mostly used for triggering a.e. a motor stop)
        /// </summary>
        /// <param name="parameter">
        /// parameter[0]=distance(int)
        /// parameter[1]=whenSmallerBoolean(boolean,optional)</param>
        /// <returns>
        /// true: a certain action should be stopped
        /// false: the action should continue
        /// </returns>
        public bool IRBreakCondition(object[] parameter)
        {
            int breakDistance = Convert.ToInt32(parameter[0]);
            bool whenSmaller = parameter.Length >= 2 ? Convert.ToBoolean(parameter[1]) : false;

            int iRDistance = this.IRSensor.ReadDistance();
            
            MonoBrickFirmware.Display.LcdConsole
                .WriteLine("{0} {1} {2}", iRDistance, breakDistance, whenSmaller);

            if (iRDistance > breakDistance)
            {
                return whenSmaller == false ? true : false;
            }
            else
            {
                return whenSmaller == true ? true : false;
            }
        }

        /// <summary>
        /// this method returns an boolean indicating if a certain action
        /// should be stopped
        /// </summary>
        /// <param name="parameter">
        /// parameter[0]=milliseconds(int)
        /// parameter[1]=startTime(DateTime)</param>
        /// <returns>
        /// true: a certain action should be stopped
        /// false: the action should continue
        /// </returns>
        public bool TimerBreakCondition(object[] parameter)
        {
            int milliseconds = Convert.ToInt32(parameter[0]);
            DateTime startTime = (DateTime)parameter[1];

            TimeSpan difference = DateTime.Now - startTime;

            return difference.TotalMilliseconds >= milliseconds;
        }

        #endregion

        #endregion
    }
}