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
            // To be check later..
            double A1 = Y2 - Y1;
            double B1 = X1 - X2;
            double C1 = A1 * X1 + B1 * Y1;

            double A2 = endY - startY;
            double B2 = startX - endX;
            double C2 = A2 * startX + B2 * startY;

            double det = A1 * B2 - A2 * B1;
            if (det == 0)
                return false;// Parrallel lines
            else
            {
                double x = (B2 * C1 - B1 * C2) / det;
                double y = (A1 * C2 - A2 * C1) / det;

                // Now we must check if (x,y) are on the 2 lines
                if (A1 * x + B1 * y - C1 <= double.Epsilon && A2 * x + B2 * y - C2 <= double.Epsilon)
                    return true;
                else
                    return false;
            }
        }
    }
}
