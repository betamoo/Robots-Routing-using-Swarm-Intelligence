using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Swarm_Logic
{
    public class GaussianFunctionSource : RadiationSource
    {

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
            return Math.Sqrt((SourceX - PX) * (SourceX - PX) + (SourceY - PY) * (SourceY - PY)) <= GeneralParameters.NearDistance;
        }
    }
}
