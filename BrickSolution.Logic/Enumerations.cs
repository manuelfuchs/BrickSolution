namespace BrickSolution.Logic.Enumerations
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
        /// robot stopped due to detecting the tree in the front
        /// </summary>
        TreeDetected,
        /// <summary>
        /// robot stopped due to detecting the fence in the front
        /// </summary>
        FenceDetected,
        /// <summary>
        /// robot stopped due to detecting our single food piece in the front
        /// </summary>
        OurFoodDetected,
        /// <summary>
        /// robot stopped due to detecting enemies food piece in the front
        /// </summary>
        EnemyFoodDetected,
        /// <summary>
        /// robot stopped due to detecting a enclosure in front
        /// </summary>
        MeadowDetected
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
        OtherMode,
        /// <summary>
        /// tells the rotation method to rotate the robot half
        /// </summary>
        HalfRotationMode
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
