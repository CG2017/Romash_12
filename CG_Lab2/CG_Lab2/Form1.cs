using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CG_Lab2
{
    public partial class ImageProcesser : Form
    {
        private Image sourceImage;

        private LabImage labImage;

        private Color originalColor;
        private Color destinationColor;
        public ImageProcesser()
        {
            InitializeComponent();
        }

        private void openImage()
        {
            if (ofdSourceFile.ShowDialog() == DialogResult.OK)
            {
                var fileName = ofdSourceFile.FileName;

                sourceImage = Image.FromFile(fileName);
            }
        }

        private void initLabImage()
        {
            labImage = new LabImage(sourceImage);
        }

        private void ShowImage(PictureBox pictureBox, Image image)
        {
            pictureBox.Image = image;
            pictureBox.Refresh();
        }

        private void SetOriginalColor(EventArgs e)
        {
            MouseEventArgs me = (MouseEventArgs)e;

            Bitmap original = (Bitmap)pbSourceImage.Image;

            Rectangle rectangle = (Rectangle)imageRectangleProperty.GetValue(pbSourceImage, null);
            if (rectangle.Contains(me.Location))
            {
                using (Bitmap copy = new Bitmap(pbSourceImage.ClientSize.Width, pbSourceImage.ClientSize.Height))
                {
                    using (Graphics g = Graphics.FromImage(copy))
                    {
                        g.DrawImage(pbSourceImage.Image, rectangle);

                        originalColor = copy.GetPixel(me.X, me.Y);
                    }
                }
            }
            pbOriginalColor.BackColor = originalColor;
        }

        private void pbSourceImage_Click(object sender, EventArgs e)
        {
            if (pbSourceImage.Image == null)
            {
                openImage();
                initLabImage();
                ShowImage(pbSourceImage, sourceImage);
                ShowImage(pbProcessedImage, sourceImage);
            }
            else
            {
                SetOriginalColor(e);
            }
        }

        private void setColor(PictureBox pb, ref Color color)
        {
            if (cdChooseColor.ShowDialog() == DialogResult.OK)
            {
                color = cdChooseColor.Color;
                pb.BackColor = color;
            }
        }

        PropertyInfo imageRectangleProperty = typeof(PictureBox).GetProperty("ImageRectangle", BindingFlags.GetProperty | BindingFlags.NonPublic | BindingFlags.Instance);

        private void pbOriginalColor_Click(object sender, EventArgs e)
        {
            setColor(pbOriginalColor, ref originalColor);
        }

        private void pbDestinationColor_Click(object sender, EventArgs e)
        {
            setColor(pbDestinationColor, ref destinationColor);
        }

        private void btReplace_Click(object sender, EventArgs e)
        {
            labImage.ReplaceColor(originalColor, destinationColor, tbRadius.Value);
            pbProcessedImage.Image = labImage.GetRgbImage();
            pbProcessedImage.Refresh();
        }

        private void btRevert_Click(object sender, EventArgs e)
        {
            initLabImage();
            ShowImage(pbProcessedImage, sourceImage);
        }

        private void tbRadius_ValueChanged(object sender, EventArgs e)
        {
            lbRadiusValue.Text = tbRadius.Value.ToString();
        }
    }
}
