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

        #region LcsOutputs
        public static string competitionStartUserMessagePart1
            = "Press the middle button to";
        public static string competitionStartUserMessagePart2
            = "continue with the competition";
        #endregion

        #region
        public static int lcdErrorDuration = 15000;
        public static int programBootTime = 10000;
        public static int samplingRate = 250;
        #endregion

        #region Error Messages
        public static string initializeErrorMessage
            = "SOMETHING WENT WRONG WHILE INITLIZING";
        #endregion
    }
}
