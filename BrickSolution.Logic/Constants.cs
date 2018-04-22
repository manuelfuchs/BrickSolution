using MonoBrickFirmware.Movement;
using MonoBrickFirmware.Sensors;

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
        #endregion

        #region
        public static int LcdErrorDuration = 15000;
        #endregion

        #region Error Messages
        public static string INITIALIZE_ERROR = "SOMETHING WENT WRONG WHILE INITLIZING";
        #endregion
    }
}
