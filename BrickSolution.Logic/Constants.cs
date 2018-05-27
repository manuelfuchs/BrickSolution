using MonoBrickFirmware.Movement;
using MonoBrickFirmware.Sensors;

namespace BrickSolution.Logic
{
    internal static class Constants
    {
        #region Motors
        public static readonly MotorPort LEFT_TRACK_PORT = MotorPort.OutA;
        public static readonly MotorPort RIGHT_TRACK_PORT = MotorPort.OutB;
        public static readonly MotorPort GRAPPLER_RISER_PORT = MotorPort.OutC;
        public static readonly MotorPort GRAPPLER_WHEEL_PORT = MotorPort.OutD;
        #endregion

        #region Sensors
        public static readonly SensorPort EV3_COLOR_SENSOR_PORT = SensorPort.In1;
        public static readonly SensorPort ULTRASONIC_SENSOR_PORT = SensorPort.In2;
        public static readonly SensorPort IR_SENSOR_PORT = SensorPort.In4;
        #endregion

        #region LcdOutputs
        public static string COMPETITION_START_USER_MSG_PART1
            = "Press the middle button to";
        public static string COMPETITION_START_USER_MSG_PART2
            = "continue with the competition";
        public static string INIT_ERROR_MSG
            = "SOMETHING WENT WRONG WHILE INITLIZING";
        public static string PROGRAM_FINISHED_MSG
            = "programs finished -> quitting";
        #endregion

        #region Values
        public static readonly sbyte DRIVE_FORWARD_SPEED = 50;
        public static readonly sbyte GRAPPLER_RISER_SPEED = 40;
        public static readonly sbyte GRAPPLER_WHEEL_SPEED = 20;
        public static readonly sbyte ROTATION_SPEED = 40;
        public static readonly int GRAPPLER_RISER_TACHO_BOUNDARY = 90;
        public static readonly int PROGRAM_ABORTION_DELAY = 15000;
        public static readonly int PROGRAM_BOOT_DELAY = 10000;
        public static readonly int SAMPLING_RATE = 250;
        public static readonly int ROTATION_DURATION = 750;
        public static readonly int IR_VALUES_NOT_INTERPRETABLE_VALUE = -1;
        public static readonly int ULTRA_SONIC_TABLE_END_VALUE = 180;
        public static readonly int IR_TABLE_END_VALUE = 20;
        public static readonly int GRAPPLER_WHEEL_BOUNDARY = 360;
        #endregion
    }
}
