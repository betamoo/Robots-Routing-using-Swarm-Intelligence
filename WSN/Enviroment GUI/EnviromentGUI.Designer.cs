namespace Enviroment_GUI
{
    partial class EnviromentGUI
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.BarrierButton = new System.Windows.Forms.Button();
            this.SourceButton = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.RestartButton = new System.Windows.Forms.Button();
            this.StartButton = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.AgentsNum = new System.Windows.Forms.TextBox();
            this.GenerateButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(556, 468);
            this.panel1.TabIndex = 0;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            this.panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseMove);
            this.panel1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseUp);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.BarrierButton);
            this.groupBox1.Controls.Add(this.SourceButton);
            this.groupBox1.Location = new System.Drawing.Point(17, 19);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(128, 166);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Objects";
            // 
            // BarrierButton
            // 
            this.BarrierButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.BarrierButton.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BarrierButton.Image = global::Enviroment_GUI.Properties.Resources.brick_wall;
            this.BarrierButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.BarrierButton.Location = new System.Drawing.Point(6, 43);
            this.BarrierButton.Name = "BarrierButton";
            this.BarrierButton.Size = new System.Drawing.Size(80, 33);
            this.BarrierButton.TabIndex = 0;
            this.BarrierButton.Text = "Barrier";
            this.BarrierButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BarrierButton.UseVisualStyleBackColor = true;
            this.BarrierButton.Click += new System.EventHandler(this.BarrierButton_Click);
            // 
            // SourceButton
            // 
            this.SourceButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.SourceButton.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SourceButton.ForeColor = System.Drawing.Color.Snow;
            this.SourceButton.Image = global::Enviroment_GUI.Properties.Resources._4row_radiator;
            this.SourceButton.Location = new System.Drawing.Point(6, 96);
            this.SourceButton.Name = "SourceButton";
            this.SourceButton.Size = new System.Drawing.Size(80, 33);
            this.SourceButton.TabIndex = 0;
            this.SourceButton.Text = "Source";
            this.SourceButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.SourceButton.UseVisualStyleBackColor = true;
            this.SourceButton.Click += new System.EventHandler(this.SourceButton_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.RestartButton);
            this.groupBox2.Controls.Add(this.StartButton);
            this.groupBox2.Location = new System.Drawing.Point(23, 352);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(119, 110);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Operations";
            // 
            // RestartButton
            // 
            this.RestartButton.Location = new System.Drawing.Point(18, 75);
            this.RestartButton.Name = "RestartButton";
            this.RestartButton.Size = new System.Drawing.Size(59, 29);
            this.RestartButton.TabIndex = 0;
            this.RestartButton.Text = "Restart";
            this.RestartButton.UseVisualStyleBackColor = true;
            this.RestartButton.Click += new System.EventHandler(this.RestartButton_Click);
            // 
            // StartButton
            // 
            this.StartButton.Enabled = false;
            this.StartButton.Location = new System.Drawing.Point(18, 34);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(59, 29);
            this.StartButton.TabIndex = 0;
            this.StartButton.Text = "Start";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.textBox1);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.AgentsNum);
            this.groupBox3.Controls.Add(this.GenerateButton);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.comboBox2);
            this.groupBox3.Controls.Add(this.groupBox1);
            this.groupBox3.Controls.Add(this.groupBox2);
            this.groupBox3.Location = new System.Drawing.Point(574, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(203, 468);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Utilities";
            this.groupBox3.Enter += new System.EventHandler(this.groupBox3_Enter);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 240);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Number Of Agents";
            // 
            // AgentsNum
            // 
            this.AgentsNum.Location = new System.Drawing.Point(112, 237);
            this.AgentsNum.Name = "AgentsNum";
            this.AgentsNum.Size = new System.Drawing.Size(74, 20);
            this.AgentsNum.TabIndex = 7;
            // 
            // GenerateButton
            // 
            this.GenerateButton.Location = new System.Drawing.Point(26, 263);
            this.GenerateButton.Name = "GenerateButton";
            this.GenerateButton.Size = new System.Drawing.Size(160, 29);
            this.GenerateButton.TabIndex = 1;
            this.GenerateButton.Text = "Generate";
            this.GenerateButton.UseVisualStyleBackColor = true;
            this.GenerateButton.Click += new System.EventHandler(this.GenerateButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 190);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Source Type";
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "Euclidean Distance Source",
            "Gaussian Function Source",
            "Multiple Gaussian Function Sources",
            "Multiple Noise Gaussian Function Sources"});
            this.comboBox2.Location = new System.Drawing.Point(26, 206);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(160, 21);
            this.comboBox2.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(47, 310);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Iterations:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(114, 307);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(74, 20);
            this.textBox1.TabIndex = 9;
            // 
            // EnviromentGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(797, 492);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.panel1);
            this.Name = "EnviromentGUI";
            this.Text = "Enviroment";
            this.Load += new System.EventHandler(this.EnviromentGUI_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button BarrierButton;
        private System.Windows.Forms.Button SourceButton;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button RestartButton;
        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Button GenerateButton;
        private System.Windows.Forms.TextBox AgentsNum;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox1;
    }
}

