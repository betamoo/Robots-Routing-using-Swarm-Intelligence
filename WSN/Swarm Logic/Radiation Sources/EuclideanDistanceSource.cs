using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Swarm_Logic
{
    public class EuclideanDistanceSource : RadiationSource
    {
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
            return Math.Sqrt((SourceX - PX) * (SourceX - PX) + (SourceY - PY) * (SourceY - PY)) <= GeneralParameters.NearDistance;
        }
    }
}
