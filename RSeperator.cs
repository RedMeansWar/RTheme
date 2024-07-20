using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace RTheme
{
    public class RSeperator : Control
    {
        public enum Style
        {
            Horizontal,
            Verticle
        }

        private static List<WeakReference> __ENCList = new List<WeakReference>();

        private Color _SeperatorColour;

        private Style _Alignment;

        private float _Thickness;

        [Category("Control")]
        public float Thickness
        {
            get
            {
                return _Thickness;
            }
            set
            {
                _Thickness = value;
            }
        }

        [Category("Control")]
        public Style Alignment
        {
            get
            {
                return _Alignment;
            }
            set
            {
                _Alignment = value;
            }
        }

        [Category("Colours")]
        public Color SeperatorColour
        {
            get
            {
                return _SeperatorColour;
            }
            set
            {
                _SeperatorColour = value;
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

        public RSeperator()
        {
            __ENCAddToList(this);
            _SeperatorColour = Color.FromArgb(35, 35, 35);
            _Alignment = Style.Horizontal;
            _Thickness = 1f;
            SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, value: true);
            DoubleBuffered = true;
            BackColor = Color.Transparent;
            Size size = new Size(20, 20);
            Size = size;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            checked
            {
                Rectangle rectangle = new Rectangle(0, 0, Width - 1, Height - 1);
                Graphics graphics2 = graphics;
                graphics2.SmoothingMode = SmoothingMode.HighQuality;
                graphics2.PixelOffsetMode = PixelOffsetMode.HighQuality;
                switch (unchecked((int)_Alignment))
                {
                    case 0:
                        {
                            Graphics graphics4 = graphics2;
                            Pen pen2 = new Pen(_SeperatorColour, _Thickness);
                            Point pt2 = new Point(0, (int)Math.Round((double)Height / 2.0));
                            Point pt3 = pt2;
                            Point point = new Point(Width, (int)Math.Round((double)Height / 2.0));
                            graphics4.DrawLine(pen2, pt3, point);
                            break;
                        }
                    case 1:
                        {
                            Graphics graphics3 = graphics2;
                            Pen pen = new Pen(_SeperatorColour, _Thickness);
                            Point point = new Point((int)Math.Round((double)Width / 2.0), 0);
                            Point pt = point;
                            Point pt2 = new Point((int)Math.Round((double)Width / 2.0), Height);
                            graphics3.DrawLine(pen, pt, pt2);
                            break;
                        }
                }
                graphics2.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics2 = null;
            }
        }
    }
}