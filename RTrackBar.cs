using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;

namespace RTheme
{
    public class RTrackBar : Control
    {
        public delegate void ValueChangedEventHandler();

        private static List<WeakReference> __ENCList = new List<WeakReference>();

        private int _Maximum;

        private int _Value;

        private bool CaptureMovement;

        private Rectangle Bar;

        private Size Track;

        private Color _TextColour;

        private Color _BorderColour;

        private Color _BarBaseColour;

        private Color _StripColour;

        private Color _StripAmountColour;

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
        public Color BarBaseColour
        {
            get
            {
                return _BarBaseColour;
            }
            set
            {
                _BarBaseColour = value;
            }
        }

        [Category("Colours")]
        public Color StripColour
        {
            get
            {
                return _StripColour;
            }
            set
            {
                _StripColour = value;
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
        public Color StripAmountColour
        {
            get
            {
                return _StripAmountColour;
            }
            set
            {
                _StripAmountColour = value;
            }
        }

        public int Maximum
        {
            get
            {
                return _Maximum;
            }
            set
            {
                if (value > 0)
                {
                    _Maximum = value;
                }
                if (value < _Value)
                {
                    _Value = value;
                }
                Invalidate();
            }
        }

        public int Value
        {
            get
            {
                return _Value;
            }
            set
            {
                if (value != _Value)
                {
                    if (value < 0)
                    {
                        _Value = 0;
                    }
                    else if (value > _Maximum)
                    {
                        _Value = _Maximum;
                    }
                    else
                    {
                        _Value = value;
                    }
                    Invalidate();
                    ValueChanged?.Invoke();
                }
            }
        }

        [method: DebuggerNonUserCode]
        public event ValueChangedEventHandler ValueChanged;

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

        protected override void OnHandleCreated(EventArgs e)
        {
            BackColor = Color.Transparent;
            base.OnHandleCreated(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            Point point = new Point(e.Location.X, e.Location.Y);
            Point location = point;
            Size size = new Size(1, 1);
            Rectangle rect = new Rectangle(location, size);
            checked
            {
                Rectangle rectangle = new Rectangle(10, 10, Width - 21, Height - 21);
                point = new Point(rectangle.X + (int)Math.Round((double)rectangle.Width * ((double)Value / (double)Maximum)) - (int)Math.Round((double)Track.Width / 2.0 - 1.0), 0);
                Point location2 = point;
                size = new Size(Track.Width, Height);
                Rectangle rectangle2 = new Rectangle(location2, size);
                Rectangle rectangle3 = rectangle2;
                if (rectangle3.IntersectsWith(rect))
                {
                    CaptureMovement = true;
                }
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            CaptureMovement = false;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            checked
            {
                if (CaptureMovement)
                {
                    Point point = new Point(e.X, e.Y);
                    Rectangle rectangle = new Rectangle(10, 10, Width - 21, Height - 21);
                    Value = (int)Math.Round((double)Maximum * ((double)(point.X - rectangle.X) / (double)rectangle.Width));
                }
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            CaptureMovement = false;
        }

        public RTrackBar()
        {
            __ENCAddToList(this);
            _Maximum = 10;
            _Value = 0;
            CaptureMovement = false;
            ref Rectangle bar = ref Bar;
            bar = checked(new Rectangle(0, 10, Width - 21, Height - 21));
            ref Size track = ref Track;
            track = new Size(25, 14);
            _TextColour = Color.FromArgb(255, 255, 255);
            _BorderColour = Color.FromArgb(35, 35, 35);
            _BarBaseColour = Color.FromArgb(47, 47, 47);
            _StripColour = Color.FromArgb(42, 42, 42);
            _StripAmountColour = Color.FromArgb(23, 119, 151);
            SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.Selectable | ControlStyles.SupportsTransparentBackColor | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, value: true);
            DoubleBuffered = true;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            Graphics graphics2 = graphics;
            graphics2.SmoothingMode = SmoothingMode.HighQuality;
            graphics2.PixelOffsetMode = PixelOffsetMode.HighQuality;
            graphics2.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            ref Rectangle bar = ref Bar;
            checked
            {
                bar = new Rectangle(13, 11, Width - 27, Height - 21);
                graphics2.Clear(Parent.FindForm().BackColor);
                graphics2.SmoothingMode = SmoothingMode.AntiAlias;
                graphics2.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
                Graphics graphics3 = graphics2;
                SolidBrush brush = new SolidBrush(_StripColour);
                Rectangle rect = new Rectangle(3, (int)Math.Round((double)Height / 2.0 - 4.0), Width - 5, 8);
                graphics3.FillRectangle(brush, rect);
                Graphics graphics4 = graphics2;
                Pen pen = new Pen(_BorderColour, 2f);
                rect = new Rectangle(4, (int)Math.Round((double)Height / 2.0 - 4.0), Width - 5, 8);
                graphics4.DrawRectangle(pen, rect);
                Graphics graphics5 = graphics2;
                SolidBrush brush2 = new SolidBrush(_StripAmountColour);
                rect = new Rectangle(4, (int)Math.Round((double)Height / 2.0 - 4.0), (int)Math.Round((double)Bar.Width * ((double)Value / (double)Maximum)) + (int)Math.Round((double)Track.Width / 2.0), 8);
                graphics5.FillRectangle(brush2, rect);
                graphics2.FillRectangle(new SolidBrush(_BarBaseColour), Bar.X + (int)Math.Round((double)Bar.Width * ((double)Value / (double)Maximum)) - (int)Math.Round((double)Track.Width / 2.0), Bar.Y + (int)Math.Round((double)Bar.Height / 2.0) - (int)Math.Round((double)Track.Height / 2.0), Track.Width, Track.Height);
                graphics2.DrawRectangle(new Pen(_BorderColour, 2f), Bar.X + (int)Math.Round((double)Bar.Width * ((double)Value / (double)Maximum)) - (int)Math.Round((double)Track.Width / 2.0), Bar.Y + (int)Math.Round((double)Bar.Height / 2.0) - (int)Math.Round((double)Track.Height / 2.0), Track.Width, Track.Height);
                Graphics graphics6 = graphics2;
                string s = Conversions.ToString(_Value);
                Font font = new Font("Segoe UI", 6.5f, FontStyle.Regular);
                SolidBrush brush3 = new SolidBrush(_TextColour);
                rect = new Rectangle(Bar.X + (int)Math.Round((double)Bar.Width * ((double)Value / (double)Maximum)) - (int)Math.Round((double)Track.Width / 2.0), Bar.Y + (int)Math.Round((double)Bar.Height / 2.0) - (int)Math.Round((double)Track.Height / 2.0), Track.Width - 1, Track.Height);
                graphics6.DrawString(s, font, brush3, rect, new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                });
                graphics2.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics2 = null;
            }
        }
    }
}