using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Swarm_Logic
{
    public class Agent
    {
        public double PX { set; get; }
        public double PY { set; get; }

        public double VX { set; get; }
        public double VY { set; get; }

        public double BestX { set; get; }
        public double BestY { set; get; }
        public double BestValue { set; get; }

        //const double MaxVelocity=10.0;
        //const double MaxAcceleration=1.0;

        public PositionFunction RadiationFunction;

        public Agent(double PX,double PY,double VX,double VY,PositionFunction RadiationFunction)
        {
            this.PX = PX;
            this.PY = PY;
            this.RadiationFunction = RadiationFunction;
            this.BestX = PX;
            this.BestY = PY;
            this.BestValue = RadiationFunction(PX,PY);
            this.VX = VX;
            this.VY = VY;
        }

        public void ApplyVelocity()
        {
            PX += VX;
            PY += VY;
        }
    }
}
