using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using lab5.Properties;
using ZedGraph;

namespace lab5
{
    public partial class Form1 : Form
    {
        private static readonly List<Point> DataList = new List<Point>();

        private const string BCircle = "B - Circle";
        private const string BLine = "B - Line";
        private const string SLine = "Simple - Line";
        private const string DDALine = "DDA - Line";

        public Form1()
        {
            InitializeComponent();
            domainUpDown.Items.Add(BCircle);
            domainUpDown.Items.Add(BLine);
            domainUpDown.Items.Add(SLine);
            domainUpDown.Items.Add(DDALine);
            domainUpDown.SelectedIndex = 0;
            zedGraph.GraphPane.XAxis.MajorGrid.IsVisible = true;
            zedGraph.GraphPane.YAxis.MajorGrid.IsVisible = true;
        }
        public static void BDCircle(int x1, int y1, int r)
        {
            int x = 0;
            int y = r;
            PutCirclePixel(x1, y1, x, y);
            while (y > 0)
            {
                int xr = x + 1;
                int xv = x;
                int xd = x + 1;
                int yr = y;
                int yv = y - 1;
                int yd = y - 1;
                double diffR = Math.Abs(xr * xr + yr * yr - r * r);
                double diffV = Math.Abs(xv * xv + yv * yv - r * r);
                double diffD = Math.Abs(xd * xd + yd * yd - r * r);
                if (diffR < diffD && diffR < diffV)
                {
                    x = xr;
                    y = yr;
                    PutCirclePixel(x1, y1, x, y);
                }
                else if (diffV < diffD && diffV < diffD)
                {
                    x = xv;
                    y = yv;
                    PutCirclePixel(x1, y1, x, y);
                }
                else
                {
                    x = xd;
                    y = yd;
                    PutCirclePixel(x1, y1, x, y);
                }

            }
        }
        public static void BDLine(int x0, int y0, int x1, int y1)
        {
            var steep = Math.Abs(y1 - y0) > Math.Abs(x1 - x0);
            if (steep)
            {
                Swap(ref x0, ref y0);
                Swap(ref x1, ref y1);
            }
            if (x0 > x1)
            {
                Swap(ref x0, ref x1);
                Swap(ref y0, ref y1);
            }
            int dX = x1 - x0, dY = Math.Abs(y1 - y0), err = dX/2, ystep = y0 < y1 ? 1 : -1, y = y0;

            for (var x = x0; x <= x1; ++x)
            {
                if (steep)
                    PutPixel(y, x);
                else
                    PutPixel(x, y);
                err = err - dY;
                if (err < 0)
                {
                    y += ystep;
                    err += dX;
                }
            }
        }

        public static void StepLine(int x0, int y0, int x1, int y1)
        {
            var dxabs = Math.Abs(x0 - x1);
            var dyabs = Math.Abs(y0 - y1);
            if (dyabs > dxabs)
            {
                if (y1 < y0)
                {
                    Swap(ref x0, ref x1);
                    Swap(ref y0, ref y1);
                }

                var dx = x1 - x0;
                var dy = y1 - y0;

                for (var y = y0; y < y1; y++)
                {
                    var x = x0 + dx * (y - y0) / dy;
                    PutPixel(x, y);
                }
            }
            else
            {
                if (x0 > x1)
                {
                    Swap(ref x0, ref x1);
                    Swap(ref y0, ref y1);
                }
                var dx = x1 - x0;
                var dy = y1 - y0;

                for (var x = x0; x < x1; x++)
                {
                    var y = y0 + dy * (x - x0) / dx;
                    PutPixel(x, y);
                }
            }

        }

        private void DDADLine(int x0, int y0, int x1, int y1)
        {
            var dx = x1 - x0;
            var dy = y1 - y0;
            int step, k;
            float xInc, yInc, x = x0, y = y0;
            if (Math.Abs(dx) > Math.Abs(dy))
                step = Math.Abs(dx);
            else
                step = Math.Abs(dy);
            xInc = (float)dx / step;
            yInc = (float)dy / step;
            PutPixel((int)Math.Round(x), (int)Math.Round(y));
            for (k = 0; k < step; k++)
            {
                x += xInc;
                y += yInc;
                PutPixel((int)Math.Round(x), (int)Math.Round(y));
            }
        }
        private static bool PutPixel(int x0, int y0)
        {
            if ((x0 >= 0 && y0 >= 0))
            {
                DataList.Add(new Point(x0, y0));
            }
            return true;
        }
      
