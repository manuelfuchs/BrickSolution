using MonoBrickFirmware.Movement;
using MonoBrickFirmware.Sensors;
using System;

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
        public static readonly sbyte DRIVE_FORWARD_SPEED = -30;
        public static readonly sbyte DRIVE_BACKWARD_SPEED = 30;
        public static readonly sbyte DRIVE_FORWARD_AFT_BACKWARD_SPEED = -30;
        public static readonly sbyte GRAPPLER_RISER_SPEED = -40;
        public static readonly sbyte GRAPPLER_WHEEL_SPEED = 25;
        public static readonly int GRAPPLER_WHEEL_BOUNDARY = 210;
        public static readonly sbyte ROTATION_SPEED_FORWARD = -40;
        public static readonly sbyte ROTATION_SPEED_BACKWARD = 40;
        public static readonly int GRAPPLER_RISER_TACHO_BOUNDARY = 100;
        public static readonly int PROGRAM_ABORTION_DELAY = 10000;
        public static readonly int PROGRAM_BOOT_DELAY = 10000;
        public static readonly int SAMPLING_RATE = 250;
        public static readonly int ROTATION_DURATION = 1000;
        public static readonly int IR_VALUES_NOT_INTERPRETABLE_VALUE = -1;
        public static readonly int ULTRA_SONIC_TABLE_END_VALUE = 820;
        public static readonly int ULTRA_SONIC_REFLECTION_TOLL = 900;
        public static readonly int IR_TABLE_END_VALUE = 45;
        public static readonly double COLOUR_TOLERANCE = 0.4;
        public static readonly int INTENSITY_TOLERANCE = 10;
        public static readonly int US_MEADOW_DISTANCE_TOLL_DOWN = 90;
        public static readonly int US_MEADOW_DISTANCE_TOLL_UP = 101;
        public static readonly int IR_MEADOW_DISTANCE_TOLL_DOWN = 4;
        public static readonly int IR_MEADOW_DISTANCE_TOLL_UP = 9;
        public static readonly int US_FENCE_DISTANCE_TOLL_DOWN = 102;
        public static readonly int US_FENCE_DISTANCE_TOLL_UP = 110;
        public static readonly int IR_FENCE_DISTANCE_TOLL_DOWN = 10;
        public static readonly int IR_FENCE_DISTANCE_TOLL_UP = 13;
        #endregion

        #region TeamValues
        public static FullColor WINNIE_TEAM_FOODSTONE_COLOR = new FullColor()
        {
            Intensity = -1,
            RGBColor = new RGBColor(173, 115, 21)
        };
        public static readonly FullColor WINNIE_TEAM_MEADOW_COLOR = new FullColor()
        {
            Intensity = -1,
            RGBColor = new RGBColor(17, 43, 52)
        };
        public static readonly FullColor IAH_TEAM_FOODSTONE_COLOR = new FullColor()
        {
            Intensity = -1,
            RGBColor = new RGBColor(24, 104, 25)
        };
        public static readonly FullColor IAH_TEAM_MEADOW_COLOR = new FullColor()
        {
            Intensity = -1,
            RGBColor = new RGBColor(158, 106, 17)
        };
        public static readonly FullColor TREE_COLOR = new FullColor()
        {
            Intensity = -1,
            RGBColor = new RGBColor(0, 0, 0)
        };
        #endregion
    }
}
