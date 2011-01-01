using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Swarm_Logic;

namespace Enviroment_GUI
{
    public partial class EnviromentGUI : Form
    {
        int AgentNo;
        int SourceX, SourceY;
        Point ps = new Point();
        Point pe = new Point();
        List<Barrier> B = new List<Barrier>();
        Barrier[] Barr;
        List<Agent> A = new List<Agent>();
        Agent[] Ag;
        RadiationSource R;
        NumberGenerator Number;
        NumberGenerator PosX;
        NumberGenerator PosY;
        Swarm_Logic.Environment env;


        public EnviromentGUI()
        {
            InitializeComponent();
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ps.X = e.X;
            ps.Y = e.Y;
            pe = ps;
            if (SourceButton.Focused == true)
            {
                Graphics g = panel1.CreateGraphics();
                Pen myPen = new Pen(System.Drawing.Color.Red, 7);
                Rectangle myRectangle = new Rectangle(e.X, e.Y, 7, 7);
                g.DrawEllipse(myPen, myRectangle);
                if (comboBox2.SelectedItem == "Euclidean Distance Source")
                {
                    R = new EuclideanDistanceSource(e.X, e.Y);
                    SourceX = e.X;
                    SourceY = e.Y;
                }
                //else if (comboBox2.SelectedItem == "Gaussian Function Source")
                //{
                //    R = new GaussianFunctionSource(e.X,e.Y,;
                //}
                //else if (comboBox2.SelectedItem == "Double Gaussian Function Sources")
                //{
                //    R = new EuclideanDistanceSource(e.X, e.Y);
                //}
                //else if (comboBox2.SelectedItem == "Multiple Gaussian Function Sources")
                //{
                //    R = new EuclideanDistanceSource(e.X, e.Y);
                //}

            }
        }

        private void BarrierButton_Click(object sender, EventArgs e)
        {

        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (BarrierButton.Focused == true)
            {
                if (e.Button == MouseButtons.Left)
                {
                    panel1 = (Panel)sender;
                    ControlPaint.DrawReversibleLine(panel1.PointToScreen(ps), panel1.PointToScreen(pe), Color.Black);
                    pe = new Point(e.X, e.Y);
                    ControlPaint.DrawReversibleLine(panel1.PointToScreen(ps), panel1.PointToScreen(pe), Color.Black);

                }
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            if (BarrierButton.Focused == true)
            {
                panel1 = (Panel)sender;
                Graphics g = panel1.CreateGraphics();
                Pen p = new Pen(Color.Blue, 2);
                ControlPaint.DrawReversibleLine(panel1.PointToScreen(ps), panel1.PointToScreen(pe), Color.Black);
                g.DrawLine(p, ps, pe);
                g.Dispose();
                B.Add(new Barrier(pe.X, pe.Y, ps.X, ps.Y));
            }
        }

        private void GenerateButton_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == "Uniform")
            {
                int x, y, Vx, Vy;
                Graphics g = panel1.CreateGraphics();
                Number = new UniformRandom(1, 100);
                PosX = new UniformRandom(0, panel1.Width);
                PosY = new UniformRandom(0, panel1.Height);
                int AgentsNumber = Convert.ToInt32(Number.NextDouble());
                AgentNo = AgentsNumber;
                for (int i = 0; i < AgentsNumber; i++)
                {
                    x = Convert.ToInt32(PosX.NextDouble());
                    y = Convert.ToInt32(PosY.NextDouble());
                    Vx = Convert.ToInt32(PosX.NextDouble());
                    Vy = Convert.ToInt32(PosY.NextDouble());
                    Pen p = new Pen(Color.Green, 1);
                    g.DrawRectangle(p, new Rectangle(x, y, 2, 2));
                    A.Add(new Agent(x, y, Vx, Vy, R.GetRadiation, null));
                }
            }
            else if (comboBox1.SelectedItem == "Exponential")
            { }
            else if (comboBox1.SelectedItem == "Normal")
            { }
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            int count1 = 0;
            int count2 = 0;
            Barr = new Barrier[B.Count];
            foreach (Barrier br in B)
            {
                Barr[count1] = br;
                count1++;
            }
            env = new Swarm_Logic.Environment(AgentNo, panel1.Width, panel1.Height, Barr, R);

            Ag = new Agent[A.Count];
            foreach (Agent l in A)
            {
                l.Send = env.Send;
                Ag[count2] = l;
                count2++;
            }
            env.Agents = Ag;
            env.OnIterationEnd += RefreshMe;
            env.Run(1000);

        }
        public void drawAgents()
        {
            Graphics g = panel1.CreateGraphics();

            Pen p = new Pen(Color.Green, 1);
            Pen p2 = new Pen(Color.Orange, 1);
            foreach (Agent i in env.Agents)
            {
                if (i.FoundSource)
                {
                    g.DrawRectangle(p2, new Rectangle(Convert.ToInt32(i.PX), Convert.ToInt32(i.PY), 2, 2));
                }
                else
                {
                    g.DrawRectangle(p, new Rectangle(Convert.ToInt32(i.PX), Convert.ToInt32(i.PY), 2, 2));
                }
            }
        }
        public void drawBarrier()
        {
            Graphics g = panel1.CreateGraphics();

            Pen p = new Pen(Color.Blue, 2);
            foreach (Barrier b in Barr)
            {
                g.DrawLine(p, new Point(Convert.ToInt32(b.X1),Convert.ToInt32(b.Y1)),new Point(Convert.ToInt32(b.X2),Convert.ToInt32(b.Y2)));
                //g.Dispose();

            }
        }
        public void drawSource()
        {
            Graphics g = panel1.CreateGraphics();
            Pen myPen = new Pen(System.Drawing.Color.Red, 7);
            Rectangle myRectangle = new Rectangle(SourceX, SourceY, 7, 7);
            g.DrawEllipse(myPen, myRectangle);

        }

        public void RefreshMe()
        {
            panel1.Refresh();
            drawAgents();
            drawSource();
            drawBarrier();
        }

    }
}