        private void trackBarScroll(object sender, EventArgs e)
        {
            if (sender == this.trackBarx1)
            {
                this.numericUpDownx1.Value = this.trackBarx1.Value;
            }
            if (sender == this.trackBarx2)
            {
                this.numericUpDownx2.Value = this.trackBarx2.Value;
            }
            if (sender == this.trackBary1)
            {
                this.numericUpDowny1.Value = this.trackBary1.Value;
            }
            if (sender == this.trackBary2)
            {
                this.numericUpDowny2.Value = this.trackBary2.Value;
            }
            if (sender == this.trackBarR)
            {
                this.numericUpDownR.Value = this.trackBarR.Value;
            }
            DataList.Clear();
            ShowPlot();
            DrawGraph();
        }
        public static void PutCirclePixel(int x0, int y0, int x, int y)
        {
            PutPixel(x0 + x, y0 + y);
            PutPixel(x0 + x, y0 - y);
            PutPixel(x0 - x, y0 - y);
            PutPixel(x0 - x, y0 + y);
        }

        private void ShowPlot()
        {
            switch (domainUpDown.Text)
            {
                case BLine:
                    BDLine(trackBarx1.Value, trackBary1.Value, trackBarx2.Value, trackBary2.Value);
                    break;
                case BCircle:
                    BDCircle(trackBarx1.Value, trackBary1.Value, trackBarR.Value);
                    break;
                case DDALine:
                    DDADLine(trackBarx1.Value, trackBary1.Value, trackBarx2.Value, trackBary2.Value);
                    break;
                case SLine:
                    StepLine(trackBarx1.Value, trackBary1.Value, trackBarx2.Value, trackBary2.Value);
                    break;
            };
        }

        private void DrawGraph()
        {
            var pane = zedGraph.GraphPane;
            pane.GraphObjList.Clear();
            int maxx=0, maxy = 0;
            foreach (var point in DataList)
            {
                maxx = Math.Max(maxx, point.X);
                maxy = Math.Max(maxy, point.Y);
               
                BoxObj boxObj = new BoxObj(point.X-0.5,point.Y+0.5,1,1,Color.Green,Color.Green);
                boxObj.IsVisible = true;
                boxObj.Location.CoordinateFrame = CoordType.AxisXYScale;
                boxObj.ZOrder = ZOrder.A_InFront;
                pane.GraphObjList.Add(boxObj);
            }
            pane.YAxis.Scale.Max = 100;
            pane.XAxis.Scale.Max = 100;

            zedGraph.AxisChange();
            zedGraph.Invalidate();
        }

        private static void Swap<T>(ref T lhs, ref T rhs)
        {
            T temp = lhs;
            lhs = rhs;
            rhs = temp;
        }

        private void numericUpDownx1_ValueChanged(object sender, EventArgs e)
        {
            this.trackBarx1.Value = (int)this.numericUpDownx1.Value;
            trackBarScroll(sender, e);
        }

        private void numericUpDowny1_ValueChanged(object sender, EventArgs e)
        {
            this.trackBary1.Value = (int)this.numericUpDowny1.Value;
            trackBarScroll(sender, e);
        }

        private void numericUpDownx2_ValueChanged(object sender, EventArgs e)
        {
            this.trackBarx2.Value = (int)this.numericUpDownx2.Value;
            trackBarScroll(sender, e);
        }

        private void numericUpDowny2_ValueChanged(object sender, EventArgs e)
        {
            this.trackBary2.Value = (int)this.numericUpDowny2.Value;
            trackBarScroll(sender, e);
        }

        private void numericUpDownR_ValueChanged(object sender, EventArgs e)
        {
            this.trackBarR.Value = (int)this.numericUpDownR.Value;
            trackBarScroll(sender, e);
        }
    }
}