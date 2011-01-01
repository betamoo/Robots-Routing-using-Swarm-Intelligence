using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Swarm_Logic
{
    public delegate double PositionFunction(double PX, double PY);

    /// <summary>
    /// An interface for the classes that model the intenistiy of radiation sources.
    /// It must contain a function that takes the coordinates of a position and returns the intenistiy of radiation as measured by a sensor at that position.
    /// It must also contain a function that takes the coordinates of a position and returns true if the position is near radiation source and returns false otherwise.
    /// </summary>
    public interface RadiationSource
    {
        /// <summary>
        /// This function takes the coordinates of an agent position and returns the intenistiy of radiation as measured by a sensor at that position.
        /// </summary>
        /// <param name="PX">Represents X coordinate of the agent.</param>
        /// <param name="PY">Represents Y coordinate of the agent.</param>
        /// <returns>The intenistiy of radiation as measured by the agent sensor at that position.</returns>
        double GetRadiation(double PX, double PY);

        /// <summary>
        /// This function takes the coordinates of an agent position and returns true if the position is near radiation source and returns false otherwise.
        /// </summary>
        /// <param name="PX">Represents X coordinate of the agent.</param>
        /// <param name="PY">Represents Y coordinate of the agent.</param>
        /// <returns></returns>
        bool IsNearASource(double PX, double PY);
    }

}
