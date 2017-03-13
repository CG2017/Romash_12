namespace CG_Lab2
{
    partial class ImageProcesser
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
            this.pbSourceImage = new System.Windows.Forms.PictureBox();
            this.pbProcessedImage = new System.Windows.Forms.PictureBox();
            this.lbRadius = new System.Windows.Forms.Label();
            this.tbRadius = new System.Windows.Forms.TrackBar();
            this.cdChooseColor = new System.Windows.Forms.ColorDialog();
            this.lbOriginalColor = new System.Windows.Forms.Label();
            this.lbDestinationColor = new System.Windows.Forms.Label();
            this.pbOriginalColor = new System.Windows.Forms.PictureBox();
            this.pbDestinationColor = new System.Windows.Forms.PictureBox();
            this.btReplace = new System.Windows.Forms.Button();
            this.ofdSourceFile = new System.Windows.Forms.OpenFileDialog();
            this.btRevert = new System.Windows.Forms.Button();
            this.lbRadiusValue = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbSourceImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbProcessedImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbRadius)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbOriginalColor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbDestinationColor)).BeginInit();
            this.SuspendLayout();
            // 
            // pbSourceImage
            // 
            this.pbSourceImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbSourceImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbSourceImage.Location = new System.Drawing.Point(17, 13);
            this.pbSourceImage.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pbSourceImage.Name = "pbSourceImage";
            this.pbSourceImage.Size = new System.Drawing.Size(411, 260);
            this.pbSourceImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbSourceImage.TabIndex = 0;
            this.pbSourceImage.TabStop = false;
            this.pbSourceImage.Click += new System.EventHandler(this.pbSourceImage_Click);
            // 
            // pbProcessedImage
            // 
            this.pbProcessedImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbProcessedImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbProcessedImage.Location = new System.Drawing.Point(455, 13);
            this.pbProcessedImage.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pbProcessedImage.Name = "pbProcessedImage";
            this.pbProcessedImage.Size = new System.Drawing.Size(422, 260);
            this.pbProcessedImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbProcessedImage.TabIndex = 0;
            this.pbProcessedImage.TabStop = false;
            // 
            // lbRadius
            // 
            this.lbRadius.AutoSize = true;
            this.lbRadius.Location = new System.Drawing.Point(885, 27);
            this.lbRadius.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbRadius.Name = "lbRadius";
            this.lbRadius.Size = new System.Drawing.Size(52, 17);
            this.lbRadius.TabIndex = 1;
            this.lbRadius.Text = "Radius";
            // 
            // tbRadius
            // 
            this.tbRadius.Location = new System.Drawing.Point(947, 15);
            this.tbRadius.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbRadius.Maximum = 100;
            this.tbRadius.Name = "tbRadius";
            this.tbRadius.Size = new System.Drawing.Size(393, 56);
            this.tbRadius.TabIndex = 2;
            this.tbRadius.Value = 70;
            this.tbRadius.ValueChanged += new System.EventHandler(this.tbRadius_ValueChanged);
            // 
            // lbOriginalColor
            // 
            this.lbOriginalColor.AutoSize = true;
            this.lbOriginalColor.Location = new System.Drawing.Point(885, 64);
            this.lbOriginalColor.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbOriginalColor.Name = "lbOriginalColor";
            this.lbOriginalColor.Size = new System.Drawing.Size(92, 17);
            this.lbOriginalColor.TabIndex = 3;
            this.lbOriginalColor.Text = "Original color";
            // 
            // lbDestinationColor
            // 
            this.lbDestinationColor.AutoSize = true;
            this.lbDestinationColor.Location = new System.Drawing.Point(1120, 64);
            this.lbDestinationColor.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbDestinationColor.Name = "lbDestinationColor";
            this.lbDestinationColor.Size = new System.Drawing.Size(114, 17);
            this.lbDestinationColor.TabIndex = 4;
            this.lbDestinationColor.Text = "Destination color";
            // 
            // pbOriginalColor
            // 
            this.pbOriginalColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbOriginalColor.Location = new System.Drawing.Point(985, 64);
            this.pbOriginalColor.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pbOriginalColor.Name = "pbOriginalColor";
            this.pbOriginalColor.Size = new System.Drawing.Size(68, 61);
            this.pbOriginalColor.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbOriginalColor.TabIndex = 5;
            this.pbOriginalColor.TabStop = false;
            this.pbOriginalColor.Click += new System.EventHandler(this.pbOriginalColor_Click);
            // 
            // pbDestinationColor
            // 
            this.pbDestinationColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbDestinationColor.Location = new System.Drawing.Point(1242, 64);
            this.pbDestinationColor.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pbDestinationColor.Name = "pbDestinationColor";
            this.pbDestinationColor.Size = new System.Drawing.Size(61, 61);
            this.pbDestinationColor.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbDestinationColor.TabIndex = 5;
            this.pbDestinationColor.TabStop = false;
            this.pbDestinationColor.Click += new System.EventHandler(this.pbDestinationColor_Click);
            // 
            // btReplace
            // 
            this.btReplace.Location = new System.Drawing.Point(888, 133);
            this.btReplace.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btReplace.Name = "btReplace";
            this.btReplace.Size = new System.Drawing.Size(132, 33);
            this.btReplace.TabIndex = 6;
            this.btReplace.Text = "Replace";
            this.btReplace.UseVisualStyleBackColor = true;
            this.btReplace.Click += new System.EventHandler(this.btReplace_Click);
            // 
            // ofdSourceFile
            // 
            this.ofdSourceFile.FileName = "openFileDialog1";
            // 
            // btRevert
            // 
            this.btRevert.Location = new System.Drawing.Point(1123, 133);
            this.btRevert.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btRevert.Name = "btRevert";
            this.btRevert.Size = new System.Drawing.Size(132, 33);
            this.btRevert.TabIndex = 6;
            this.btRevert.Text = "Revert";
            this.btRevert.UseVisualStyleBackColor = true;
            this.btRevert.Click += new System.EventHandler(this.btRevert_Click);
            // 
            // lbRadiusValue
            // 
            this.lbRadiusValue.AutoSize = true;
            this.lbRadiusValue.Location = new System.Drawing.Point(1348, 27);
            this.lbRadiusValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbRadiusValue.Name = "lbRadiusValue";
            this.lbRadiusValue.Size = new System.Drawing.Size(0, 17);
            this.lbRadiusValue.TabIndex = 1;
            // 
            // ImageProcesser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1400, 304);
            this.Controls.Add(this.btRevert);
            this.Controls.Add(this.btReplace);
            this.Controls.Add(this.pbDestinationColor);
            this.Controls.Add(this.pbOriginalColor);
            this.Controls.Add(this.lbDestinationColor);
            this.Controls.Add(this.lbOriginalColor);
            this.Controls.Add(this.tbRadius);
            this.Controls.Add(this.lbRadiusValue);
            this.Controls.Add(this.lbRadius);
            this.Controls.Add(this.pbProcessedImage);
            this.Controls.Add(this.pbSourceImage);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "ImageProcesser";
            this.Text = "Image Processer";
            ((System.ComponentModel.ISupportInitialize)(this.pbSourceImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbProcessedImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbRadius)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbOriginalColor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbDestinationColor)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox pbProcessedImage;
        private System.Windows.Forms.Label lbRadius;
        private System.Windows.Forms.TrackBar tbRadius;
        private System.Windows.Forms.ColorDialog cdChooseColor;
        private System.Windows.Forms.Label lbOriginalColor;
        private System.Windows.Forms.Label lbDestinationColor;
        private System.Windows.Forms.PictureBox pbOriginalColor;
        private System.Windows.Forms.PictureBox pbDestinationColor;
        private System.Windows.Forms.Button btReplace;
        private System.Windows.Forms.OpenFileDialog ofdSourceFile;
        public System.Windows.Forms.PictureBox pbSourceImage;
        private System.Windows.Forms.Button btRevert;
        private System.Windows.Forms.Label lbRadiusValue;
    }
}

