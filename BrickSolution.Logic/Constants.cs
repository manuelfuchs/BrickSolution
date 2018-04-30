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
        public static SensorPort GrapplerTouchSensorPort = SensorPort.In1;
        public static SensorPort UltraSonicSensorPort = SensorPort.In2;
        public static SensorPort IRSensorPort = SensorPort.In3;
        public static SensorPort LegoColorSensorPort = SensorPort.In4;
        #endregion

        #region LcdOutputs
        public static string CompetitionStartUserMessagePart1
            = "Press the middle button to";
        public static string CompetitionStartUserMessagePart2
            = "continue with the competition";
        public static string InitializeErrorMessage
            = "SOMETHING WENT WRONG WHILE INITLIZING";
        public static string ClosingMessage
            = "programs finished -> quitting";
        #endregion

        #region Values
        public static sbyte DriveForwardSpeed = 50;
        public static sbyte GrapplerMotorUpSpeed = 40;
        public static sbyte GrapplerMotorDownSpeed = -40;
        public static int GrapplerUpToDownTachoBoundary = -4500;
        public static int LcdMessageDisplayDuration = 15000;
        public static int ProgramBootTime = 10000;
        public static int SamplingRate = 250;
        public static sbyte RotationSpeed = 40;
        public static int RotationDuration = 750;
        public static int IRValuesNotInterpretableIdenticator = -1;
        #endregion
    }
}
