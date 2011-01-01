using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Swarm_Logic
{
    public class Agent
    {
        const double W = -0.4349;
        const double P = -0.6504;
        const double G = 2.2073;
        const double MaxVelocity = 5.0;
        //const double MaxAcceleration=1.0;

        static Random r = new Random();

        public bool FoundSource { get; set; }

        public double PX { set; get; }
        public double PY { set; get; }

        public double VX { set; get; }
        public double VY { set; get; }

        public double MyBestX { set; get; }
        public double MyBestY { set; get; }
        public double MyBestValue { set; get; }

        public double OthersBestX { set; get; }
        public double OthersBestY { set; get; }
        public double OthersBestValue { set; get; }

        public PositionFunction RadiationFunction { set; get; }
        public SendMessageFunction Send{set;get;}

        private void TakeTotalyRandomDecision()
        {
            VX = (r.NextDouble() - 0.5) * 2 * MaxVelocity;
            VY = (r.NextDouble() - 0.5) * 2 * MaxVelocity;

            double V = Math.Sqrt(VX * VX + VY * VY);
            if (V == 0)
                V = 1.0;
            VX = VX / V;
            VY = VY / V;

            V = Math.Min(V, MaxVelocity);
            VX *= V;
            VY *= V;
        }
        private void TakeRandomPerpendicularDecision()
        {
            VX = -(r.NextDouble()) * VX;
            VY = -(r.NextDouble()) * VY;

            double V = Math.Sqrt(VX * VX + VY * VY);
            if (V == 0)
                V = 1.0;
            VX = VX / V;
            VY = VY / V;

            V = Math.Min(V, MaxVelocity);
            VX *= V;
            VY *= V;
        }

        private void TakeRandomTangentDecision()
        {
            // VX = -(r.NextDouble()) * VX;
            // VY = -(r.NextDouble()) * VY;

            double dx = VX - PX;
            double dy = VY - PY;

            double rdir = new Random().NextDouble();

            double theta = Math.Atan(dy / dx);

            if (rdir > 0.5)
                theta *= 1;

            double mag = Math.Sqrt((dx * dx) + (dy * dy));

            double dxn = -dy;
            double dyn = dx;

            VX = PX + (dxn * mag);
            VY = PY + (dyn * mag);

            double V = Math.Sqrt(VX * VX + VY * VY);
            if (V == 0)
                V = 1.0;
            VX = VX / V;
            VY = VY / V;

            V = Math.Min(V, MaxVelocity);
            VX *= V;
            VY *= V;

        }

        public Agent(double PX, double PY, double VX, double VY)
        {
            this.PX = PX;
            this.PY = PY;

            this.VX = VX;
            this.VY = VY;

            this.MyBestX = PX;
            this.MyBestY = PY;
            this.MyBestValue = 0;

        }
        public Agent(double PX, double PY, double VX, double VY, PositionFunction RadiationFunction, SendMessageFunction Send)
        {
            this.PX = PX;
            this.PY = PY;

            this.VX = VX;
            this.VY = VY;

            this.MyBestX = PX;
            this.MyBestY = PY;

            this.RadiationFunction = RadiationFunction;
            this.MyBestValue = RadiationFunction(PX, PY);
            this.Send = Send;
        }

        public void Receive(AgentMessage Message)
        {
            if (Message.Value >= OthersBestValue)
            {
                OthersBestValue = Message.Value;
                OthersBestX = Message.PX;
                OthersBestY = Message.PY;
            }
        }

        public void TakeDecision()
        {
            if (FoundSource)
            {
                TakeTotalyRandomDecision();
            }
            else
            {
                VX = W * VX + r.NextDouble() * P * (MyBestX - PX) + r.NextDouble() * G * (OthersBestX - PX);
                VY = W * VY + r.NextDouble() * P * (MyBestY - PY) + r.NextDouble() * G * (OthersBestY - PY);

                double V = Math.Sqrt(VX * VX + VY * VY);
                if (V == 0)
                    V = 1.0;
                VX = VX / V;
                VY = VY / V;

                V = Math.Min(V, MaxVelocity);
                VX *= V;
                VY *= V;
            }
        }

        public void TakeDecision2()
        {
            double temp0 = Math.Sqrt(VX * VX + VY * VY);
            double temp1 = Math.Sqrt((MyBestX - PX) * (MyBestX - PX) + (MyBestY - PY) * (MyBestY - PY));
            double temp2 = Math.Sqrt((OthersBestX - PX) * (OthersBestX - PX) + (OthersBestY - PY) * (OthersBestY - PY));

            if (temp0 == 0)
                temp0 = 1;
            if (temp1 == 0)
                temp1 = 1;
            if (temp1 == 0)
                temp1 = 1;


            VX = W * VX / temp0 + r.NextDouble() * P * (MyBestX - PX) / temp1 + r.NextDouble() * G * (OthersBestX - PX) / temp2;
            VY = W * VY / temp0 + r.NextDouble() * P * (MyBestY - PY) / temp1 + r.NextDouble() * G * (OthersBestY - PY) / temp2;


            double V = Math.Sqrt(VX * VX + VY * VY);
            if (V == 0)
                V = 1;

            VX = MaxVelocity * VX / V;
            VY = MaxVelocity * VY / V;
        }

        public void TakeRandomDecision()
        {
            if (r.NextDouble() < 0.5)
                TakeRandomPerpendicularDecision();
            else
                TakeRandomTangentDecision();
        }

        public void AfterMoving()
        {
            if (FoundSource)
            {
                Send(this, new AgentMessage(MyBestX, MyBestY, MyBestValue));
            }
            else
            {
                double CurrentRadiation = RadiationFunction(PX, PY);
                if (CurrentRadiation > MyBestValue)
                {
                    MyBestX = PX;
                    MyBestY = PY;
                    MyBestValue = CurrentRadiation;
                    Send(this, new AgentMessage(MyBestX, MyBestY, MyBestValue));
                }
            }
        }
    }
}
