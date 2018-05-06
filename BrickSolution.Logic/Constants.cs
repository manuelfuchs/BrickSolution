using MonoBrickFirmware.Movement;
using MonoBrickFirmware.Sensors;

namespace BrickSolution.Logic
{
    internal static class Constants
    {
        #region Motors
        public static MotorPort LEFT_TRACK_PORT = MotorPort.OutA;
        public static MotorPort RIGHT_TRACK_PORT = MotorPort.OutB;
        public static MotorPort GRAPPLER_PORT = MotorPort.OutD;
        #endregion

        #region Sensors
        public static SensorPort GRAPPLER_TOUCH_SENSOR_PORT = SensorPort.In1;
        public static SensorPort ULTRASONIC_SENSOR_PORT = SensorPort.In2;
        public static SensorPort IR_SENSOR_PORT = SensorPort.In3;
        public static SensorPort EV3_COLOR_SENSOR_PORT = SensorPort.In4;
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
        public static sbyte DRIVE_FORWARD_SPEED = 50;
        public static sbyte GRAPPLER_MOTOR_UP_SPEED = 40;
        public static sbyte GRAPPLER_MOTOR_DOWN_SPEED = -40;
        public static int GRAPPLER_UP_TO_DOWN_TACHO_BOUNDARY = -4500;
        public static int PROGRAM_ABORTION_DELAY = 15000;
        public static int PROGRAM_BOOT_DELAY = 10000;
        public static int SAMPLING_RATE = 250;
        public static sbyte ROTATION_SPEED = 40;
        public static int ROTATION_DURATION = 750;
        public static int IR_VALUES_NOT_INTERPRETABLE_VALUE = -1;
        public static int ULTRA_SONIC_TABLE_END_VALUE = 180;
        #endregion
    }
}
