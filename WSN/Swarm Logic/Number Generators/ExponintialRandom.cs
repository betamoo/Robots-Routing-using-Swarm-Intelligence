using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Swarm_Logic.Number_Generators
{
    /// <summary>
    /// This class implements an exponintial random generator.
    /// </summary>
    public class ExponintialRandom : NumberGenerator
    {
        double Mean;

        Random UniformRandom;

        public ExponintialRandom(double Mean)
        {
            this.Mean = Mean;
            UniformRandom = new Random();
        }

        public ExponintialRandom(double Mean, int Seed)
        {
            this.Mean = Mean;
            UniformRandom = new Random(Seed);
        }

        public double NextDouble()
        {
            return -Math.Log(UniformRandom.NextDouble()) * Mean;
        }
    }

}
