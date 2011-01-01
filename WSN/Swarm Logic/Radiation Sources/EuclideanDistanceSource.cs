﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Swarm_Logic.Radiation_Sources
{
    public class EuclideanDistanceSource : RadiationSource
    {
        const double NearDistance = 5.0;

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
            return Math.Sqrt((SourceX - PX) * (SourceX - PX) + (SourceY - PY) * (SourceY - PY)) <= NearDistance;
        }
    }
}