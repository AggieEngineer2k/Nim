namespace Nim.Blazor.Data
{
    /// <summary>
    /// All possible game states.
    /// </summary>
    public enum NimState
    {
        /// <summary>
        /// The user can enter the desired starting heap sizes.
        /// </summary>
        Setup = 0,

        /// <summary>
        /// The game is being played.
        /// </summary>
        InProgress = 1,

        /// <summary>
        /// The game is finished.
        /// </summary>
        Done = 2
    }
}
