using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Swarm_Logic;

namespace UIDemo
{
    public partial class Form1 : Form
    {


        public static int SrcX = 50;
        public static int SrcY = 50;

        public static int MaxX = 100;
        public static int MaxY = 100;


        public Form1()
        {
            br = new Barrier[4];
            br[0] = new Barrier(0,0,MaxX,0);
            br[1] = new Barrier(MaxX, 0, MaxX, MaxY);
            br[2] = new Barrier(MaxX, MaxY, 0, MaxY);
            br[3] = new Barrier(0, MaxY, 0, 0);
            InitializeComponent();
            rs = new GaussianFunctionSource(SrcX,SrcY);
            env = new Swarm_Logic.Environment(100, 100, 100, br, rs);
        }

        private System.Drawing.Graphics g;
        private System.Drawing.Pen pen1 = new System.Drawing.Pen(Color.Black, 2F);
        private System.Drawing.Pen pen2 = new System.Drawing.Pen(Color.Red, 2F);


        Swarm_Logic.Environment env;
        GaussianFunctionSource rs;
        Barrier[] br;

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
            g.DrawRectangle(pen2, new Rectangle(new Point(SrcX,SrcY), new Size(5, 5)));
        }

        void drawBarr()
        {
            foreach (Barrier b in br)
            {
                g.DrawLine(pen1, new Point((int)b.X1, (int)b.Y1), new Point((int)b.X2,(int)b.Y2));
            }
        }

    }
}
