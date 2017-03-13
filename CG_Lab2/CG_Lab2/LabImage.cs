using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using ColorMine.ColorSpaces;

namespace CG_Lab2
{
    public class LabImage
    {
        private List<List<Lab>> labPixels;
        private readonly int wid;
        private readonly int hei;

        private Rgb CreateRgbColor(Color color)
        {
            return new Rgb { R = color.R, G = color.G, B = color.B };
        }
        public LabImage(Image image)
        {
            labPixels = new List<List<Lab>>();
            wid = image.Width;
            hei = image.Height;

            ConvertToLab(new Bitmap(image));
        }

        private void ConvertToLab(Bitmap image)
        {
            for (int i = 0; i < hei; ++i)
            {
                List<Lab> pixelsRow = new List<Lab>();

                for (int j = 0; j < wid; ++j)
                {
                    var pixel = image.GetPixel(j, i);

                    pixelsRow.Add(CreateRgbColor(pixel).To<Lab>());
                }

                labPixels.Add(pixelsRow);
            }
        }

        public Image GetRgbImage()
        {
            Bitmap bitmap = new Bitmap(wid, hei);
            var i = -1;
            var j = 0;

            foreach(var row in labPixels)
            {
                ++i;
                foreach(var pixel in row)
                {
                    var rgbPixel = pixel.To<Rgb>();

                    bitmap.SetPixel(j++, i, Color.FromArgb((int)rgbPixel.R, (int)rgbPixel.G, (int)rgbPixel.B));
                }
                j = 0;
            }

            return bitmap;
        }

        private Lab GenerateDestinationColor(double dL, double dA, double dB, Lab labDestinationColor)
        {
            return new Lab { L = labDestinationColor.L + dL, A = labDestinationColor.A + dA, B = labDestinationColor.B + dB };
        }

        public void ReplaceColor(Color sourceColor, Color destinationColor, Int32 radius)
        {
            var labSourceColor = CreateRgbColor(sourceColor).To<Lab>();
            var labDestinationColor = CreateRgbColor(destinationColor).To<Lab>();

            List<List<Lab>> processedImage = new List<List<Lab>>();

            foreach(var row in labPixels)
            {
                List<Lab> processedRow = new List<Lab>();
                foreach(var pixel in row)
                {
                    var dL = pixel.L - labSourceColor.L;
                    var dA = pixel.A - labSourceColor.A;
                    var dB = pixel.B - labSourceColor.B;

                    processedRow.Add(Math.Sqrt(dL * dL + dA * dA + dB * dB) < radius ? GenerateDestinationColor(dL, dA, dB, labDestinationColor) : pixel);
                }
                processedImage.Add(processedRow);
            }

            labPixels = processedImage;
        }
    }
}
