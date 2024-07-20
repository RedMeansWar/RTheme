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
    #pragma warning disable
    public class RNumeric : Control
    {
        private static List<WeakReference> __ENCList = new List<WeakReference>();

        private DrawHelper.MouseState State;

        private int MouseXLoc;

        private int MouseYLoc;

        private long _Value;

        private long _Minimum;

        private long _Maximum;

        private bool BoolValue;

        private Color _BaseColour;

        private Color _ButtonColour;

        private Color _BorderColour;

        private Color _SecondBorderColour;

        private Color _FontColour;

        public long Value
        {
            get
            {
                return _Value;
            }
            set
            {
                if ((value <= _Maximum) & (value >= _Minimum))
                {
                    _Value = value;
                }
                Invalidate();
            }
        }

        public long Maximum
        {
            get
            {
                return _Maximum;
            }
            set
            {
                if (value > _Minimum)
                {
                    _Maximum = value;
                }
                if (_Value > _Maximum)
                {
                    _Value = _Maximum;
                }
                Invalidate();
            }
        }

        public long Minimum
        {
            get
            {
                return _Minimum;
            }
            set
            {
                if (value < _Maximum)
                {
                    _Minimum = value;
                }
                if (_Value < _Minimum)
                {
                    _Value = Minimum;
                }
                Invalidate();
            }
        }

        [Category("Colours")]
        public Color BaseColour
        {
            get
            {
                return _BaseColour;
            }
            set
            {
                _BaseColour = value;
            }
        }

        [Category("Colours")]
        public Color ButtonColour
        {
            get
            {
                return _ButtonColour;
            }
            set
            {
                _ButtonColour = value;
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
        public Color SecondBorderColour
        {
            get
            {
                return _SecondBorderColour;
            }
            set
            {
                _SecondBorderColour = value;
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

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            MouseXLoc = e.Location.X;
            MouseYLoc = e.Location.Y;
            Invalidate();
            if (e.X < checked(Width - 47))
            {
                Cursor = Cursors.IBeam;
            }
            else
            {
                Cursor = Cursors.Hand;
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            checked
            {
                if ((MouseXLoc > Width - 47 && MouseXLoc < Width - 3) ? true : false)
                {
                    if (MouseXLoc < Width - 23)
                    {
                        if (Value + 1 <= _Maximum)
                        {
                            _Value++;
                        }
                    }
                    else if (Value - 1 >= _Minimum)
                    {
                        _Value--;
                    }
                }
                else
                {
                    BoolValue = !BoolValue;
                    Focus();
                }
                Invalidate();
            }
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);
            try
            {
                if (BoolValue)
                {
                    _Value = Conversions.ToLong(Conversions.ToString(_Value) + e.KeyChar);
                }
                if (_Value > _Maximum)
                {
                    _Value = _Maximum;
                }
                Invalidate();
            }
            catch (Exception projectError)
            {
                ProjectData.SetProjectError(projectError);
                ProjectData.ClearProjectError();
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (e.KeyCode == Keys.Back)
            {
                Value = 0L;
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Height = 24;
        }

        public RNumeric()
        {
            __ENCAddToList(this);
            State = DrawHelper.MouseState.None;
            _Minimum = 0L;
            _Maximum = 9999999L;
            _BaseColour = Color.FromArgb(42, 42, 42);
            _ButtonColour = Color.FromArgb(47, 47, 47);
            _BorderColour = Color.FromArgb(35, 35, 35);
            _SecondBorderColour = Color.FromArgb(0, 191, 255);
            _FontColour = Color.FromArgb(255, 255, 255);
            SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, value: true);
            DoubleBuffered = true;
            Font = new Font("Segoe UI", 10f);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            Rectangle rect = new Rectangle(0, 0, Width, Height);
            StringFormat stringFormat = new StringFormat();
            stringFormat.LineAlignment = StringAlignment.Center;
            stringFormat.Alignment = StringAlignment.Center;
            StringFormat stringFormat2 = stringFormat;
            Graphics graphics2 = graphics;
            graphics2.SmoothingMode = SmoothingMode.HighQuality;
            graphics2.PixelOffsetMode = PixelOffsetMode.HighQuality;
            graphics2.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            graphics2.Clear(BackColor);
            graphics2.FillRectangle(new SolidBrush(_BaseColour), rect);
            Graphics graphics3 = graphics2;
            SolidBrush brush = new SolidBrush(_ButtonColour);
            checked
            {
                Rectangle rect2 = new Rectangle(Width - 48, 0, 48, Height);
                graphics3.FillRectangle(brush, rect2);
                graphics2.DrawRectangle(new Pen(_BorderColour, 2f), rect);
                Graphics graphics4 = graphics2;
                Pen pen = new Pen(_SecondBorderColour);
                Point point = new Point(Width - 48, 1);
                Point pt = point;
                Point pt2 = new Point(Width - 48, Height - 2);
                graphics4.DrawLine(pen, pt, pt2);
                Graphics graphics5 = graphics2;
                Pen pen2 = new Pen(_BorderColour);
                pt2 = new Point(Width - 24, 1);
                Point pt3 = pt2;
                point = new Point(Width - 24, Height - 2);
                graphics5.DrawLine(pen2, pt3, point);
                Graphics graphics6 = graphics2;
                Pen pen3 = new Pen(_FontColour);
                pt2 = new Point(Width - 36, 7);
                Point pt4 = pt2;
                point = new Point(Width - 36, 17);
                graphics6.DrawLine(pen3, pt4, point);
                Graphics graphics7 = graphics2;
                Pen pen4 = new Pen(_FontColour);
                pt2 = new Point(Width - 31, 12);
                Point pt5 = pt2;
                point = new Point(Width - 41, 12);
                graphics7.DrawLine(pen4, pt5, point);
                Graphics graphics8 = graphics2;
                Pen pen5 = new Pen(_FontColour);
                pt2 = new Point(Width - 17, 13);
                Point pt6 = pt2;
                point = new Point(Width - 7, 13);
                graphics8.DrawLine(pen5, pt6, point);
                Graphics graphics9 = graphics2;
                string s = Conversions.ToString(Value);
                Font font = Font;
                SolidBrush brush2 = new SolidBrush(_FontColour);
                rect2 = new Rectangle(5, 1, Width, Height);
                graphics9.DrawString(s, font, brush2, rect2, new StringFormat
                {
                    LineAlignment = StringAlignment.Center
                });
                graphics2.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics2 = null;
            }
        }
    }
}