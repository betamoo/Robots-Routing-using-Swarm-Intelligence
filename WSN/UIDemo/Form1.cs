using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Swarm_Logic;

namespace UIDemo
{
    public partial class Form1 : Form
    {
        static double[] SourceXs = new double[] { 5,200,5};
        static double[] SourceYs = new double[] {5, 150,180};
        static double[] SourceAs = new double[] { 1,1 ,1};
        static double[] SourceBs = new double[] { 1000,1000 ,1000};


        public static int MaxX = 400;
        public static int MaxY = 400;


        public Form1()
        {
            br = new Swarm_Logic.Barrier[8];
            br[0] = new Swarm_Logic.Barrier(0, 0, MaxX, 0);
            br[1] = new Swarm_Logic.Barrier(MaxX, 0, MaxX, MaxY);
            br[2] = new Swarm_Logic.Barrier(MaxX, MaxY, 0, MaxY);
            br[3] = new Swarm_Logic.Barrier(0, MaxY, 0, 0);

            br[4] = new Swarm_Logic.Barrier(50, 100, 95, 50);
            br[5] = new Swarm_Logic.Barrier(200, 30, 211, 40);
            br[6] = new Swarm_Logic.Barrier(150, 120, 100, 50);
            br[7] = new Swarm_Logic.Barrier(250, 130, 200, 140);

            InitializeComponent();

            rs = new MultipleGaussianFunctionSources(SourceXs,SourceYs,SourceAs,SourceBs);
            
            env = new Swarm_Logic.Environment(25, MaxX, MaxY, br, rs);
            env.OnIterationEnd += RefreshMe;
        }

        private System.Drawing.Graphics g;
        private System.Drawing.Pen pen1 = new System.Drawing.Pen(Color.Black, 2F);
        private System.Drawing.Pen pen2 = new System.Drawing.Pen(Color.Red, 2F);


        Swarm_Logic.Environment env;
        RadiationSource rs;

        Swarm_Logic.Barrier[] br;

        bool b;

        List<Rectangle> rect = new List<Rectangle>();
      /*  
        private void tsm(void)
        {
            env.OnIterationEnd+=new Swarm_Logic.Environment.VoidFunction(env_OnIterationEnd);
        }
        /*/
        private void Run_Click(object sender, EventArgs e)
        {
            pictureBox1.Refresh();
            env.Run(1);
            drawAgents();
            drawSource();
            drawBarr();
        }


        void drawAgents()
        {
            List<Rectangle> rect = new List<Rectangle>();
            g = pictureBox1.CreateGraphics();

                foreach (Agent agent in env.Agents)
                {
                    g.DrawRectangle(pen1, new Rectangle(new Point((int)agent.PX, (int)agent.PY), new Size(5, 5)));
                }

        }

        void drawSource()
        {
            for (int i = 0; i < SourceXs.Length;i++ )
                g.DrawRectangle(pen2, new Rectangle(new Point((int)SourceXs[i], (int)SourceYs[i]), new Size(5, 5)));
        }

        void drawBarr()
        {
            foreach (Swarm_Logic.Barrier b in br)
            {
                g.DrawLine(pen1, new Point((int)b.X1, (int)b.Y1), new Point((int)b.X2,(int)b.Y2));
            }
        }

        void RefreshMe()
        {
            pictureBox1.Refresh();
            drawAgents();
            drawSource();
            drawBarr();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            env.Run(10000);
        }

    }
}
