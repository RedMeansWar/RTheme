using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace RTheme
{
    public class RGroupBox : ContainerControl
    {
        private static List<WeakReference> __ENCList = new List<WeakReference>();

        private Color _MainColour;

        private Color _HeaderColour;

        private Color _TextColour;

        private Color _BorderColour;

        [Category("Colours")]
        public Color BorderColour
        {
            get
            {
                return _BorderColour;
            }
            set
            {
                _BorderColour = value;
            }
        }

        [Category("Colours")]
        public Color TextColour
        {
            get
            {
                return _TextColour;
            }
            set
            {
                _TextColour = value;
            }
        }

        [Category("Colours")]
        public Color HeaderColour
        {
            get
            {
                return _HeaderColour;
            }
            set
            {
                _HeaderColour = value;
            }
        }

        [Category("Colours")]
        public Color MainColour
        {
            get
            {
                return _MainColour;
            }
            set
            {
                _MainColour = value;
            }
        }

        [DebuggerNonUserCode]
        private static void __ENCAddToList(object value)
        {
            checked
            {
                lock (__ENCList)
                {
                    if (__ENCList.Count == __ENCList.Capacity)
                    {
                        int num = 0;
                        int num2 = __ENCList.Count - 1;
                        int num3 = 0;
                        while (true)
                        {
                            int num4 = num3;
                            int num5 = num2;
                            if (num4 > num5)
                            {
                                break;
                            }
                            WeakReference weakReference = __ENCList[num3];
                            if (weakReference.IsAlive)
                            {
                                if (num3 != num)
                                {
                                    __ENCList[num] = __ENCList[num3];
                                }
                                num++;
                            }
                            num3++;
                        }
                        __ENCList.RemoveRange(num, __ENCList.Count - num);
                        __ENCList.Capacity = __ENCList.Count;
                    }
                    __ENCList.Add(new WeakReference(RuntimeHelpers.GetObjectValue(value)));
                }
            }
        }

        public RGroupBox()
        {
            __ENCAddToList(this);
            _MainColour = Color.FromArgb(47, 47, 47);
            _HeaderColour = Color.FromArgb(42, 42, 42);
            _TextColour = Color.FromArgb(255, 255, 255);
            _BorderColour = Color.FromArgb(35, 35, 35);
            SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, value: true);
            DoubleBuffered = true;
            Size size = new Size(160, 110);
            Size = size;
            Font = new Font("Segoe UI", 10f, FontStyle.Bold);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            Graphics graphics2 = graphics;
            graphics2.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            graphics2.SmoothingMode = SmoothingMode.HighQuality;
            graphics2.PixelOffsetMode = PixelOffsetMode.HighQuality;
            graphics2.Clear(Color.FromArgb(54, 54, 54));
            Graphics graphics3 = graphics2;
            SolidBrush brush = new SolidBrush(_MainColour);
            Rectangle rect = new Rectangle(0, 28, Width, Height);
            graphics3.FillRectangle(brush, rect);
            Graphics graphics4 = graphics2;
            SolidBrush brush2 = new SolidBrush(_HeaderColour);
            checked
            {
                rect = new Rectangle(0, 0, (int)Math.Round(graphics2.MeasureString(Text, Font).Width + 7f), 28);
                graphics4.FillRectangle(brush2, rect);
                Graphics graphics5 = graphics2;
                string s = Text;
                Font font = Font;
                SolidBrush brush3 = new SolidBrush(_TextColour);
                Point point = new Point(5, 5);
                graphics5.DrawString(s, font, brush3, point);
                Point[] array = new Point[7];
                ref Point reference = ref array[0];
                point = new Point(0, 0);
                reference = point;
                ref Point reference2 = ref array[1];
                Point point2 = new Point((int)Math.Round(graphics2.MeasureString(Text, Font).Width + 7f), 0);
                reference2 = point2;
                ref Point reference3 = ref array[2];
                Point point3 = new Point((int)Math.Round(graphics2.MeasureString(Text, Font).Width + 7f), 28);
                reference3 = point3;
                ref Point reference4 = ref array[3];
                Point point4 = new Point(Width - 1, 28);
                reference4 = point4;
                ref Point reference5 = ref array[4];
                Point point5 = new Point(Width - 1, Height - 1);
                reference5 = point5;
                ref Point reference6 = ref array[5];
                Point pt = (reference6 = new Point(1, Height - 1));
                ref Point reference7 = ref array[6];
                Point point6 = new Point(1, 1);
                reference7 = point6;
                Point[] points = array;
                graphics2.DrawLines(new Pen(_BorderColour), points);
                Graphics graphics6 = graphics2;
                Pen pen = new Pen(_BorderColour, 2f);
                point6 = new Point(0, 28);
                Point pt2 = point6;
                pt = new Point((int)Math.Round(graphics2.MeasureString(Text, Font).Width + 7f), 28);
                graphics6.DrawLine(pen, pt2, pt);
                graphics2.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics2 = null;
            }
        }
    }
}