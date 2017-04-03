using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;


namespace lab3
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            ConfigureApplication();
        }

        private void ConfigureApplication()
        {
            listBoxChannels.SelectedValueChanged += DiagramTypeChangedHandler;
        }

        private void DiagramTypeChangedHandler(object sender, EventArgs args)
        {
            string diagramType = (string)listBoxChannels.SelectedItem;
            switch (diagramType)
            {
                case "Red":
                    PaintDiagram(Color.Red, 256, color => color.R);
                    break;
                case "Green":
                    PaintDiagram(Color.Green, 256, color => color.G);
                    break;
                case "Blue":
                    PaintDiagram(Color.Blue, 256, color => color.B);
                    break;
            }
        }

        private void PaintDiagram(Color color, int size, Func<Color, int> indexSelector)
        {
            Bitmap bitmap = pctSource.Image as Bitmap;
            if (bitmap == null)
                return;

            int[] statistics = new int[size];

            for (int x = 0; x < bitmap.Width; x++)
            {
                for (int y = 0; y < bitmap.Height; y++)
                {
                    Color curPixel = bitmap.GetPixel(x, y);
                    statistics[indexSelector(curPixel)]++;
                }
            }

            long sumPixels = 0;
            long sumColor = 0;
            for (int i = 0; i < statistics.Length; i++)
            {
                sumPixels += statistics[i];
                sumColor += statistics[i]*i;
            }

            int average = (int) ((double)sumColor/sumPixels);
            textBoxAverage.Text = average.ToString();

            chartChannels.Series["ChannelValues"].Color = color;
            chartChannels.Series["ChannelValues"].Points.Clear();
            for (int i = 0; i < statistics.Length; i++)
            {
                chartChannels.Series["ChannelValues"].Points.AddXY(i, statistics[i] + 1);
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            FileDialog fd = new OpenFileDialog();
            if (fd.ShowDialog(this) == DialogResult.OK)
            {
                pctSource.Image = Bitmap.FromFile(fd.FileName);
            }
        }
    }
}
