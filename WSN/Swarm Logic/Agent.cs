﻿using System;
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
            VX = -(r.NextDouble()) * VX;
            VY = -(r.NextDouble()) * VY;
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
