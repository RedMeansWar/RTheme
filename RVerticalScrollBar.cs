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
    [DefaultEvent("Scroll")]
    public class RVerticalScrollBar : Control
    {
        public delegate void ScrollEventHandler(object sender);

        private static List<WeakReference> __ENCList = new List<WeakReference>();

        private int ThumbMovement;

        private Rectangle TSA;

        private Rectangle BSA;

        private Rectangle Shaft;

        private Rectangle Thumb;

        private bool ShowThumb;

        private bool ThumbPressed;

        private int _ThumbSize;

        public int _Minimum;

        public int _Maximum;

        public int _Value;

        public int _SmallChange;

        private int _ButtonSize;

        public int _LargeChange;

        private Color _ThumbBorder;

        private Color _LineColour;

        private Color _ArrowColour;

        private Color _BaseColour;

        private Color _ThumbColour;

        private Color _ThumbSecondBorder;

        private Color _FirstBorder;

        private Color _SecondBorder;

        [Category("Colours")]
        public Color ThumbBorder
        {
            get
            {
                return _ThumbBorder;
            }
            set
            {
                _ThumbBorder = value;
            }
        }

        [Category("Colours")]
        public Color LineColour
        {
            get
            {
                return _LineColour;
            }
            set
            {
                _LineColour = value;
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
        public Color ThumbColour
        {
            get
            {
                return _ThumbColour;
            }
            set
            {
                _ThumbColour = value;
            }
        }

        [Category("Colours")]
        public Color ThumbSecondBorder
        {
            get
            {
                return _ThumbSecondBorder;
            }
            set
            {
                _ThumbSecondBorder = value;
            }
        }

        [Category("Colours")]
        public Color FirstBorder
        {
            get
            {
                return _FirstBorder;
            }
            set
            {
                _FirstBorder = value;
            }
        }

        [Category("Colours")]
        public Color SecondBorder
        {
            get
            {
                return _SecondBorder;
            }
            set
            {
                _SecondBorder = value;
            }
        }

        public int Minimum
        {
            get
            {
                return _Minimum;
            }
            set
            {
                _Minimum = value;
                if (value > _Value)
                {
                    _Value = value;
                }
                if (value > _Maximum)
                {
                    _Maximum = value;
                }
                InvalidateLayout();
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
                if (value < _Value)
                {
                    _Value = value;
                }
                if (value < _Minimum)
                {
                    _Minimum = value;
                }
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
                    if (value < _Minimum)
                    {
                        _Value = _Minimum;
                    }
                    else if (value > _Maximum)
                    {
                        _Value = _Maximum;
                    }
                    else
                    {
                        _Value = value;
                    }
                    InvalidatePosition();
                    Scroll?.Invoke(this);
                }
            }
        }

        public int SmallChange
        {
            get
            {
                return _SmallChange;
            }
            set
            {
                if (value >= 1 && value <= 0 - ((_SmallChange == value) ? 1 : 0))
                {
                }
            }
        }

        public int LargeChange
        {
            get
            {
                return _LargeChange;
            }
            set
            {
                if (value >= 1)
                {
                    _LargeChange = value;
                }
            }
        }

        public int ButtonSize
        {
            get
            {
                return _ButtonSize;
            }
            set
            {
                if (value < 16)
                {
                    _ButtonSize = 16;
                }
                else
                {
                    _ButtonSize = value;
                }
            }
        }

        [method: DebuggerNonUserCode]
        public event ScrollEventHandler Scroll;

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

        protected override void OnSizeChanged(EventArgs e)
        {
            InvalidateLayout();
        }

        private void InvalidateLayout()
        {
            ref Rectangle tSA = ref TSA;
            tSA = new Rectangle(0, 1, Width, 0);
            ref Rectangle shaft = ref Shaft;
            checked
            {
                shaft = new Rectangle(0, TSA.Bottom - 1, Width, Height - 3);
                ShowThumb = _Maximum - _Minimum != 0;
                if (ShowThumb)
                {
                    ref Rectangle thumb = ref Thumb;
                    thumb = new Rectangle(1, 0, Width - 3, _ThumbSize);
                }
                Scroll?.Invoke(this);
                InvalidatePosition();
            }
        }

        private void InvalidatePosition()
        {
            Thumb.Y = checked((int)Math.Round((double)(_Value - _Minimum) / (double)(_Maximum - _Minimum) * (double)(Shaft.Height - _ThumbSize) + 1.0));
            Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left || !ShowThumb || 1 == 0)
            {
                return;
            }
            checked
            {
                if (TSA.Contains(e.Location))
                {
                    ThumbMovement = _Value - _SmallChange;
                }
                else if (BSA.Contains(e.Location))
                {
                    ThumbMovement = _Value + _SmallChange;
                }
                else
                {
                    if (Thumb.Contains(e.Location))
                    {
                        ThumbPressed = true;
                        return;
                    }
                    if (e.Y < Thumb.Y)
                    {
                        ThumbMovement = _Value - _LargeChange;
                    }
                    else
                    {
                        ThumbMovement = _Value + _LargeChange;
                    }
                }
                Value = Math.Min(Math.Max(ThumbMovement, _Minimum), _Maximum);
                InvalidatePosition();
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            checked
            {
                if ((ThumbPressed && ShowThumb) ? true : false)
                {
                    int num = e.Y - TSA.Height - unchecked(_ThumbSize / 2);
                    int num2 = Shaft.Height - _ThumbSize;
                    ThumbMovement = (int)Math.Round((double)num / (double)num2 * (double)(_Maximum - _Minimum)) + _Minimum;
                    Value = Math.Min(Math.Max(ThumbMovement, _Minimum), _Maximum);
                    InvalidatePosition();
                }
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            ThumbPressed = false;
        }

        public RVerticalScrollBar()
        {
            __ENCAddToList(this);
            _ThumbSize = 24;
            _Minimum = 0;
            _Maximum = 100;
            _Value = 0;
            _SmallChange = 1;
            _ButtonSize = 16;
            _LargeChange = 10;
            _ThumbBorder = Color.FromArgb(35, 35, 35);
            _LineColour = Color.FromArgb(23, 119, 151);
            _ArrowColour = Color.FromArgb(37, 37, 37);
            _BaseColour = Color.FromArgb(47, 47, 47);
            _ThumbColour = Color.FromArgb(55, 55, 55);
            _ThumbSecondBorder = Color.FromArgb(65, 65, 65);
            _FirstBorder = Color.FromArgb(55, 55, 55);
            _SecondBorder = Color.FromArgb(35, 35, 35);
            SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.Selectable | ControlStyles.SupportsTransparentBackColor | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, value: true);
            DoubleBuffered = true;
            Size size = new Size(24, 50);
            Size = size;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            Graphics graphics2 = graphics;
            graphics2.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            graphics2.SmoothingMode = SmoothingMode.HighQuality;
            graphics2.PixelOffsetMode = PixelOffsetMode.HighQuality;
            graphics2.Clear(_BaseColour);
            Point[] array = new Point[10];
            ref Point reference = ref array[0];
            checked
            {
                Point point = new Point((int)Math.Round((double)Width / 2.0), 5);
                reference = point;
                ref Point reference2 = ref array[1];
                Point point2 = new Point((int)Math.Round((double)Width / 4.0), 13);
                reference2 = point2;
                ref Point reference3 = ref array[2];
                Point point3 = new Point((int)Math.Round((double)Width / 2.0 - 2.0), 13);
                reference3 = point3;
                ref Point reference4 = ref array[3];
                Point point4 = new Point((int)Math.Round((double)Width / 2.0 - 2.0), Height - 13);
                reference4 = point4;
                ref Point reference5 = ref array[4];
                Point point5 = new Point((int)Math.Round((double)Width / 4.0), Height - 13);
                reference5 = point5;
                ref Point reference6 = ref array[5];
                Point point6 = new Point((int)Math.Round((double)Width / 2.0), Height - 5);
                reference6 = point6;
                ref Point reference7 = ref array[6];
                Point point7 = new Point((int)Math.Round((double)Width - (double)Width / 4.0 - 1.0), Height - 13);
                reference7 = point7;
                ref Point reference8 = ref array[7];
                Point point8 = new Point((int)Math.Round((double)Width / 2.0 + 2.0), Height - 13);
                reference8 = point8;
                ref Point reference9 = ref array[8];
                Point point9 = new Point((int)Math.Round((double)Width / 2.0 + 2.0), 13);
                reference9 = point9;
                ref Point reference10 = ref array[9];
                Point point10 = new Point((int)Math.Round((double)Width - (double)Width / 4.0 - 1.0), 13);
                reference10 = point10;
                Point[] points = array;
                graphics2.FillPolygon(new SolidBrush(_ArrowColour), points);
                graphics2.FillRectangle(new SolidBrush(_ThumbColour), Thumb);
                graphics2.DrawRectangle(new Pen(_ThumbBorder), Thumb);
                graphics2.DrawRectangle(new Pen(_ThumbSecondBorder), Thumb.X + 1, Thumb.Y + 1, Thumb.Width - 2, Thumb.Height - 2);
                Graphics graphics3 = graphics2;
                Pen pen = new Pen(_LineColour, 2f);
                point10 = new Point((int)Math.Round((double)Thumb.Width / 2.0 + 1.0), Thumb.Y + 4);
                Point pt = point10;
                point9 = new Point((int)Math.Round((double)Thumb.Width / 2.0 + 1.0), Thumb.Bottom - 4);
                graphics3.DrawLine(pen, pt, point9);
                graphics2.DrawRectangle(new Pen(_FirstBorder), 0, 0, Width - 1, Height - 1);
                graphics2.DrawRectangle(new Pen(_SecondBorder), 1, 1, Width - 3, Height - 3);
                graphics2.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics2 = null;
            }
        }
    }
}