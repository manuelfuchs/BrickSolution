using MonoBrickFirmware.Movement;
using MonoBrickFirmware.Sensors;

namespace BrickSolution.Logic
{
    internal static class Constants
    {
        #region Motors
        public static MotorPort leftTrackPort = MotorPort.OutA;
        public static MotorPort rightTrackPort = MotorPort.OutB;
        public static MotorPort grapplerPort = MotorPort.OutD;
        #endregion

        #region Sensors
        public static SensorPort pressureSensorPort = SensorPort.In1;
        public static SensorPort colorSensorPort = SensorPort.In2;
        public static SensorPort ev3colorSensorPort = SensorPort.In3;


        #endregion

        #region
        public static int LcdErrorDuration = 15000;
        #endregion

        #region Error Messages
        public static string INITIALIZE_ERROR_MSG = "SOMETHING WENT WRONG WHILE INITLIZING";
        #endregion
    }
}
