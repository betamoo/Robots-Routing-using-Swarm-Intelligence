using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Swarm_Logic.Radiation_Sources
{
    public class MultipleGaussianFunctionSources : RadiationSource
    {
        const double NearDistance = 5.0;

        double[] SourceXs;
        double[] SourceYs;
        double[] SourceAs;
        double[] SourceBs;

        public MultipleGaussianFunctionSources(double[] SourceXs, double[] SourceYs, double[] SourceAs, double[] SourceBs)
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
                double Magnitude = SourceAs[i] * Math.Exp(-0.5 * ((SourceXs[i] - PX) * (SourceXs[i] - PX) + (SourceYs[i] - PY) * (SourceYs[i] - PY)) / SourceBs[i]);
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
