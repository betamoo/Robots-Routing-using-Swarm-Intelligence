﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Swarm_Logic.Radiation_Sources
{
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
}