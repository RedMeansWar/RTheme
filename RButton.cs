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
    public class RButton : Control
    {
        private static List<WeakReference> __ENCList = new List<WeakReference>();

        private readonly Font _Font;

        private Color _ProgressColour;

        private Color _BorderColour;

        private Color _FontColour;

        private Color _MainColour;

        private Color _HoverColour;

        private Color _PressedColour;

        private DrawHelper.MouseState State;

        [Category("Colours")]
        public Color ProgressColour
        {
            get
            {
                return _ProgressColour;
            }
            set
            {
                _ProgressColour = value;
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
        public Color FontColour
        {
            get
            {
                return _FontColour;
            }
            set
            {
                _FontColour = value;
            }
        }

        [Category("Colours")]
        public Color BaseColour
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

        public RButton()
        {
            __ENCAddToList(this);
            _Font = new Font("Segoe UI", 9f);
            _ProgressColour = Color.FromArgb(0, 191, 255);
            _BorderColour = Color.FromArgb(25, 25, 25);
            _FontColour = Color.FromArgb(255, 255, 255);
            _MainColour = Color.FromArgb(42, 42, 42);
            _HoverColour = Color.FromArgb(52, 52, 52);
            _PressedColour = Color.FromArgb(47, 47, 47);
            State = default(DrawHelper.MouseState);
            SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, value: true);
            DoubleBuffered = true;
            Size size = new Size(75, 30);
            Size = size;
            BackColor = Color.Transparent;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            Graphics graphics2 = graphics;
            graphics2.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            graphics2.SmoothingMode = SmoothingMode.HighQuality;
            graphics2.PixelOffsetMode = PixelOffsetMode.HighQuality;
            graphics2.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics2.Clear(BackColor);
            checked
            {
                switch (unchecked((byte)State))
                {
                    case 0:
                        {
                            Graphics graphics9 = graphics2;
                            SolidBrush brush3 = new SolidBrush(_MainColour);
                            Rectangle rect = new Rectangle(0, 0, Width, Height);
                            graphics9.FillRectangle(brush3, rect);
                            Graphics graphics10 = graphics2;
                            Pen pen3 = new Pen(_BorderColour, 2f);
                            rect = new Rectangle(0, 0, Width, Height);
                            graphics10.DrawRectangle(pen3, rect);
                            Graphics graphics11 = graphics2;
                            string s3 = Text;
                            Font font3 = _Font;
                            Brush white3 = Brushes.White;
                            Point point = new Point((int)Math.Round((double)Width / 2.0), (int)Math.Round((double)Height / 2.0));
                            graphics11.DrawString(s3, font3, white3, point, new StringFormat
                            {
                                Alignment = StringAlignment.Center,
                                LineAlignment = StringAlignment.Center
                            });
                            break;
                        }
                    case 1:
                        {
                            Graphics graphics6 = graphics2;
                            SolidBrush brush2 = new SolidBrush(_HoverColour);
                            Rectangle rect = new Rectangle(0, 0, Width, Height);
                            graphics6.FillRectangle(brush2, rect);
                            Graphics graphics7 = graphics2;
                            Pen pen2 = new Pen(_BorderColour, 1f);
                            rect = new Rectangle(1, 1, Width - 2, Height - 2);
                            graphics7.DrawRectangle(pen2, rect);
                            Graphics graphics8 = graphics2;
                            string s2 = Text;
                            Font font2 = _Font;
                            Brush white2 = Brushes.White;
                            Point point = new Point((int)Math.Round((double)Width / 2.0), (int)Math.Round((double)Height / 2.0));
                            graphics8.DrawString(s2, font2, white2, point, new StringFormat
                            {
                                Alignment = StringAlignment.Center,
                                LineAlignment = StringAlignment.Center
                            });
                            break;
                        }
                    case 2:
                        {
                            Graphics graphics3 = graphics2;
                            SolidBrush brush = new SolidBrush(_PressedColour);
                            Rectangle rect = new Rectangle(0, 0, Width, Height);
                            graphics3.FillRectangle(brush, rect);
                            Graphics graphics4 = graphics2;
                            Pen pen = new Pen(_BorderColour, 1f);
                            rect = new Rectangle(1, 1, Width - 2, Height - 2);
                            graphics4.DrawRectangle(pen, rect);
                            Graphics graphics5 = graphics2;
                            string s = Text;
                            Font font = _Font;
                            Brush white = Brushes.White;
                            Point point = new Point((int)Math.Round((double)Width / 2.0), (int)Math.Round((double)Height / 2.0));
                            graphics5.DrawString(s, font, white, point, new StringFormat
                            {
                                Alignment = StringAlignment.Center,
                                LineAlignment = StringAlignment.Center
                            });
                            break;
                        }
                }
                graphics2 = null;
            }
        }
    }
}
