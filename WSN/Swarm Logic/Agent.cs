using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Swarm_Logic
{
    public class Agent
    {
        const double W = 0.3;
        const double P = 0.4;
        const double G = 0.3;
        const double MaxVelocity = 20.0;
        //const double MaxAcceleration=1.0;

        static Random r = new Random();

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

        public PositionFunction RadiationFunction;
        public SendMessageFunction Send;

        public Agent(double PX, double PY, double VX, double VY, PositionFunction RadiationFunction, SendMessageFunction Send)
        {
            this.PX = PX;
            this.PY = PY;

            this.VX = VX;
            this.VY = VY;

            this.MyBestX = PX;
            this.MyBestY = PY;
            this.MyBestValue = RadiationFunction(PX, PY);

            this.RadiationFunction = RadiationFunction;
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
            VX = W * VX + r.NextDouble() * P * (MyBestX - PX) + r.NextDouble() * G * (OthersBestX - PX);
            VY = W * VY + r.NextDouble() * P * (MyBestY - PY) + r.NextDouble() * G * (OthersBestY - PY);

            double V = Math.Sqrt(VX * VX + VY * VY);
            VX = VX / V;
            VY = VY / V;

            V = Math.Min(V, MaxVelocity);
            VX *= V;
            VY *= V;
        }

        public void AfterMoving()
        {
            double CurrentRadiation = RadiationFunction(PX, PY);
            if (CurrentRadiation > MyBestValue)
            {
                MyBestX = PX;
                MyBestY = PY;
                MyBestValue = CurrentRadiation;
                Send(this, new AgentMessage(PX, PY, MyBestValue));
            }
        }
    }
}
