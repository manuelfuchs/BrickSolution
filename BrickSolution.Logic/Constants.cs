using MonoBrickFirmware.Movement;
using MonoBrickFirmware.Sensors;

namespace BrickSolution.Logic
{
    internal static class Constants
    {
        #region Motors
        public static MotorPort LeftTrackPort = MotorPort.OutA;
        public static MotorPort RightTrackPort = MotorPort.OutB;
        public static MotorPort GrapplerPort = MotorPort.OutD;
        #endregion

        #region Sensors
        public static SensorPort PressureSensorPort = SensorPort.In1;
        public static SensorPort ColorSensorPort = SensorPort.In2;
        public static SensorPort LegoColorSensorPort = SensorPort.In3;
        #endregion

        #region LcdOutputs
        public static string CompetitionStartUserMessagePart1
            = "Press the middle button to";
        public static string CompetitionStartUserMessagePart2
            = "continue with the competition";
        public static string InitializeErrorMessage
            = "SOMETHING WENT WRONG WHILE INITLIZING";
        public static string ClosingMessage
            = "programs finished -> quitting now";
        #endregion

        #region Values
        public static sbyte DriveForwardSpeed = 50;
        public static int LcdErrorDuration = 15000;
        public static int ProgramBootTime = 10000;
        public static int SamplingRate = 250;
        public static sbyte RotationSpeed = 75;
        public static int RotationDuration = 1000;
        #endregion
    }
}
