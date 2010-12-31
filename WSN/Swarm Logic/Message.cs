using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Swarm_Logic
{
    public delegate void SendMessageFunction(Agent SendingAgent, AgentMessage Message);

    public struct AgentMessage
    {
        public double PX;
        public double PY;
        public double Value;

        public AgentMessage(double PX, double PY, double Value)
        {
            this.PX = PX;
            this.PY = PY;
            this.Value = Value;
        }
    }
}