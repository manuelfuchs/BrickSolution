using MonoBrickFirmware.Movement;
using MonoBrickFirmware.Sensors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MonoBrickTest
{
    public class Robot
    {
        #region Constructor

        private Robot()
        {
            this.LeftWheel = new Motor(Constants.leftLargeMotorPort);
            this.RightWheel = new Motor(Constants.rightLargeMotorPort);

            this.IRSensor = new EV3IRSensor(Constants.iRSensorPort);

            this.ColorSensor = new NXTColorSensor(Constants.colorSensorPort, ColorMode.Color);
        }

        #endregion

        #region Singleton Pattern

        private static Robot instance = null;

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

        private Motor LeftWheel { get; set; }
        private Motor RightWheel { get; set; }

        #endregion

        #region Sensors

        private EV3IRSensor IRSensor { get; set; }
        private NXTColorSensor ColorSensor{ get; set; }

        #endregion

        #endregion

        #region Methods

        #region Public Helper

        public object[] GenerateParameter(params object[] parameter)
        {
            return parameter;
        }

        #endregion

        #region Public Logic

        public void Drive(sbyte speed, Func<object[], bool> breakCondition = null, object[] parameter = null)
        {
            this.SetWheelSpeed(speed, speed);

            if (breakCondition != null && parameter != null)
            {
                this.HaltWheelsWhen(breakCondition, parameter);
            }
        }

        public void RotateClockWise(sbyte speed, Func<object[], bool> breakCondition)
        {
            this.Rotate(speed, speed, breakCondition);
        }

        public void RotateCounterClockWise(sbyte speed, Func<object[], bool> breakCondition)
        {
            this.Rotate(speed, speed, breakCondition);
        }

        public void HaltWheels()
        {
            this.LeftWheel.Brake();
            this.RightWheel.Brake();
        }

        #endregion

        #region Private Logic

        private void HaltWheelsWhen(Func<object[], bool> breakCondition, object[] parameter)
        {
            while (!breakCondition(parameter))
            {
            }

            this.HaltWheels();
        }

        private void Rotate(sbyte leftWheelSpeed, sbyte rightWheelSpeed, Func<object[], bool> breakCondition)
        {

        }

        private void SetWheelSpeed(sbyte leftWheelSpeed, sbyte rightWheelSpeed)
        {
            this.LeftWheel.SetSpeed(leftWheelSpeed);
            this.RightWheel.SetSpeed(rightWheelSpeed);
        }

        #endregion

        #region Logic-Conditions
        //TEST: TO REMOVE LATER
        private int counter = 0;
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter">
        /// parameter[0]=distance
        /// parameter[1]=whenSmallerBoolean(optional)
        /// parameter[2]=whenEqualBoolean(optional)</param>
        /// <returns></returns>
        public bool IRBreakCondition(object[] parameter)
        {
            int breakDistance = (int)parameter[0];
            bool whenSmaller = parameter.Length >= 2 ? (bool)parameter[1] : false;

            int iRDistance = this.IRSensor.ReadDistance();

            MonoBrickFirmware.Display.LcdConsole.WriteLine("{0} {1} {2}", iRDistance, breakDistance, whenSmaller);

            if (iRDistance < breakDistance)
            {
                return whenSmaller == false ? true : false;
            }
            else
            {
                return whenSmaller == true ? true : false;
            }
        }

        public bool ColorBreakCondition(object[] parameter)
        {
            String color = this.ColorSensor.ReadAsString();
            Color

            

           
            return true;
        }

        #endregion

        #endregion
    }
}
