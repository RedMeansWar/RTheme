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
    [DefaultEvent("CheckedChanged")]
    public class RCheckBox : Control
    {
        public delegate void CheckedChangedEventHandler(object sender);

        private static List<WeakReference> __ENCList = new List<WeakReference>();

        private bool _Checked;

        private DrawHelper.MouseState State;

        private Color _CheckedColour;

        private Color _BorderColour;

        private Color _BackColour;

        private Color _TextColour;

        [Category("Colours")]
        public Color BaseColour
        {
            get
            {
                return _BackColour;
            }
            set
            {
                _BackColour = value;
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
        public Color CheckedColour
        {
            get
            {
                return _CheckedColour;
            }
            set
            {
                _CheckedColour = value;
            }
        }

        [Category("Colours")]
        public Color FontColour
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

        public bool Checked
        {
            get
            {
                return _Checked;
            }
            set
            {
                _Checked = value;
                Invalidate();
            }
        }

        [method: DebuggerNonUserCode]
        public event CheckedChangedEventHandler CheckedChanged;

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

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            Invalidate();
        }

        protected override void OnClick(EventArgs e)
        {
            _Checked = !_Checked;
            CheckedChanged?.Invoke(this);
            base.OnClick(e);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Height = 22;
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

        public RCheckBox()
        {
            __ENCAddToList(this);
            State = DrawHelper.MouseState.None;
            _CheckedColour = Color.FromArgb(173, 173, 174);
            _BorderColour = Color.FromArgb(35, 35, 35);
            _BackColour = Color.FromArgb(42, 42, 42);
            _TextColour = Color.FromArgb(255, 255, 255);
            SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, value: true);
            DoubleBuffered = true;
            Cursor = Cursors.Hand;
            Size size = new Size(100, 22);
            Size = size;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            Rectangle rect = new Rectangle(0, 0, 20, 20);
            Graphics graphics2 = graphics;
            graphics2.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            graphics2.SmoothingMode = SmoothingMode.HighQuality;
            graphics2.PixelOffsetMode = PixelOffsetMode.HighQuality;
            graphics2.Clear(Color.FromArgb(54, 54, 54));
            graphics2.FillRectangle(new SolidBrush(_BackColour), rect);
            Graphics graphics3 = graphics2;
            Pen pen = new Pen(_BorderColour);
            Rectangle rect2 = new Rectangle(1, 1, 18, 18);
            graphics3.DrawRectangle(pen, rect2);
            DrawHelper.MouseState mouseState = State;
            if (mouseState == DrawHelper.MouseState.Over)
            {
                graphics2.FillRectangle(new SolidBrush(Color.FromArgb(50, 49, 51)), rect);
                Graphics graphics4 = graphics2;
                Pen pen2 = new Pen(_BorderColour);
                rect2 = new Rectangle(1, 1, 18, 18);
                graphics4.DrawRectangle(pen2, rect2);
            }
            if (Checked)
            {
                Point[] array = new Point[6];
                ref Point reference = ref array[0];
                Point point = new Point(4, 11);
                reference = point;
                ref Point reference2 = ref array[1];
                Point point2 = new Point(6, 8);
                reference2 = point2;
                ref Point reference3 = ref array[2];
                Point point3 = new Point(9, 12);
                reference3 = point3;
                ref Point reference4 = ref array[3];
                Point point4 = new Point(15, 3);
                reference4 = point4;
                ref Point reference5 = ref array[4];
                Point point5 = new Point(17, 6);
                reference5 = point5;
                ref Point reference6 = ref array[5];
                Point point6 = new Point(9, 16);
                reference6 = point6;
                Point[] points = array;
                graphics2.FillPolygon(new SolidBrush(_CheckedColour), points);
            }
            Graphics graphics5 = graphics2;
            string s = Text;
            Font font = Font;
            SolidBrush brush = new SolidBrush(_TextColour);
            rect2 = new Rectangle(24, 1, Width, checked(Height - 2));
            graphics5.DrawString(s, font, brush, rect2, new StringFormat
            {
                Alignment = StringAlignment.Near,
                LineAlignment = StringAlignment.Center
            });
            graphics2.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics2 = null;
        }
    }
}