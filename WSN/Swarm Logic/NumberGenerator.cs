using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Swarm_Logic
{
    public interface NumberGenerator
    {
        double NextDouble();
    }
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
