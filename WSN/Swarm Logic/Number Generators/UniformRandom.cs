using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Swarm_Logic
{
    /// <summary>
    /// This class implements a uniform random generator.
    /// </summary>
    public class UniformRandom : NumberGenerator
    {
        double Min;
        double Max;

        Random MyUniformRandom;

        public UniformRandom(double Min, double Max)
        {
            this.Min = Min;
            this.Max = Max;
            MyUniformRandom = new Random();
        }

        public UniformRandom(double Min, double Max, int Seed)
        {
            this.Min = Min;
            this.Max = Max;
            MyUniformRandom = new Random(Seed);
        }

        public double NextDouble()
        {
            return MyUniformRandom.NextDouble() * (Max - Min) + Min;
        }
    }
}
