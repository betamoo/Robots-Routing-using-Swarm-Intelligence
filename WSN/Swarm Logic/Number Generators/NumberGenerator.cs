using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Swarm_Logic
{
    /// <summary>
    /// An interface for the classes that implements random number generators.
    /// It must contain a function that returns the next random double number.
    /// </summary>
    public interface NumberGenerator
    {
        double NextDouble();
    }

}
