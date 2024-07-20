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
    public class RRadioButton : Control
    {
        public delegate void CheckedChangedEventHandler(object sender);

        private static List<WeakReference> __ENCList = new List<WeakReference>();

        private bool _Checked;

        private DrawHelper.MouseState State;

        private Color _HoverColour;

        private Color _CheckedColour;

        private Color _BorderColour;

        private Color _BackColour;

        private Color _TextColour;

        [Category("Colours")]
        public Color HighlightColour
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
                InvalidateControls();
                CheckedChanged?.Invoke(this);
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

        protected override void OnClick(EventArgs e)
        {
            if (!_Checked)
            {
                Checked = true;
            }
            base.OnClick(e);
        }

        private void InvalidateControls()
        {
            if (!IsHandleCreated || !_Checked)
            {
                return;
            }
            foreach (Control control in Parent.Controls)
            {
                if ((control != this && control is RRadioButton) ? true : false)
                {
                    ((RRadioButton)control).Checked = false;
                    Invalidate();
                }
            }
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            InvalidateControls();
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

        public RRadioButton()
        {
            __ENCAddToList(this);
            State = DrawHelper.MouseState.None;
            _HoverColour = Color.FromArgb(50, 49, 51);
            _CheckedColour = Color.FromArgb(173, 173, 174);
            _BorderColour = Color.FromArgb(35, 35, 35);
            _BackColour = Color.FromArgb(54, 54, 54);
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
            checked
            {
                Rectangle rect = new Rectangle(1, 1, Height - 2, Height - 2);
                Rectangle rect2 = new Rectangle(6, 6, Height - 12, Height - 12);
                Graphics graphics2 = graphics;
                graphics2.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                graphics2.SmoothingMode = SmoothingMode.HighQuality;
                graphics2.PixelOffsetMode = PixelOffsetMode.HighQuality;
                graphics2.Clear(_BackColour);
                graphics2.FillEllipse(new SolidBrush(_BackColour), rect);
                graphics2.DrawEllipse(new Pen(_BorderColour, 2f), rect);
                Rectangle rect3;
                if (Checked)
                {
                    DrawHelper.MouseState mouseState = State;
                    if (mouseState == DrawHelper.MouseState.Over)
                    {
                        Graphics graphics3 = graphics2;
                        SolidBrush brush = new SolidBrush(_HoverColour);
                        rect3 = new Rectangle(2, 2, Height - 4, Height - 4);
                        graphics3.FillEllipse(brush, rect3);
                    }
                    graphics2.FillEllipse(new SolidBrush(_CheckedColour), rect2);
                }
                else
                {
                    DrawHelper.MouseState mouseState2 = State;
                    if (mouseState2 == DrawHelper.MouseState.Over)
                    {
                        Graphics graphics4 = graphics2;
                        SolidBrush brush2 = new SolidBrush(_HoverColour);
                        rect3 = new Rectangle(2, 2, Height - 4, Height - 4);
                        graphics4.FillEllipse(brush2, rect3);
                    }
                }
                Graphics graphics5 = graphics2;
                string s = Text;
                Font font = Font;
                SolidBrush brush3 = new SolidBrush(_TextColour);
                rect3 = new Rectangle(24, 3, Width, Height);
                graphics5.DrawString(s, font, brush3, rect3, new StringFormat
                {
                    Alignment = StringAlignment.Near,
                    LineAlignment = StringAlignment.Near
                });
                graphics2.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics2 = null;
            }
        }
    }
}