using MonoBrickFirmware.Movement;
using MonoBrickFirmware.Sensors;

namespace BrickSolution.Logic
{
    internal static class Constants
    {
        #region Motors
        public static MotorPort leftTrackPort = MotorPort.OutA;
        public static MotorPort rightTrackPort = MotorPort.OutD;
        #endregion

        #region Sensors
        public static SensorPort colorSensorPort = SensorPort.In2;
        #endregion

        #region
        public static int LcdErrorDuration = 15000;
        #endregion

        #region Error Messages
        public static string INITIALIZE_ERROR_MSG = "SOMETHING WENT WRONG WHILE INITLIZING";
        #endregion
    }
}
