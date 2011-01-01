using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Swarm_Logic
{
    public delegate double PositionFunction(double PX, double PY);

    /// <summary>
    /// An interface for the classes that model the intenistiy of radiation sources.
    /// It must contain a function that takes the coordinates of a position and returns the quantity of radiation as measured by a sensor at that position
    /// </summary>
    public interface RadiationSource
    {
        double GetRadiation(double PX, double PY);
        bool IsNearASource(double PX, double PY);
    }

}
