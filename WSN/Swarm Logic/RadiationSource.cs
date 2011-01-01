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

    public class EuclideanDistanceSource:RadiationSource
    {
        const double NearDistance= 5.0;

        double SourceX { get; set; }
        double SourceY { get; set; }

        public EuclideanDistanceSource(double SourceX, double SourceY)
        {
            this.SourceX = SourceX;
            this.SourceY = SourceY;
        }
        public double GetRadiation(double PX, double PY)
        {
            return Math.Sqrt((SourceX - PX) * (SourceX - PX) + (SourceY - PY) * (SourceY - PY));
        }

        public bool IsNearASource(double PX, double PY)
        {
            return Math.Sqrt((SourceX - PX) * (SourceX - PX) + (SourceY - PY) * (SourceY - PY))<=NearDistance;
        }
    }

    public class GaussianFunctionSource : RadiationSource
    {
        const double NearDistance = 5.0;

        double SourceX { get; set; }
        double SourceY { get; set; }
        double Source1B { set; get; }

        public GaussianFunctionSource(double SourceX, double SourceY, double Source1B)
        {
            this.SourceX = SourceX;
            this.SourceY = SourceY;
            this.Source1B = Source1B;
        }
        public double GetRadiation(double PX, double PY)
        {
            return Math.Exp(-0.5 * ((SourceX - PX) * (SourceX - PX) / Source1B + (SourceY - PY) * (SourceY - PY) / Source1B));
        }
        public bool IsNearASource(double PX, double PY)
        {
            return Math.Sqrt((SourceX - PX) * (SourceX - PX) + (SourceY - PY) * (SourceY - PY)) <= NearDistance;
        }
    }

    public class DoubleGaussianFunctionSources : RadiationSource
    {
        const double NearDistance = 5.0;

        double Source1X { set; get; }
        double Source1Y { set; get; }
        double Source1A { set; get; }
        double Source1B { set; get; }
        double Source1C { set; get; }

        double Source2X { set; get; }
        double Source2Y { set; get; }
        double Source2A { set; get; }
        double Source2B { set; get; }
        double Source2C { set; get; }

        public DoubleGaussianFunctionSources(double Source1X, double Source1Y, double Source1A, double Source1B, double Source1C, double Source2X, double Source2Y, double Source2A, double Source2B, double Source2C)
        {
            this.Source1X = Source1X;
            this.Source1Y = Source1Y;
            this.Source1A = Source1A;
            this.Source1B = Source1B;
            this.Source1C = Source1C;

            this.Source2X = Source2X;
            this.Source2Y = Source2Y;
            this.Source2A = Source2A;
            this.Source2B = Source2B;
            this.Source2C = Source2C;
        }

        public double GetRadiation(double PX, double PY)
        {
            double Source1Magnitude = Source1A * Math.Exp(-0.5 * ((Source1X - PX) * (Source1X - PX) / Source1B + (Source1Y - PY) * (Source1Y - PY) / Source1C));
            double Source2Magnitude = Source2A * Math.Exp(-0.5 * ((Source2X - PX) * (Source2X - PX) / Source2B + (Source2Y - PY) * (Source2Y - PY) / Source2C));
            return Source1Magnitude + Source2Magnitude;
        }

        public bool IsNearASource(double PX, double PY)
        {
            return Math.Sqrt((Source1X - PX) * (Source1X - PX) + (Source1Y - PY) * (Source1Y - PY)) <= NearDistance ||
                Math.Sqrt((Source2X - PX) * (Source2X - PX) + (Source2Y - PY) * (Source2Y - PY)) <= NearDistance;
        }
    }

    public class MultipleGaussianFunctionSources : RadiationSource
    {
        const double NearDistance = 5.0;

        double[] SourceXs;
        double[] SourceYs;
        double[] SourceAs;
        double[] SourceBs;

        public MultipleGaussianFunctionSources(double[] SourceXs,double[] SourceYs,double[] SourceAs,double[] SourceBs)
        {
            this.SourceXs = SourceXs;
            this.SourceYs = SourceYs;
            this.SourceAs = SourceAs;
            this.SourceBs = SourceBs;
        }

        public double GetRadiation(double PX, double PY)
        {
            double Sum = 0;
            for (int i = 0; i < SourceXs.Length; i++)
            {
                double Magnitude = SourceAs[i] * Math.Exp(-0.5 * ((SourceXs[i] - PX) * (SourceXs[i] - PX)  + (SourceYs[i] - PY) * (SourceYs[i] - PY)) / SourceBs[i]);
                Sum += Magnitude;
            }
            return Sum;
        }

        public bool IsNearASource(double PX, double PY)
        {
            for (int i = 0; i < SourceXs.Length; i++)
            {
                if (Math.Sqrt((SourceXs[i] - PX) * (SourceXs[i] - PX) + (SourceYs[i] - PY) * (SourceYs[i] - PY)) <= NearDistance)
                    return true;
            }
                return false;
        }

    }

    public class MultipleNoisyGaussianFunctionSources : RadiationSource
    {
        const double NearDistance = 5.0;

        NumberGenerator NoiseGenerator = new NormalRandom(0.0,1.0);

        double[] SourceXs;
        double[] SourceYs;
        double[] SourceAs;
        double[] SourceBs;

        public MultipleNoisyGaussianFunctionSources(double[] SourceXs, double[] SourceYs, double[] SourceAs, double[] SourceBs)
        {
            this.SourceXs = SourceXs;
            this.SourceYs = SourceYs;
            this.SourceAs = SourceAs;
            this.SourceBs = SourceBs;
        }

        public double GetRadiation(double PX, double PY)
        {
            double Sum = 0;
           
            for (int i = 0; i < SourceXs.Length; i++)
            {
                double RandomX = NoiseGenerator.NextDouble();
                double RandomY = NoiseGenerator.NextDouble();
                double Magnitude = SourceAs[i] * Math.Exp(-0.5 * ((SourceXs[i] - PX + RandomX) * (SourceXs[i] - PX + RandomX) + (SourceYs[i] - PY + RandomY) * (SourceYs[i] - PY + RandomY)) / SourceBs[i]);
                Sum += Magnitude;
            }
            return Sum;
        }

        public bool IsNearASource(double PX, double PY)
        {
            for (int i = 0; i < SourceXs.Length; i++)
            {
                if (Math.Sqrt((SourceXs[i] - PX) * (SourceXs[i] - PX) + (SourceYs[i] - PY) * (SourceYs[i] - PY)) <= NearDistance)
                    return true;
            }
            return false;
        }

    }
}
