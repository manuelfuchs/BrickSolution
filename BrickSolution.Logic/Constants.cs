using MonoBrickFirmware.Movement;
using MonoBrickFirmware.Sensors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrickSolution.Logic
{
    internal static class Constants
    {
        #region Motors
        public static MotorPort leftLargeMotorPort = MotorPort.OutA;
        public static MotorPort rightLargeMotorPort = MotorPort.OutD;
        #endregion

        #region Sensors
        public static SensorPort iRSensorPort = SensorPort.In1;
        public static SensorPort colorSensorPort = SensorPort.In2;
        public static SensorPort nxtColorSensorPort = SensorPort.In3;
        #endregion

        #region
        public static int tableEndDistance = 30;
        #endregion
    }
}
