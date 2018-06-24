using MonoBrickFirmware.Movement;
using MonoBrickFirmware.Sensors;
using System;

namespace BrickSolution.Logic
{
    internal static class Constants
    {
        #region Motors
        public const MotorPort LEFT_TRACK_PORT = MotorPort.OutA;
        public const MotorPort RIGHT_TRACK_PORT = MotorPort.OutB;
        public const MotorPort GRAPPLER_RISER_PORT = MotorPort.OutC;
        public const MotorPort GRAPPLER_WHEEL_PORT = MotorPort.OutD;
        #endregion

        #region Sensors
        public const SensorPort EV3_COLOR_SENSOR_PORT = SensorPort.In1;
        public const SensorPort ULTRASONIC_SENSOR_PORT = SensorPort.In2;
        public const SensorPort IR_SENSOR_PORT = SensorPort.In4;
        #endregion

        #region LcdOutputs
        public const string COMPETITION_START_USER_MSG_PART1 = "Press the middle button to";
        public const string COMPETITION_START_USER_MSG_PART2  = "continue with the competition";
        public const string INIT_ERROR_MSG = "SOMETHING WENT WRONG WHILE INITLIZING";
        public const string PROGRAM_FINISHED_MSG = "programs finished -> quitting";
        #endregion

        #region Values
        public const sbyte DRIVE_FORWARD_SPEED = -30;
        public const sbyte DRIVE_BACKWARD_SPEED = 30;
        public const sbyte DRIVE_FORWARD_AFT_BACKWARD_SPEED = -30;
        public const sbyte GRAPPLER_RISER_SPEED = -40;
        public const sbyte GRAPPLER_WHEEL_SPEED = 25;
        public const int GRAPPLER_WHEEL_BOUNDARY = 210;
        public const sbyte ROTATION_SPEED_FORWARD = -40;
        public const sbyte ROTATION_SPEED_BACKWARD = 40;
        public const int GRAPPLER_RISER_TACHO_BOUNDARY = 100;
        public const int PROGRAM_ABORTION_DELAY = 15000;
        public const int PROGRAM_BOOT_DELAY = 10000;
        public const int SAMPLING_RATE = 250;
        public const int ROTATION_DURATION = 1000;
        public const int IR_VALUES_NOT_INTERPRETABLE_VALUE = -1;
        public const int ULTRA_SONIC_TABLE_END_VALUE = 820;
        public const int ULTRA_SONIC_REFLECTION_TOLL = 900;
        public const int IR_TABLE_END_VALUE = 20;
        public const double COLOUR_TOLERANCE = 0.4;
        public const int INTENSITY_TOLERANCE = 10;
        public const int US_MEADOW_DISTANCE_TOLL_DOWN = 55/*90*/;
        public const int US_MEADOW_DISTANCE_TOLL_UP = 74/*101*/;
        public const int IR_MEADOW_DISTANCE_TOLL_DOWN = 2;
        public const int IR_MEADOW_DISTANCE_TOLL_UP = 6;
        public const int US_FENCE_DISTANCE_TOLL_DOWN = 75/*102*/;
        public const int US_FENCE_DISTANCE_TOLL_UP = 95/*110*/;
        public const int IR_FENCE_DISTANCE_TOLL_DOWN = 7;
        public const int IR_FENCE_DISTANCE_TOLL_UP = 9;
        public const int STEP_DELAY = 200;
        #endregion

        #region TeamValues
        public static readonly FullColor WINNIE_TEAM_FOODSTONE_COLOR = new FullColor()
        //gelb
        {
            Intensity = 16,
            RGBColor = new RGBColor(150, 103, 18)
        };
        public static readonly FullColor WINNIE_TEAM_MEADOW_COLOR = new FullColor()
        //blau
        {
            Intensity = 7,
            RGBColor = new RGBColor(23, 63, 54)
        };
        public static readonly FullColor IAH_TEAM_FOODSTONE_COLOR = new FullColor()
        //grün
        {
            Intensity = 11,
            RGBColor = new RGBColor(8, 41, 9)
        };
        public static readonly FullColor IAH_TEAM_MEADOW_COLOR = new FullColor()
        //weiß
        {
            Intensity = 16,
            RGBColor = new RGBColor(90, 100, 73)
        };
        public static readonly FullColor TREE_COLOR = new FullColor()
        //braun
        {
            Intensity = 6,
            RGBColor = new RGBColor(25, 6, 10)
        };
        #endregion
    }
}
