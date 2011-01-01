using System;
using System.Collections.Generic;
using System.Threading;
using System.Text;

namespace Swarm_Logic
{
    public class Environment
    {
        public delegate void VoidFunction();

        const double ReceiveRange = 50;

        public RadiationSource Source;

        public List<Agent> Agents;
        public Barrier[] Barriers;

        private bool WillTheAgentReceiveTheMessage(Agent SendingAgent, Agent ReceivingAgent)
        {
            return (SendingAgent != ReceivingAgent) &&
                (SendingAgent.PX - ReceivingAgent.PX) * (SendingAgent.PX - ReceivingAgent.PX) + (SendingAgent.PY - ReceivingAgent.PY) * (SendingAgent.PY - ReceivingAgent.PY) <= ReceiveRange * ReceiveRange;
        }

        public void Update()
        {
            foreach (Agent agent in Agents)
            {
                int Try = 3;
                agent.TakeDecision();
                double startX = agent.PX;
                double startY = agent.PY;
                double endX = agent.PX + agent.VX;
                double endY = agent.PY + agent.VY;
                for (int i = 0; i < Barriers.Length; i++)
                {
                    if (Barriers[i].IsIntersected(startX, startY, endX, endY))
                    {
                        if (Try < 0)
                        {
                            endX = agent.PX;
                            endY = agent.PY;
                            agent.VX = 0;
                            agent.VY = 0;
                        }
                        else
                        {
                            Try--;
                            agent.TakeRandomDecision();
                            endX = agent.PX + agent.VX;
                            endY = agent.PY + agent.VY;
                            i = -1;
                        }
                    }
                }
                agent.PX = endX;
                agent.PY = endY;

                if (Source.IsNearASource(agent.PX, agent.PY))
                    agent.FoundSource = true;

                agent.AfterMoving();
            }
        }

        private void Send(Agent SendingAgent, AgentMessage Message)
        {
            foreach (Agent a in Agents)
            {
                if (WillTheAgentReceiveTheMessage(SendingAgent, a))
                {
                    a.Receive(Message);
                }
            }
        }

        public Environment(int NumberOfAgents, double BoundaryX, double BoundaryY, List<Barrier> Barriers, RadiationSource Source)
        {
            NumberGenerator PXRandomGenerator = new UniformRandom(0.0, BoundaryX, (int)(DateTime.Now.Ticks));
            NumberGenerator PYRandomGenerator = new UniformRandom(0.0, BoundaryY, (int)(DateTime.Now.Ticks + 1));
            NumberGenerator VXRandomGenerator = new UniformRandom(-BoundaryX, BoundaryX, (int)(DateTime.Now.Ticks + 2));
            NumberGenerator VYRandomGenerator = new UniformRandom(-BoundaryY, BoundaryY, (int)(DateTime.Now.Ticks + 3));

            this.Source = Source;
            this.Barriers = Barriers.ToArray();
            Agents = new List<Agent>(NumberOfAgents);
            for (int i = 0; i < NumberOfAgents; i++)
            {
                Agents.Add(new Agent(
                    PXRandomGenerator.NextDouble(),
                    PYRandomGenerator.NextDouble(),
                    VXRandomGenerator.NextDouble(),
                    VYRandomGenerator.NextDouble(),
                    Source.GetRadiation,
                    Send
                    ));
            }
        }


        public Environment(List<Agent> Agents, double BoundaryX, double BoundaryY, List<Barrier> Barriers, RadiationSource Source)
        {
            NumberGenerator PXRandomGenerator = new UniformRandom(0.0, BoundaryX, (int)(DateTime.Now.Ticks));
            NumberGenerator PYRandomGenerator = new UniformRandom(0.0, BoundaryY, (int)(DateTime.Now.Ticks + 1));
            NumberGenerator VXRandomGenerator = new UniformRandom(-BoundaryX, BoundaryX, (int)(DateTime.Now.Ticks + 2));
            NumberGenerator VYRandomGenerator = new UniformRandom(-BoundaryY, BoundaryY, (int)(DateTime.Now.Ticks + 3));

            this.Source = Source;
            this.Barriers = Barriers.ToArray();
            this.Agents = Agents;
            foreach (Agent a in Agents)
            {
                a.RadiationFunction = Source.GetRadiation;
                a.MyBestValue = a.RadiationFunction(a.PX,a.PY);
                a.Send = Send;
            }
        }

        public event VoidFunction OnIterationEnd;

        public void Run(int NumberOfIterations)
        {
            for (int i = 0; i < NumberOfIterations; i++)
            {
                Update();
                if (OnIterationEnd != null)
                    OnIterationEnd();
                Thread.Sleep(20);
            }
        }

    }
}
