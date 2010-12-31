using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Swarm_Logic
{

    public class Environment
    {
        //public double SourceX { get; set; }
        //public double SourceY { get; set; }

        public RadiationSource Source;

        public Agent[] Agents;
        public Barrier[] Barriers;


        public void Initialize(int NumberOfAgents, double BoundaryX, double BoundaryY, Barrier[] Barriers, RadiationSource Source)
        {
            NumberGenerator PXRandomGenerator = new UniformRandom(0.0, BoundaryX);
            NumberGenerator PYRandomGenerator = new UniformRandom(0.0, BoundaryY);
            NumberGenerator VXRandomGenerator = new UniformRandom(-BoundaryX, BoundaryX);
            NumberGenerator VYRandomGenerator = new UniformRandom(-BoundaryY, BoundaryY);

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
                    Source.GetRadiation
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

    }
}
