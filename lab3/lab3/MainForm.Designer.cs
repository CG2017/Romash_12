namespace lab3
{
    partial class MainForm
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
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint1 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(1D, 5D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint2 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(2D, 3D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint3 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(30D, 7D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint4 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(30D, 2D);
            this.pctSource = new System.Windows.Forms.PictureBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.listBoxChannels = new System.Windows.Forms.ListBox();
            this.chartChannels = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.textBoxAverage = new System.Windows.Forms.TextBox();
            this.lblAverage = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pctSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartChannels)).BeginInit();
            this.SuspendLayout();
            // 
            // pctSource
            // 
            this.pctSource.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pctSource.Location = new System.Drawing.Point(16, 15);
            this.pctSource.Margin = new System.Windows.Forms.Padding(4);
            this.pctSource.Name = "pctSource";
            this.pctSource.Size = new System.Drawing.Size(481, 419);
            this.pctSource.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pctSource.TabIndex = 0;
            this.pctSource.TabStop = false;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(16, 453);
            this.btnBrowse.Margin = new System.Windows.Forms.Padding(4);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(100, 28);
            this.btnBrowse.TabIndex = 1;
            this.btnBrowse.Text = "Browse...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // listBoxChannels
            // 
            this.listBoxChannels.FormattingEnabled = true;
            this.listBoxChannels.ItemHeight = 16;
            this.listBoxChannels.Items.AddRange(new object[] {
            "Red",
            "Green",
            "Blue"});
            this.listBoxChannels.Location = new System.Drawing.Point(341, 453);
            this.listBoxChannels.Margin = new System.Windows.Forms.Padding(4);
            this.listBoxChannels.Name = "listBoxChannels";
            this.listBoxChannels.Size = new System.Drawing.Size(155, 52);
            this.listBoxChannels.TabIndex = 3;
            // 
            // chartChannels
            // 
            chartArea1.AxisY.IsLogarithmic = true;
            chartArea1.AxisY2.IsLogarithmic = true;
            chartArea1.Name = "ChannelArea";
            this.chartChannels.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chartChannels.Legends.Add(legend1);
            this.chartChannels.Location = new System.Drawing.Point(518, 15);
            this.chartChannels.Margin = new System.Windows.Forms.Padding(4);
            this.chartChannels.Name = "chartChannels";
            series1.ChartArea = "ChannelArea";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Area;
            series1.Legend = "Legend1";
            series1.Name = "ChannelValues";
            series1.Points.Add(dataPoint1);
            series1.Points.Add(dataPoint2);
            series1.Points.Add(dataPoint3);
            series1.Points.Add(dataPoint4);
            this.chartChannels.Series.Add(series1);
            this.chartChannels.Size = new System.Drawing.Size(1270, 714);
            this.chartChannels.TabIndex = 4;
            this.chartChannels.Text = "Channels diagram";
            // 
            // textBoxAverage
            // 
            this.textBoxAverage.Location = new System.Drawing.Point(193, 456);
            this.textBoxAverage.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxAverage.Name = "textBoxAverage";
            this.textBoxAverage.Size = new System.Drawing.Size(132, 22);
            this.textBoxAverage.TabIndex = 5;
            // 
            // lblAverage
            // 
            this.lblAverage.AutoSize = true;
            this.lblAverage.Location = new System.Drawing.Point(124, 453);
            this.lblAverage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAverage.Name = "lblAverage";
            this.lblAverage.Size = new System.Drawing.Size(61, 17);
            this.lblAverage.TabIndex = 6;
            this.lblAverage.Text = "Average";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1661, 742);
            this.Controls.Add(this.lblAverage);
            this.Controls.Add(this.textBoxAverage);
            this.Controls.Add(this.chartChannels);
            this.Controls.Add(this.listBoxChannels);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.pctSource);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainForm";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pctSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartChannels)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pctSource;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.ListBox listBoxChannels;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartChannels;
        private System.Windows.Forms.TextBox textBoxAverage;
        private System.Windows.Forms.Label lblAverage;
    }
}

