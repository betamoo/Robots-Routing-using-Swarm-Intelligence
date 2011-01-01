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

        List<double> def = new List<double>();

        List<Point> _source;

        int MaxX;
        int MaxY;


        public EnviromentGUI()
        {
            InitializeComponent();
            MaxX = panel1.Size.Width;
            MaxY = panel1.Size.Height;



            _source = new List<Point>();

            B.Add(new Swarm_Logic.Barrier(0, 0, MaxX, 0));
            B.Add(new Swarm_Logic.Barrier(MaxX, 0, MaxX, MaxY));
            B.Add(new Swarm_Logic.Barrier(MaxX, MaxY, 0, MaxY));
            B.Add(new Swarm_Logic.Barrier(0, MaxY, 0, 0));

   

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


                _source.Add(new Point(e.X,e.Y));
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
            try
            {
                int agentNum = int.Parse(AgentsNum.Text);
                enableStart();
                if (_source.Count == 0)
                    throw new Exception("Please Add at least one source");

                if (_source.Count == 1)
                {
                    switch (comboBox2.SelectedIndex)
                    {
                        case 0:
                            R = new EuclideanDistanceSource(_source[0].X,_source[0].Y);
                            env = new Swarm_Logic.Environment(agentNum, MaxY, MaxY, B,R);
                            break;
                        case 1:
                            R = new GaussianFunctionSource(_source[0].X,_source[0].Y,1000);
                            env = new Swarm_Logic.Environment(agentNum, MaxY, MaxY, B,R);
                            break;
                        default:
                            throw new Exception("Please Add at More Than source");
                            
                    }
                    

                }
                else
                {
                    List<double> _xpos = new List<double>();
                    List<double> _ypos = new List<double>();
                    List<double> _A = new List<double>();
                    List<double> _B = new List<double>();
                    foreach (Point p in _source)
                    {
                        _xpos.Add(p.X);
                        _ypos.Add(p.Y);
                        _A.Add(1000);
                        _B.Add(1000);
                    }
                    switch (comboBox2.SelectedIndex)
                    {
                        case 2:

                            R = new MultipleGaussianFunctionSources(_xpos.ToArray(), _ypos.ToArray(), _A.ToArray(), _B.ToArray());
                            env = new Swarm_Logic.Environment(agentNum, MaxY, MaxY, B, R);
                            break;
                        case 3:
                            R = new MultipleNoisyGaussianFunctionSources(_xpos.ToArray(), _ypos.ToArray(), _A.ToArray(), _B.ToArray());
                            env = new Swarm_Logic.Environment(agentNum, MaxY, MaxY, B, R);
                            break;
                        default:
                            throw new Exception("Please Select Multiple Type Sources");
                            
                    }
                }
                RefreshMe();
            }
            
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
                disableStart();
            }


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
          //  env = new Swarm_Logic.Environment(A, panel1.Width, panel1.Height, B, R);

            env.OnIterationEnd += RefreshMe;
            env.Run(int.Parse(textBox1.Text));

        }
        public void drawAgents()
        {
            Graphics g = panel1.CreateGraphics();

            Pen p = new Pen(Color.Orange, 1);
            Pen p2 = new Pen(Color.Green, 1);
            Pen p3 = new Pen(Color.Yellow, 2);
            Pen myPen = new Pen(System.Drawing.Color.Red, 1);
            myPen.DashStyle = DashStyle.Dot;

            foreach (Agent i in env.Agents)
            {
                Rectangle myRectangle = new Rectangle((int)i.PX-((int)def[3]/2+1), (int)i.PY-((int)def[3]/2+1), (int)def[3], (int)def[3]);
                g.DrawEllipse(myPen, myRectangle);

                g.DrawEllipse(myPen, myRectangle);

                if (i.FoundSource)
                {
                    g.DrawRectangle(p2, new Rectangle(Convert.ToInt32(i.PX), Convert.ToInt32(i.PY), 2, 2));
                }
                else
                {
                    if(!i.Sending)
                        g.DrawRectangle(p, new Rectangle(Convert.ToInt32(i.PX), Convert.ToInt32(i.PY), 2, 2));
                    else
                        g.DrawRectangle(p3, new Rectangle(Convert.ToInt32(i.PX), Convert.ToInt32(i.PY), 2, 2));
                }
            }
        }
        public void drawBarrier()
        {
            Graphics g = panel1.CreateGraphics();

            Pen p = new Pen(Color.Blue, 2);
            foreach (Barrier b in B)
            {
                g.DrawLine(p, new Point(Convert.ToInt32(b.X1),Convert.ToInt32(b.Y1)),new Point(Convert.ToInt32(b.X2),Convert.ToInt32(b.Y2)));
                //g.Dispose();

            }
        }
        public void drawSource()
        {
            Graphics g = panel1.CreateGraphics();
            Pen myPen = new Pen(System.Drawing.Color.Red, 7);
            foreach (Point p in _source)
            {
                Rectangle myRectangle = new Rectangle(p.X, p.Y, 7, 7);
                g.DrawEllipse(myPen, myRectangle);
            }
           
        }

        public void resetInitDraw()
        {
            GenerateButton.Enabled = false;
            StartButton.Enabled = false;
            RestartButton.Enabled = false;
            AgentsNum.Enabled = false;
            comboBox2.Enabled = false;
            textBox1.Enabled = false;
            SourceButton.Enabled = false;
            BarrierButton.Enabled = false;
        }

        public void enableInitDraw()
        {
            GenerateButton.Enabled = true;
            comboBox2.Enabled = true;
            AgentsNum.Enabled = true;
            textBox1.Enabled = true;
            SourceButton.Enabled = true;
            BarrierButton.Enabled = true;

            StartButton.Enabled = false;
            RestartButton.Enabled = false;
        }
        public void RefreshMe()
        {
            panel1.Refresh();
            drawAgents();
            drawSource();
            drawBarrier();
        }

        private void EnviromentGUI_Load(object sender, EventArgs e)
        {
            readDefaultParameters();
        }
        public void readDefaultParameters()
        {
            def.Add(Swarm_Logic.GeneralParameters.W);
            def.Add(Swarm_Logic.GeneralParameters.P);
            def.Add(Swarm_Logic.GeneralParameters.G);
            def.Add(Swarm_Logic.GeneralParameters.ReceiveRange);
            def.Add(Swarm_Logic.GeneralParameters.NearDistance);
            def.Add(Swarm_Logic.GeneralParameters.MaxVelocity);
            def.Add(Swarm_Logic.GeneralParameters.MinVelocity);

            W_param.Text = def[0].ToString();
            B_param.Text = def[1].ToString();
            G_param.Text = def[2].ToString();
            CR_param.Text = def[3].ToString();
            SR_param.Text = def[4].ToString();
            MV_param.Text = def[5].ToString();
            MinVPram.Text = def[6].ToString();
        }
        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void SourceButton_Click(object sender, EventArgs e)
        {

        }

        private void RestartButton_Click(object sender, EventArgs e)
        {
            panel1.Refresh();
            _source = new List<Point>();
            B = new List<Barrier>();
            B.Add(new Swarm_Logic.Barrier(0, 0, MaxX, 0));
            B.Add(new Swarm_Logic.Barrier(MaxX, 0, MaxX, MaxY));
            B.Add(new Swarm_Logic.Barrier(MaxX, MaxY, 0, MaxY));
            B.Add(new Swarm_Logic.Barrier(0, MaxY, 0, 0));
            disableStart();
        }

        public void enableStart()
        {
            StartButton.Enabled = true;
        }

        public void disableStart()
        {
            StartButton.Enabled = false;
        }

        private void Writepram_Click(object sender, EventArgs e)
        {
            double w, b, g, maxv, minv, comrange, neardistance;
            try
            {
                w = double.Parse(W_param.Text);
                b = double.Parse(B_param.Text);
                g = double.Parse(G_param.Text);
                maxv = double.Parse(MV_param.Text);
                minv = double.Parse(MinVPram.Text);
                comrange = double.Parse(CR_param.Text);
                neardistance = double.Parse(SR_param.Text);
                Swarm_Logic.GeneralParameters.G = g;
                Swarm_Logic.GeneralParameters.P=  b;
                Swarm_Logic.GeneralParameters.W = w;
                Swarm_Logic.GeneralParameters.MaxVelocity = maxv;
                Swarm_Logic.GeneralParameters.MinVelocity = minv;
                Swarm_Logic.GeneralParameters.ReceiveRange = comrange;
                Swarm_Logic.GeneralParameters.NearDistance = neardistance;
                enableInitDraw();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }

        }

        private void Clear_Click(object sender, EventArgs e)
        {
            W_param.Text = "";
            B_param.Text = "";
            G_param.Text = "";
            CR_param.Text = "";
            SR_param.Text = "";
            MV_param.Text = "";
            MinVPram.Text = "";

            resetInitDraw();

            _source = new List<Point>();
            B = new List<Barrier>();
            B.Add(new Swarm_Logic.Barrier(0, 0, MaxX, 0));
            B.Add(new Swarm_Logic.Barrier(MaxX, 0, MaxX, MaxY));
            B.Add(new Swarm_Logic.Barrier(MaxX, MaxY, 0, MaxY));
            B.Add(new Swarm_Logic.Barrier(0, MaxY, 0, 0));
            disableStart();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            W_param.Text = def[0].ToString();
            B_param.Text = def[1].ToString();
            G_param.Text = def[2].ToString();
            CR_param.Text = def[3].ToString();
            SR_param.Text = def[4].ToString();
            MV_param.Text = def[5].ToString();
            MinVPram.Text = def[6].ToString();
            enableInitDraw();
        }
    }
}

