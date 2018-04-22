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
    /// describes the state of the grappler
    /// </summary>
    public enum GrapplerState
    {
        /// <summary>
        /// the grappler is open
        /// </summary>
        Open,
        /// <summary>
        /// the grappler is closed
        /// </summary>
        Closed
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
}
