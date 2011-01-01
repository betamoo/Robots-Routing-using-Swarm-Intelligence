
namespace Swarm_Logic
{
    /// <summary>
    /// A static class that contains various parameters for Particle Swarm Optimization algorithm,
    /// communication range, minimum and maximum velocities.
    /// </summary>
    public static class GeneralParameters
    {
        // Represents the ratio of the agent's current velocity that affects the next velocity decesion.
        public static double W = 0.3925;

        // Represents the ratio of the agent's distance to the best encountered position that affects the next velocity decesion.
        public static double P = 2.5586;

        // Represents the ratio of the agent's distance to the best known position that affects the next velocity decesion.
        public static double G = 1.3358;

        // Represents the range in which agents can communicate with each other.
        public static double ReceiveRange = 50.0;

        // Represents the maximum velocity for every agent.
        public static double MaxVelocity = 5.0;

        // Represents the minimum velocity for every agent.
        public static double MinVelocity = 2.0;

        // An agent is considered reached a radiation source, if it is at distance NearDistance or less to the radiation source.
        public static double NearDistance = 5.0;
    }
}
