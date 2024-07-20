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
    public class RLogButton : Control
    {
        private static List<WeakReference> __ENCList = new List<WeakReference>();

        private DrawHelper.MouseState State;

        private Color _ArcColour;

        private Color _ArrowColour;

        private Color _ArrowBorderColour;

        private Color _BorderColour;

        private Color _HoverColour;

        private Color _PressedColour;

        private Color _NormalColour;

        [Category("Colours")]
        public Color ArcColour
        {
            get
            {
                return _ArcColour;
            }
            set
            {
                _ArcColour = value;
            }
        }

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
        public Color ArrowColour
        {
            get
            {
                return _ArrowColour;
            }
            set
            {
                _ArrowColour = value;
            }
        }

        [Category("Colours")]
        public Color ArrowBorderColour
        {
            get
            {
                return _ArrowBorderColour;
            }
            set
            {
                _ArrowBorderColour = value;
            }
        }

        [Category("Colours")]
        public Color HoverColour
        {
            get
            {
                return _HoverColour;
            }
            set
            {
                _HoverColour = value;
            }
        }

        [Category("Colours")]
        public Color PressedColour
        {
            get
            {
                return _PressedColour;
            }
            set
            {
                _PressedColour = value;
            }
        }

        [Category("Colours")]
        public Color NormalColour
        {
            get
            {
                return _NormalColour;
            }
            set
            {
                _NormalColour = value;
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

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            State = DrawHelper.MouseState.Down;
            Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            State = DrawHelper.MouseState.Over;
            Invalidate();
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            State = DrawHelper.MouseState.Over;
            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            State = DrawHelper.MouseState.None;
            Invalidate();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Size size = new Size(50, 50);
            Size = size;
        }

        public RLogButton()
        {
            __ENCAddToList(this);
            State = DrawHelper.MouseState.None;
            _ArcColour = Color.FromArgb(43, 43, 43);
            _ArrowColour = Color.FromArgb(235, 233, 234);
            _ArrowBorderColour = Color.FromArgb(170, 170, 170);
            _BorderColour = Color.FromArgb(35, 35, 35);
            _HoverColour = Color.FromArgb(0, 130, 169);
            _PressedColour = Color.FromArgb(0, 145, 184);
            _NormalColour = Color.FromArgb(0, 160, 199);
            SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, value: true);
            DoubleBuffered = true;
            Size size = new Size(50, 50);
            Size = size;
            BackColor = Color.Transparent;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            GraphicsPath graphicsPath = new GraphicsPath();
            GraphicsPath graphicsPath2 = new GraphicsPath();
            Graphics graphics2 = graphics;
            graphics2.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            graphics2.SmoothingMode = SmoothingMode.HighQuality;
            graphics2.PixelOffsetMode = PixelOffsetMode.HighQuality;
            graphics2.Clear(BackColor);
            Point[] array = new Point[7];
            ref Point reference = ref array[0];
            Point point = new Point(18, 22);
            reference = point;
            ref Point reference2 = ref array[1];
            Point point2 = new Point(28, 22);
            reference2 = point2;
            ref Point reference3 = ref array[2];
            Point point3 = new Point(28, 18);
            reference3 = point3;
            ref Point reference4 = ref array[3];
            Point point4 = new Point(34, 25);
            reference4 = point4;
            ref Point reference5 = ref array[4];
            Point point5 = new Point(28, 32);
            reference5 = point5;
            ref Point reference6 = ref array[5];
            Point point6 = new Point(28, 28);
            reference6 = point6;
            ref Point reference7 = ref array[6];
            Point point7 = new Point(18, 28);
            reference7 = point7;
            Point[] points = array;
            checked
            {
                switch (unchecked((byte)State))
                {
                    case 0:
                        {
                            Graphics graphics7 = graphics2;
                            SolidBrush brush3 = new SolidBrush(Color.FromArgb(56, 56, 56));
                            Rectangle rect = new Rectangle(3, 3, Width - 3 - 3, Height - 3 - 3);
                            graphics7.FillEllipse(brush3, rect);
                            graphics2.DrawArc(new Pen(new SolidBrush(_ArcColour), 4f), 3, 3, Width - 3 - 3, Height - 3 - 3, -90, 360);
                            Graphics graphics8 = graphics2;
                            Pen pen3 = new Pen(_BorderColour);
                            rect = new Rectangle(1, 1, Height - 3, Height - 3);
                            graphics8.DrawEllipse(pen3, rect);
                            Graphics graphics9 = graphics2;
                            SolidBrush brush4 = new SolidBrush(_NormalColour);
                            rect = new Rectangle(5, 5, Height - 11, Height - 11);
                            graphics9.FillEllipse(brush4, rect);
                            graphics2.FillPolygon(new SolidBrush(_ArrowColour), points);
                            graphics2.DrawPolygon(new Pen(_ArrowBorderColour), points);
                            break;
                        }
                    case 1:
                        {
                            graphics2.DrawArc(new Pen(new SolidBrush(_ArcColour), 4f), 3, 3, Width - 3 - 3, Height - 3 - 3, -90, 360);
                            Graphics graphics5 = graphics2;
                            Pen pen2 = new Pen(_BorderColour);
                            Rectangle rect = new Rectangle(1, 1, Height - 3, Height - 3);
                            graphics5.DrawEllipse(pen2, rect);
                            Graphics graphics6 = graphics2;
                            SolidBrush brush2 = new SolidBrush(_HoverColour);
                            rect = new Rectangle(6, 6, Height - 13, Height - 13);
                            graphics6.FillEllipse(brush2, rect);
                            graphics2.FillPolygon(new SolidBrush(_ArrowColour), points);
                            graphics2.DrawPolygon(new Pen(_ArrowBorderColour), points);
                            break;
                        }
                    case 2:
                        {
                            graphics2.DrawArc(new Pen(new SolidBrush(_ArcColour), 4f), 3, 3, Width - 3 - 3, Height - 3 - 3, -90, 360);
                            Graphics graphics3 = graphics2;
                            Pen pen = new Pen(_BorderColour);
                            Rectangle rect = new Rectangle(1, 1, Height - 3, Height - 3);
                            graphics3.DrawEllipse(pen, rect);
                            Graphics graphics4 = graphics2;
                            SolidBrush brush = new SolidBrush(_PressedColour);
                            rect = new Rectangle(6, 6, Height - 13, Height - 13);
                            graphics4.FillEllipse(brush, rect);
                            graphics2.FillPolygon(new SolidBrush(_ArrowColour), points);
                            graphics2.DrawPolygon(new Pen(_ArrowBorderColour), points);
                            break;
                        }
                }
                graphicsPath.Dispose();
                graphicsPath2.Dispose();
                graphics2.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics2 = null;
            }
        }
    }
}