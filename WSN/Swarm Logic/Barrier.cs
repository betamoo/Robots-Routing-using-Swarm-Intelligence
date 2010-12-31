using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Swarm_Logic
{
    public class Barrier
    {
        public double X1 { set; get; }
        public double Y1 { set; get; }
        public double X2 { set; get; }
        public double Y2 { set; get; }

        public bool IsIntersected(double startX, double startY, double endX, double endY)
        {
            return false;
        }
    }
}
