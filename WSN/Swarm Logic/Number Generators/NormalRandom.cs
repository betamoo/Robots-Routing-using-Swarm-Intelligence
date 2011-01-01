using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Swarm_Logic.Number_Generators
{

    /// <summary>
    /// This class implements a normal random generator.
    /// </summary>
    public class NormalRandom : NumberGenerator
    {
        double Mean;
        double Variance;
        double StandardDeviation;

        Random UniformRandom1;
        Random UniformRandom2;

        public NormalRandom(double Mean, double Variance)
        {
            this.Mean = Mean;
            this.Variance = Variance;
            this.StandardDeviation = Math.Sqrt(Variance);
            UniformRandom1 = new Random();
            UniformRandom2 = new Random();
        }

        public NormalRandom(double Mean, double Variance, int Seed1, int Seed2)
        {
            this.Mean = Mean;
            this.Variance = Variance;
            this.StandardDeviation = Math.Sqrt(Variance);
            UniformRandom1 = new Random(Seed1);
            UniformRandom2 = new Random(Seed2);
        }

        public double NextDouble()
        {
            return StandardDeviation * Math.Sqrt(-2 * Math.Log(UniformRandom1.NextDouble())) * Math.Cos(2 * Math.PI * UniformRandom2.NextDouble()) + Mean;
        }
    }
}
