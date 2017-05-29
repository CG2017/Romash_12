using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace lab7
{

    public static class MiddlePoi
    {
        private static OutCode CodeCounting(float x, float y, RectangleF rect)
        {
            var code = OutCode.Inside;

            if (x < rect.Left) code |= OutCode.Left;
            if (x > rect.Right) code |= OutCode.Right;
            if (y < rect.Top) code |= OutCode.Top;
            if (y > rect.Bottom) code |= OutCode.Bottom;

            return code;
        }

        private static OutCode CodeCounting(PointF poi, RectangleF rect)
        {
            return CodeCounting(poi.X, poi.Y, rect);
        }

        public static double PointsLength(PointF poi1, PointF poi2)
        {
            double dx = poi1.X - poi2.X;
            double dy = poi1.Y - poi2.Y;
            return Math.Sqrt(dx * dx + dy * dy);
        }

        public static PointF GetMiddlePointF(PointF p1, PointF p2)
        {
            return new PointF((p2.X + p1.X) / 2, (p2.Y + p1.Y) / 2);
        }


        internal static void ClipSegment(RectangleF bounds, PointF poi1, PointF poi2, List<Tuple<PointF, PointF>> newLines)
        {
            var codeP1 = CodeCounting(poi1, bounds);
            var codeP2 = CodeCounting(poi2, bounds);
            if (PointsLength(poi1, poi2) < 1)
            {
                return;
            }
            if ((codeP1 & codeP2) != 0)
            {
                return;
            }
            if ((codeP1 | codeP2) == OutCode.Inside)
            {
                newLines.Add(new Tuple<PointF, PointF>(poi1, poi2));
            }
            ClipSegment(bounds, poi1, GetMiddlePointF(poi1, poi2), newLines);
            ClipSegment(bounds, GetMiddlePointF(poi1, poi2), poi2, newLines);
        }


        [Flags]
        private enum OutCode
        {
            Inside = 0,
            Left = 1,
            Right = 2,
            Bottom = 4,
            Top = 8
        }
    }
    public struct Segm
    {
        public readonly PointF A, B;

        public Segm(PointF a, PointF b)
        {
            A = a;
            B = b;
        }
    }

    public class Poly : List<PointF>
    {
        public Poly() { }

        public Poly(int capacity): base(capacity) { }

        public Poly(IEnumerable<PointF> collection) : base(collection) { }

        public IEnumerable<Segm> Edges
        {
            get
            {
                if (Count >= 2)
                {
                    for (int a = Count - 1, b = 0; b < Count; a = b, ++b)
                    {
                        yield return new Segm(this[a], this[b]);
                    }
                }
            }
        }

        public IEnumerable<Segm> SazerlandHojman(RectangleF bound)
        {
            List<PointF> points = new List<PointF>();
            foreach (Segm seg in Edges)
            {
                bool startIn = IsInside(seg.A, bound);
                bool endIn = IsInside(seg.B, bound);
                if (startIn && endIn)
                {
                    points.Add(seg.A);
                }
                else if (startIn && !endIn)
                {
                    points.Add(seg.A);
                    var intersectP = CountIntersec(bound, seg.A, seg.B, CountCode(seg.B, bound));
                    points.Add(intersectP);
                }
                else if (!startIn && endIn)
                {
                    var intersectP = CountIntersec(bound, seg.A, seg.B, CountCode(seg.A, bound));
                    points.Add(intersectP);
                }
            }

            return new Poly(points).Edges;
        }

        private static PointF CountIntersec(RectangleF r, PointF poi1, PointF poi2, OutCode clipTo)
        {
            var dx = poi2.X - poi1.X;
            var dy = poi2.Y - poi1.Y;

            var slopeY = dx / dy; // slope to use for possibly-vertical lines
            var slopeX = dy / dx; // slope to use for possibly-horizontal lines

            if (clipTo.HasFlag(OutCode.Top))
                return new PointF(poi1.X + slopeY * (r.Top - poi1.Y),r.Top);
            
            if (clipTo.HasFlag(OutCode.Bottom))
                return new PointF(poi1.X + slopeY * (r.Bottom - poi1.Y),r.Bottom);

            if (clipTo.HasFlag(OutCode.Right))
                return new PointF(r.Right,poi1.Y + slopeX * (r.Right - poi1.X));

            if (clipTo.HasFlag(OutCode.Left))
                return new PointF(r.Left,poi1.Y + slopeX * (r.Left - poi1.X));
            throw new ArgumentOutOfRangeException("clipTo = " + clipTo);
        }

        private static bool IsInside(PointF poi, RectangleF bound)
        {
            return CountCode(poi, bound) == OutCode.Inside;
        }

        private static OutCode CountCode(float x, float y, RectangleF rect)
        {
            var code = OutCode.Inside;

            if (x < rect.Left) code |= OutCode.Left;
            if (x > rect.Right) code |= OutCode.Right;
            if (y < rect.Top) code |= OutCode.Top;
            if (y > rect.Bottom) code |= OutCode.Bottom;

            return code;
        }

        private static OutCode CountCode(PointF p, RectangleF r) =>  CountCode(p.X, p.Y, r);

        [Flags]
        private enum OutCode
        {
            Inside = 0,
            Left = 1,
            Right = 2,
            Bottom = 4,
            Top = 8
        }
    }
    public partial class Form1 : Form
    {
        private RectangleF _bounds;

        private List<Tuple<PointF, PointF>> _lines = new List<Tuple<PointF, PointF>>();
        private List<Tuple<PointF, PointF>> tab3Lines = new List<Tuple<PointF, PointF>>();
        /* {
                        new PointF(100,100),new PointF(150,100),new PointF(220,200),new PointF(200,200)
                    };*/
        private PointF? _prevPoint;
        private readonly List<PointF> _boundsList = new List<PointF>();

        private List<Tuple<PointF, PointF>> boundaryList = new List<Tuple<PointF, PointF>>();

        public Form1()
        {
            InitializeComponent();
            ReadFile();
        }

        private void ReadFile()
        {
            int n;
            var counter = 0;
            using (TextReader reader = File.OpenText(@"input.txt"))
            {
                string line;
                int count = int.Parse(reader.ReadLine());
                for (int i = 0; i < count; i++)
                {
                    var lines = reader.ReadLine().Split(' ');
                    _lines.Add(
                                new Tuple<PointF, PointF>(
                                    new PointF(Convert.ToSingle(lines[0]), Convert.ToSingle(lines[1])),
                                    new PointF(Convert.ToSingle(lines[2]), Convert.ToSingle(lines[3]))));
                }
                while ((line = reader.ReadLine()) != null)
                {
                    var lines = line.Split(' ');
                    if (lines[0].Contains("rom"))
                    {
                        _boundsList.Add(new PointF(Convert.ToSingle(lines[1]), Convert.ToSingle(lines[2])));
                    }
                    else
                    {
                        _bounds = new RectangleF(Convert.ToSingle(lines[0]), Convert.ToSingle(lines[1]),
                            Convert.ToSingle(lines[2]), Convert.ToSingle(lines[3]));
                        boundaryList.Add(new Tuple<PointF, PointF>(
                            new PointF(_bounds.Left, _bounds.Top), new PointF(_bounds.Right, _bounds.Top)));
                        boundaryList.Add(new Tuple<PointF, PointF>(
                            new PointF(_bounds.Right, _bounds.Top), new PointF(_bounds.Right, _bounds.Bottom)));
                        boundaryList.Add(new Tuple<PointF, PointF>(
                            new PointF(_bounds.Right, _bounds.Bottom), new PointF(_bounds.Left, _bounds.Bottom)));
                        boundaryList.Add(new Tuple<PointF, PointF>(
                            new PointF(_bounds.Left, _bounds.Bottom), new PointF(_bounds.Left, _bounds.Top)));
                    }
                }
            }
        }


        private void btnClearLines_Click(object sender, EventArgs e)
        {
            _lines = new List<Tuple<PointF, PointF>>();
            _prevPoint = null;
            tabPage1.Invalidate();
            tabPage2.Invalidate();
            tabPage3.Invalidate();
        }


        private void btnClipLines_Click(object sender, EventArgs e)
        {
            var newLines = new List<Tuple<PointF, PointF>>();
            if (tabControl1.SelectedIndex == 2)
            {
                var polygon = new Poly(_boundsList);

                var newPolygonSegments = polygon.SazerlandHojman(_bounds);
                tab3Lines = new List<Tuple<PointF, PointF>>();
                foreach (var newSegment in newPolygonSegments)
                {
                    tab3Lines.Add(new Tuple<PointF, PointF>(newSegment.A, newSegment.B));
                }
            }
            else
                foreach (var line in _lines)
                {
                    var p1 = line.Item1;
                    var p2 = line.Item2;
                    if (tabControl1.SelectedIndex == 0)
                    {
                        var clipped = LiangBarskiy.ClipSegment(_bounds, p1, p2);
                        if (clipped != null)
                        {
                            newLines.Add(clipped);
                        }
                    }
                    if (tabControl1.SelectedIndex == 1)
                    {
                        MiddlePoi.ClipSegment(_bounds, p1, p2, newLines);
                    }
                }
            _lines = newLines;
            tabPage1.Invalidate();
            tabPage2.Invalidate();
            tabPage3.Invalidate();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            _lines.Clear();
            ReadFile();
        }

        private void readButton_Click(object sender, EventArgs e)
        {
            _lines.Clear();
            ReadFile();
            tabPage1.Invalidate();
            tabPage2.Invalidate();
            tabPage3.Invalidate();
        }

        private void tabPage1_Paint(object sender, PaintEventArgs e)
        {
            var width = tabPage1.ClientSize.Width;
            var height = tabPage1.ClientSize.Height;
            var heightIn = _bounds.Height;
            var widthIn = _bounds.Width;
            var widthBound = _bounds.X; 
            var heightBound = _bounds.Y; 


            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            g.DrawLine(Pens.Indigo, widthBound, 0, widthBound, height);
            g.DrawLine(Pens.Indigo, widthIn + widthBound, 0, widthIn + widthBound, height);
            g.DrawLine(Pens.Indigo, 0, heightBound, width, heightBound);
            g.DrawLine(Pens.Indigo, 0, heightIn + heightBound, width, heightIn + heightBound);

            var rects = new[]
            {
                new RectangleF(0, 0, widthBound, heightBound),
                new RectangleF(widthBound, 0, widthIn, heightBound),
                new RectangleF(widthIn + widthBound, 0, widthBound, heightBound),
                new RectangleF(0, heightBound, widthBound, heightIn),
                new RectangleF(widthBound, heightBound, widthIn, heightIn),
                new RectangleF(widthIn + widthBound, heightBound, widthBound, heightIn),
                new RectangleF(0, heightIn + heightBound, widthBound, heightBound),
                new RectangleF(widthBound, heightIn + heightBound, widthIn, heightBound),
                new RectangleF(widthIn + widthBound, heightIn + heightBound, widthBound, heightBound)
            };
            var codes = new[]
            {
                "Left|Top", "Top", "Right|Top",
                "Left", "Inside", "Right",
                "Left|Bottom", "Bottom", "Right|Bottom"
            };

            for (var i = 0; i < codes.Length; i++)
            {
                var brush = Brushes.Black;
                g.DrawString(codes[i], DefaultFont, brush, rects[i],
                    new StringFormat {Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center});
            }


            foreach (var line in _lines)
            {
                g.DrawLine(new Pen(Color.Black, 1), line.Item1, line.Item2);
            }
        }

        private void tabPage1_MouseClick(object sender, MouseEventArgs e)
        {
            var position = e.Location;
            if (_prevPoint == null)
            {
                _prevPoint = position;
            }
            else
            {
                var line = new Tuple<PointF, PointF>(_prevPoint.Value, position);
                _lines.Add(line);
                _prevPoint = null;
                tabPage1.Invalidate();
            }
        }

        private void tabPage2_Paint(object sender, PaintEventArgs e)
        {
            var width = tabPage2.ClientSize.Width;
            var height = tabPage2.ClientSize.Height;
            var heightIn = _bounds.Height;
            var widthIn = _bounds.Width;
            var widthBound = _bounds.X; 
            var heightBound = _bounds.Y;


            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            g.DrawLine(Pens.Indigo, widthBound, 0, widthBound, height);
            g.DrawLine(Pens.Indigo, widthIn + widthBound, 0, widthIn + widthBound, height);
            g.DrawLine(Pens.Indigo, 0, heightBound, width, heightBound);
            g.DrawLine(Pens.Indigo, 0, heightIn + heightBound, width, heightIn + heightBound);

            var rects = new[]
            {
                new RectangleF(0, 0, widthBound, heightBound),
                new RectangleF(widthBound, 0, widthIn, heightBound),
                new RectangleF(widthIn + widthBound, 0, widthBound, heightBound),
                new RectangleF(0, heightBound, widthBound, heightIn),
                new RectangleF(widthBound, heightBound, widthIn, heightIn),
                new RectangleF(widthIn + widthBound, heightBound, widthBound, heightIn),
                new RectangleF(0, heightIn + heightBound, widthBound, heightBound),
                new RectangleF(widthBound, heightIn + heightBound, widthIn, heightBound),
                new RectangleF(widthIn + widthBound, heightIn + heightBound, widthBound, heightBound)
            };
            var codes = new[]
            {
                "Left|Top", "Top", "Right|Top",
                "Left", "Inside", "Right",
                "Left|Bottom", "Bottom", "Right|Bottom"
            };

            for (var i = 0; i < codes.Length; i++)
            {
                var brush = Brushes.Black;
                g.DrawString(codes[i], DefaultFont, brush, rects[i],
                    new StringFormat {Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center});
            }


            foreach (var line in _lines)
            {
                g.DrawLine(new Pen(Color.Black, 1), line.Item1, line.Item2);
            }
        }

        private void tabPage2_MouseClick(object sender, MouseEventArgs e)
        {
            var position = e.Location;
            if (_prevPoint == null)
            {
                _prevPoint = position;
            }
            else
            {
                var line = new Tuple<PointF, PointF>(_prevPoint.Value, position);
                _lines.Add(line);
                _prevPoint = null;
                tabPage2.Invalidate();
            }
        }

        private void tabPage3_MouseClick(object sender, MouseEventArgs e)
        {
            var position = e.Location;
            if (_prevPoint == null)
            {
                _prevPoint = position;
            }
            else
            {
                var line = new Tuple<PointF, PointF>(_prevPoint.Value, position);
                _lines.Add(line);
                _prevPoint = null;
                tabPage3.Invalidate();
            }
        }

        private void tabPage3_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            for (var i = 0; i < _boundsList.Count - 1; i++)
            {
                g.DrawLine(new Pen(Color.Aqua, 2), _boundsList[i], _boundsList[i + 1]);
            }
            g.DrawLine(new Pen(Color.Aqua, 2), _boundsList.Last(), _boundsList.First());

            foreach (var line in boundaryList)
            {
                g.DrawLine(new Pen(Color.Aquamarine, 1), line.Item1, line.Item2);
            }

            foreach (var line in tab3Lines)
            {
                g.DrawLine(new Pen(Color.MediumAquamarine, 4), line.Item1, line.Item2);
            }
        }
    }

    public static class LiangBarskiy
    {
        public static Tuple<PointF, PointF> ClipSegment(RectangleF r, PointF P1, PointF P2)
        {
            var dx = P2.X - P1.X;
            var dy = P2.Y - P1.Y;

            float t_In = 0;
            float t_Out = 1;

            Action recount = () =>
            {
                if (t_In > 0)
                {
                    P1.X = P1.X + dx * t_In;
                    P1.Y = P1.Y + dy * t_In;
                }
                if (t_Out < 1)
                {
                    P2.X = P1.X + dx * t_Out;
                    P2.Y = P1.Y + dy * t_Out;
                }
                dx = P2.X - P1.X;
                dy = P2.Y - P1.Y;
                t_In = 0;
                t_Out = 1;
            };

            // Clip_t может модифицировать t_In и t_Out
            // (ref  означает передачу по адресу)
            if (Clip_t(dx, r.Left - P1.X, ref t_In, ref t_Out))
            {
                recount();
                if (Clip_t(-dx, P1.X - r.Right, ref t_In, ref t_Out))
                {
                    recount();
                    if (Clip_t(dy, r.Top - P1.Y, ref t_In, ref t_Out))
                    {
                        recount();
                        if (Clip_t(-dy, P1.Y - r.Bottom, ref t_In, ref t_Out))
                        {
                            recount();
                            return new Tuple<PointF, PointF>(P1, P2);
                        }
                    }
                }
            }
            return null;
        }
        static bool Clip_t(float denom, float num, ref float t_In, ref float t_Out)
        {
            float t;
            if (denom > 0)
            {
                t = num / denom;
                if (t > t_Out)
                    return false;
                if (t > t_In) 
                    t_In = t;
            }
            else if (denom < 0)
            {
                t = num / denom;
                if (t < t_In)
                    return false;
                if (t < t_Out) 
                    t_Out = t;
            }
            else if (num > 0)
                return false;

            return true;
        }
    }
}