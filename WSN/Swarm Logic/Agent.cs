using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Swarm_Logic
{
    public class Agent
    {

        static Random r = new Random();

        public bool FoundSource { get; set; }
        public bool Sending { get; set; }

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
            VX = (r.NextDouble() - 0.5) * 2 * GeneralParameters.MaxVelocity;
            VY = (r.NextDouble() - 0.5) * 2 * GeneralParameters.MaxVelocity;

            double V = Math.Sqrt(VX * VX + VY * VY);
            if (V == 0)
                V = 1.0;
            VX = VX / V;
            VY = VY / V;

            V = Math.Min(V, GeneralParameters.MaxVelocity);
            V = Math.Max(V, GeneralParameters.MinVelocity);
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

            V = Math.Min(V, GeneralParameters.MaxVelocity);
            V = Math.Max(V, GeneralParameters.MinVelocity);
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

            V = Math.Min(V, GeneralParameters.MaxVelocity);
            V = Math.Max(V, GeneralParameters.MinVelocity);
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

        public void CalculateNextAction()
        {
            if (FoundSource)
            {
                TakeTotalyRandomDecision();
            }
            else
            {
                VX = GeneralParameters.W * VX + r.NextDouble() * GeneralParameters.P * (MyBestX - PX) + r.NextDouble() * GeneralParameters.G * (OthersBestX - PX);
                VY = GeneralParameters.W * VY + r.NextDouble() * GeneralParameters.P * (MyBestY - PY) + r.NextDouble() * GeneralParameters.G * (OthersBestY - PY);

                double V = Math.Sqrt(VX * VX + VY * VY);
                if (V == 0)
                    V = 1.0;
                VX = VX / V;
                VY = VY / V;

                V = Math.Min(V, GeneralParameters.MaxVelocity);
                V = Math.Max(V, GeneralParameters.MinVelocity);
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


            VX = GeneralParameters.W * VX / temp0 + r.NextDouble() * GeneralParameters.P * (MyBestX - PX) / temp1 + r.NextDouble() * GeneralParameters.G * (OthersBestX - PX) / temp2;
            VY = GeneralParameters.W * VY / temp0 + r.NextDouble() * GeneralParameters.P * (MyBestY - PY) / temp1 + r.NextDouble() * GeneralParameters.G * (OthersBestY - PY) / temp2;


            double V = Math.Sqrt(VX * VX + VY * VY);
            if (V == 0)
                V = 1;

            VX = GeneralParameters.MaxVelocity * VX / V;
            VY = GeneralParameters.MaxVelocity * VY / V;
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
            bool WillAgentSend = FoundSource;
            if (!FoundSource)
            {
                double CurrentRadiation = RadiationFunction(PX, PY);
                if (CurrentRadiation > MyBestValue)
                {
                    MyBestX = PX;
                    MyBestY = PY;
                    MyBestValue = CurrentRadiation;
                    WillAgentSend = true;
                }
                if (MyBestValue > OthersBestValue)
                {
                    OthersBestValue = MyBestValue;
                    OthersBestX = MyBestX;
                    OthersBestY = MyBestY;
                    WillAgentSend = true;
                }
            }

            Sending = WillAgentSend;
            if (WillAgentSend)
            {
                Send(this, new AgentMessage(OthersBestX, OthersBestY, OthersBestValue));
            }


        }

        public delegate void SendMessageFunction(Agent SendingAgent, AgentMessage Message);

    }
}
