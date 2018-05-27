﻿namespace BrickSolution.Logic.Enumerations
{
    /// <summary>
    /// describes the position of the grappler
    /// </summary>
    public enum GrapplerPosition
    {
        /// <summary>
        /// the grappler is not on the ground
        /// </summary>
        Up,
        /// <summary>
        /// the grappler is on the ground
        /// </summary>
        Down
    }

    /// <summary>
    /// describes if the robot is carrying food or searching currently
    /// </summary>
    public enum FoodState
    {
        /// <summary>
        /// the robot is currently carrying food
        /// </summary>
        Carrying,
        /// <summary>
        /// the robot is currently searching for food
        /// </summary>
        Searching
    }

    /// <summary>
    /// describes why the robot stopped
    /// </summary>
    public enum StopReason
    {
        /// <summary>
        /// robot stopped due to detecting a abyss in the front
        /// </summary>
        AbyssDetected,
        /// <summary>
        /// robot stopped due to detecting a obstacle in the front
        /// </summary>
        ObstacleDetected,
        /// <summary>
        /// robot stopped due to detecting a foodplace in the front
        /// </summary>
        FoodplaceDetected,
        /// <summary>
        /// robot stopped due to detecting a single food piece in the front
        /// </summary>
        SingleFoodDetected,
        /// <summary>
        /// robot stopped due to detecting a enclosure in front
        /// </summary>
        EnclosureDetected
    }

    public enum RotationMode
    {
        /// <summary>
        /// uses the constant data in the constants file to instruct
        /// the rotation
        /// </summary>
        TimerMode,
        /// <summary>
        /// used to evade the abyss, a enemy robot or different animals
        /// </summary>
        OtherMode
    }

    public enum TeamMode
    {
        /// <summary>
        /// describes what Team the Robot is and which colors to react to
        /// food: green, meadow: 
        /// </summary>
       WinnieTeam,
        /// <summary>
        /// describes what Team the Robot is and which colors to react to
        /// food: yellow, meadow: 
        /// </summary>
        IAhTeam
    }
}
