
namespace Swarm_Logic
{
    /// <summary>
    /// A struct that represents the messages sent between different agents.
    /// It contains position coordinates and value of the radiation intensity at that position.
    /// </summary>
    public struct AgentMessage
    {
        // Represents the X coordinate of the sent message.
        public double PX;
        
        // Represents the Y coordinate of the sent message.
        public double PY;

        // Represents radiation intensity at the position (PX,PY).
        public double Value;
        
        /// <summary>
        /// Creates an new agent message with the specified coordinates and radiation intensity value.
        /// </summary>
        /// <param name="PX">Represents the X coordinate of the sent message.</param>
        /// <param name="PY">Represents the Y coordinate of the sent message.</param>
        /// <param name="Value">Represents radiation intensity at the position (PX,PY).</param>
        public AgentMessage(double PX, double PY, double Value)
        {
            this.PX = PX;
            this.PY = PY;
            this.Value = Value;
        }
    }
}