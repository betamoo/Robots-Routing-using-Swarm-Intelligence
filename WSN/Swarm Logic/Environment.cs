using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Swarm_Logic
{
    public class Environment
    {
        const double ReceiveRange = 5.0;

        public RadiationSource Source;

        public Agent[] Agents;
        public Barrier[] Barriers;

        private bool WillTheMessageArrive(double X1, double Y1, double X2, double Y2)
        {
            return (X1 - X2) * (X1 - X2) + (Y1 - Y2) * (Y1 - Y2) <= ReceiveRange * ReceiveRange;
        }

        public Environment(int NumberOfAgents, double BoundaryX, double BoundaryY, Barrier[] Barriers, RadiationSource Source)
        {
            NumberGenerator PXRandomGenerator = new UniformRandom(0.0, BoundaryX, (int)(DateTime.Now.Ticks));
            NumberGenerator PYRandomGenerator = new UniformRandom(0.0, BoundaryY, (int)(DateTime.Now.Ticks + 1));
            NumberGenerator VXRandomGenerator = new UniformRandom(-BoundaryX, BoundaryX, (int)(DateTime.Now.Ticks + 2));
            NumberGenerator VYRandomGenerator = new UniformRandom(-BoundaryY, BoundaryY, (int)(DateTime.Now.Ticks + 3));

            this.Source = Source;
            this.Barriers = Barriers;
            Agents = new Agent[NumberOfAgents];
            for (int i = 0; i < Agents.Length; i++)
            {
                Agents[i] = new Agent(
                    PXRandomGenerator.NextDouble(),
                    PYRandomGenerator.NextDouble(),
                    VXRandomGenerator.NextDouble(),
                    VYRandomGenerator.NextDouble(),
                    Source.GetRadiation,
                    Send
                    );
            }
        }

        public void Update()
        {
            foreach (Agent agent in Agents)
            {
                double startX = agent.PX;
                double startY = agent.PY;
                double endX = agent.PX + agent.VX;
                double endY = agent.PY + agent.VY;
                foreach (Barrier barrier in Barriers)
                {
                    if (barrier.IsIntersected(startX, startY, endX, endY))
                    {
                        endX = agent.PX;
                        endY = agent.PY;
                        agent.VX = 0;
                        agent.VY = 0;
                        break;
                    }
                }
                agent.PX = endX;
                agent.PY = endY;
            }
        }

        public void Send(Agent SendingAgent, AgentMessage Message)
        {
            for (int i = 0; i < Agents.Length; i++)
            {
                if (SendingAgent != Agents[i] && WillTheMessageArrive(SendingAgent.PX, SendingAgent.PY, Agents[i].PX, Agents[i].PY))
                {
                    Agents[i].Receive(Message);
                }
            }
        }

    }
}
