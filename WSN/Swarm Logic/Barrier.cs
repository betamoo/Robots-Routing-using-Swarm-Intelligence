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

        private double m;
        private double c;
        public Barrier(double X1, double Y1,double X2 ,double Y2 )
        {
            this.X1 = X1;
            this.Y1 = Y1;
            this.X2 = X2;
            this.Y2 = Y2;

            if ((X2 - X1) != 0)
                m = (Y2 - Y1) / (X2 - X1);
            else
                m = 0;

            c = Y1-(m*X1);
        }

        public bool IsIntersected(double startX, double startY, double endX, double endY)
        {
            // To be check later..
          
          /*  double A1 = Y2 - Y1;
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

                if (
                    x>= Math.Min(startX,endX) && x<= Math.Max(startX,endX)&&
                    x>= Math.Min(X1,X2) && x<= Math.Max(X1,X2) &&
                    y>= Math.Min(startY,endY) && y<= Math.Max(startY,endY)&&
                    y>= Math.Min(Y1,Y2) && y<= Math.Max(Y1,Y2) 
                    )
                    return true;
                else
                    return false;
            }
           */

            if (m == 0)
            {
                if((Y1-startY)*(Y2-endY))>0)
                    return true;
            }

            if ((X1 - X2) == 0)
            {

            }
            else
            {
                if ((endY - (m * endX) - c) * (startY - (m * startX)) > 0)
                    return false;
                else
                    return true;
            }
        }
    }
}
