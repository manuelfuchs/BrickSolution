using MonoBrickFirmware.Movement;
using MonoBrickFirmware.Sensors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoBrickTest
{
    internal static class Constants
    {
        public static MotorPort leftLargeMotorPort = MotorPort.OutA;
        public static MotorPort rightLargeMotorPort = MotorPort.OutD;

        public static SensorPort iRSensorPort = SensorPort.In1;

        public static SensorPort colorSensorPort = SensorPort.In2;
    }
}
