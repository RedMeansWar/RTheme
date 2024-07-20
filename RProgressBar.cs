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
    public class RProgressBar : Control
    {
        private static List<WeakReference> __ENCList = new List<WeakReference>();

        private Color _ProgressColour;

        private Color _BorderColour;

        private Color _BaseColour;

        private Color _FontColour;

        private Color _SecondColour;

        private int _Value;

        private int _Maximum;

        private bool _TwoColour;

        public Color SecondColour
        {
            get
            {
                return _SecondColour;
            }
            set
            {
                _SecondColour = value;
            }
        }

        [Category("Control")]
        public bool TwoColour
        {
            get
            {
                return _TwoColour;
            }
            set
            {
                _TwoColour = value;
            }
        }

        [Category("Control")]
        public int Maximum
        {
            get
            {
                return _Maximum;
            }
            set
            {
                if (value < _Value)
                {
                    _Value = value;
                }
                _Maximum = value;
                Invalidate();
            }
        }

        [Category("Control")]
        public int Value
        {
            get
            {
                if (_Value == 0)
                {
                    return 0;
                }
                return _Value;
            }
            set
            {
                int num = value;
                if (num > _Maximum)
                {
                    value = _Maximum;
                    Invalidate();
                }
                _Value = value;
                Invalidate();
            }
        }

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

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Height = 25;
        }

        protected override void CreateHandle()
        {
            base.CreateHandle();
            Height = 25;
        }

        public void Increment(int Amount)
        {
            checked
            {
                Value += Amount;
            }
        }

        public RProgressBar()
        {
            __ENCAddToList(this);
            _ProgressColour = Color.FromArgb(0, 160, 199);
            _BorderColour = Color.FromArgb(35, 35, 35);
            _BaseColour = Color.FromArgb(42, 42, 42);
            _FontColour = Color.FromArgb(50, 50, 50);
            _SecondColour = Color.FromArgb(0, 145, 184);
            _Value = 0;
            _Maximum = 100;
            _TwoColour = true;
            SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, value: true);
            DoubleBuffered = true;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            Rectangle rect = new Rectangle(0, 0, Width, Height);
            Graphics graphics2 = graphics;
            graphics2.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            graphics2.SmoothingMode = SmoothingMode.HighQuality;
            graphics2.PixelOffsetMode = PixelOffsetMode.HighQuality;
            graphics2.Clear(BackColor);
            checked
            {
                int num = (int)Math.Round((double)_Value / (double)_Maximum * (double)Width);
                int value = Value;
                if (value == 0)
                {
                    graphics2.FillRectangle(new SolidBrush(_BaseColour), rect);
                    Graphics graphics3 = graphics2;
                    SolidBrush brush = new SolidBrush(_ProgressColour);
                    Rectangle rect2 = new Rectangle(0, 0, num - 1, Height);
                    graphics3.FillRectangle(brush, rect2);
                    graphics2.DrawRectangle(new Pen(_BorderColour, 3f), rect);
                }
                else if (value == _Maximum)
                {
                    graphics2.FillRectangle(new SolidBrush(_BaseColour), rect);
                    Graphics graphics4 = graphics2;
                    SolidBrush brush2 = new SolidBrush(_ProgressColour);
                    Rectangle rect2 = new Rectangle(0, 0, num - 1, Height);
                    graphics4.FillRectangle(brush2, rect2);
                    if (_TwoColour)
                    {
                        rect2 = new Rectangle(0, -10, (int)Math.Round((double)(Width * _Value) / (double)_Maximum - 1.0), Height - 5);
                        graphics.SetClip(rect2);
                        double num2 = (double)((Width - 1) * _Maximum) / (double)_Value;
                        double num3 = 0.0;
                        while (true)
                        {
                            double num4 = num3;
                            double num5 = num2;
                            if (!(num4 <= num5))
                            {
                                break;
                            }
                            Pen pen = new Pen(new SolidBrush(_SecondColour), 7f);
                            Point point = new Point((int)Math.Round(num3), 0);
                            Point pt = point;
                            Point pt2 = new Point((int)Math.Round(num3 - 15.0), Height);
                            graphics.DrawLine(pen, pt, pt2);
                            num3 += 25.0;
                        }
                        graphics.ResetClip();
                    }
                    graphics2.DrawRectangle(new Pen(_BorderColour, 3f), rect);
                }
                else
                {
                    graphics2.FillRectangle(new SolidBrush(_BaseColour), rect);
                    Graphics graphics5 = graphics2;
                    SolidBrush brush3 = new SolidBrush(_ProgressColour);
                    Rectangle rect2 = new Rectangle(0, 0, num - 1, Height);
                    graphics5.FillRectangle(brush3, rect2);
                    if (_TwoColour)
                    {
                        Graphics graphics6 = graphics2;
                        rect2 = new Rectangle(0, 0, (int)Math.Round((double)(Width * _Value) / (double)_Maximum - 1.0), Height - 1);
                        graphics6.SetClip(rect2);
                        double num6 = (double)((Width - 1) * _Maximum) / (double)_Value;
                        double num7 = 0.0;
                        while (true)
                        {
                            double num8 = num7;
                            double num5 = num6;
                            if (!(num8 <= num5))
                            {
                                break;
                            }
                            Graphics graphics7 = graphics2;
                            Pen pen2 = new Pen(new SolidBrush(_SecondColour), 7f);
                            Point pt2 = new Point((int)Math.Round(num7), 0);
                            Point pt3 = pt2;
                            Point point = new Point((int)Math.Round(num7 - 10.0), Height);
                            graphics7.DrawLine(pen2, pt3, point);
                            num7 += 25.0;
                        }
                        graphics2.ResetClip();
                    }
                    graphics2.DrawRectangle(new Pen(_BorderColour, 3f), rect);
                }
                graphics2.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics2 = null;
            }
        }
    }
}