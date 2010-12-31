using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Swarm_Logic
{
    public class Environment
    {
        public delegate void VoidFunction();

        const double ReceiveRange = 30.0;

        public RadiationSource Source;

        public Agent[] Agents;
        public Barrier[] Barriers;

        private bool WillTheAgentReceiveTheMessage(Agent SendingAgent,Agent ReceivingAgent)
        {
            return (SendingAgent != ReceivingAgent) && 
                (SendingAgent.PX - ReceivingAgent.PX) * (SendingAgent.PX - ReceivingAgent.PX) + (SendingAgent.PY - ReceivingAgent.PY) * (SendingAgent.PY - ReceivingAgent.PY) <= ReceiveRange * ReceiveRange;
        }

        public void Update()
        {
            foreach (Agent agent in Agents)
            {
                agent.TakeDecision();
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
                agent.AfterMoving();
            }
        }

        private void Send(Agent SendingAgent, AgentMessage Message)
        {
            for (int i = 0; i < Agents.Length; i++)
            {
                if (WillTheAgentReceiveTheMessage(SendingAgent,Agents[i]))
                {
                    Agents[i].Receive(Message);
                }
            }
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

        public event VoidFunction OnIterationEnd;

        public void Run(int NumberOfIterations)
        {
            for (int i = 0; i < NumberOfIterations; i++)
            {
                Update();
                if (OnIterationEnd!=null)
                    OnIterationEnd();
            }
        }

    }
}
