using MonoBrickFirmware.Movement;
using MonoBrickFirmware.Sensors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            return instance;
        }

        #endregion

        #region Properties

        #region Motors

        private Motor LeftWheel { get; set; }
        private Motor RightWheel { get; set; }

        #endregion

        #region Sensors

        private EV3IRSensor IRSensor { get; set; }

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
            this.SetWheelSpeed(0, 0);
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
            bool whenSmaller = (bool)parameter[1];
            bool whenEqual = (bool)parameter[2];

            int iRDistance = this.IRSensor.ReadDistance();

            if (iRDistance < breakDistance)
            {
                return whenSmaller == true ? true : false;
            }
            else if (iRDistance == breakDistance && whenEqual == true)
            {
                return true;
            }
            else
            {
                return whenSmaller == false ? true : false;
            }
        }
        public bool OLDIRBreakCondition(int distance, bool whenSmaller = true, bool whenEqual = false)
        {
            int iRDistance = this.IRSensor.ReadDistance();

            if (iRDistance < distance)
            {
                return whenSmaller == true ? true : false;
            }
            else if (iRDistance == distance && whenEqual == true)
            {
                return true;
            }
            else
            {
                return whenSmaller == false ? true : false;
            }
        }

        #endregion

        #endregion
    }
}
