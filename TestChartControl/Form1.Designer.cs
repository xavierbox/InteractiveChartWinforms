namespace TestChartControl
    {
        partial class Form1
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            this.chart2 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.generateDataButton = new System.Windows.Forms.Button();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.button6 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.chart2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // chart2
            // 
            this.chart2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chart2.BorderlineColor = System.Drawing.Color.Black;
            this.chart2.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            chartArea1.AxisY2.MajorGrid.Enabled = false;
            chartArea1.Name = "ChartArea1";
            this.chart2.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart2.Legends.Add(legend1);
            this.chart2.Location = new System.Drawing.Point(609, 31);
            this.chart2.Name = "chart2";
            this.chart2.Size = new System.Drawing.Size(563, 618);
            this.chart2.TabIndex = 1;
            this.chart2.Text = "chart2";
            this.chart2.Click += new System.EventHandler(this.ActivateChart);
            this.chart2.DoubleClick += new System.EventHandler(this.Form1_DoubleClick);
            // 
            // generateDataButton
            // 
            this.generateDataButton.Location = new System.Drawing.Point(3, 2);
            this.generateDataButton.Name = "generateDataButton";
            this.generateDataButton.Size = new System.Drawing.Size(112, 23);
            this.generateDataButton.TabIndex = 2;
            this.generateDataButton.Text = "Generate data";
            this.generateDataButton.UseVisualStyleBackColor = true;
            this.generateDataButton.Click += new System.EventHandler(this.GenerateSampleData);
            // 
            // chart1
            // 
            this.chart1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.chart1.BorderlineColor = System.Drawing.Color.Black;
            this.chart1.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            chartArea2.AxisY2.MajorGrid.Enabled = false;
            chartArea2.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.chart1.Legends.Add(legend2);
            this.chart1.Location = new System.Drawing.Point(12, 31);
            this.chart1.Name = "chart1";
            this.chart1.Size = new System.Drawing.Size(591, 618);
            this.chart1.TabIndex = 3;
            this.chart1.Text = "chart1";
            this.chart1.Click += new System.EventHandler(this.ActivateChart);
            this.chart1.DoubleClick += new System.EventHandler(this.Form1_DoubleClick);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(121, 2);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(112, 23);
            this.button6.TabIndex = 4;
            this.button6.Text = "Clear";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.ClearChart);
            // 
            // Form1
            // 
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(1184, 661);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.chart2);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.generateDataButton);
            this.MaximumSize = new System.Drawing.Size(1200, 700);
            this.MinimumSize = new System.Drawing.Size(1200, 400);
            this.Name = "Form1";
            this.DoubleClick += new System.EventHandler(this.Form1_DoubleClick);
            ((System.ComponentModel.ISupportInitialize)(this.chart2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);

            }
    
            #endregion
    
           // private ChartControl.InteractiveChart interactiveChart1;
            private System.Windows.Forms.Button button1;
            private System.Windows.Forms.Button button2;
            private System.Windows.Forms.Button button3;
            private System.Windows.Forms.Button button4;
            private System.Windows.Forms.Button button5;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart2;
        private System.Windows.Forms.Button generateDataButton;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Button button6;
    }
    }
    

