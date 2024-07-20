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
    public class RStatusBar : Control
    {
        public enum LinesCount
        {
            One = 1,
            Two
        }

        public enum Alignments
        {
            Left,
            Center,
            Right
        }

        private static List<WeakReference> __ENCList = new List<WeakReference>();

        private Color _BaseColour;

        private Color _BorderColour;

        private Color _TextColour;

        private Color _RectColour;

        private bool _ShowLine;

        private LinesCount _LinesToShow;

        private Alignments _Alignment;

        private bool _ShowBorder;

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

        [Category("Control")]
        public Alignments Alignment
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

        [Category("Control")]
        public LinesCount LinesToShow
        {
            get
            {
                return _LinesToShow;
            }
            set
            {
                _LinesToShow = value;
            }
        }

        public bool ShowBorder
        {
            get
            {
                return _ShowBorder;
            }
            set
            {
                _ShowBorder = value;
            }
        }

        [Category("Colours")]
        public Color RectangleColor
        {
            get
            {
                return _RectColour;
            }
            set
            {
                _RectColour = value;
            }
        }

        public bool ShowLine
        {
            get
            {
                return _ShowLine;
            }
            set
            {
                _ShowLine = value;
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

        protected override void CreateHandle()
        {
            base.CreateHandle();
            Dock = DockStyle.Bottom;
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            Invalidate();
        }

        public RStatusBar()
        {
            __ENCAddToList(this);
            _BaseColour = Color.FromArgb(42, 42, 42);
            _BorderColour = Color.FromArgb(35, 35, 35);
            _TextColour = Color.White;
            _RectColour = Color.FromArgb(21, 117, 149);
            _ShowLine = true;
            _LinesToShow = LinesCount.One;
            _Alignment = Alignments.Left;
            _ShowBorder = true;
            SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, value: true);
            DoubleBuffered = true;
            Font = new Font("Segoe UI", 9f);
            ForeColor = Color.White;
            Size size = new Size(Width, 20);
            Size = size;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            Rectangle rect = new Rectangle(0, 0, Width, Height);
            Graphics graphics2 = graphics;
            graphics2.SmoothingMode = SmoothingMode.HighQuality;
            graphics2.PixelOffsetMode = PixelOffsetMode.HighQuality;
            graphics2.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            graphics2.Clear(BaseColour);
            graphics2.FillRectangle(new SolidBrush(BaseColour), rect);
            checked
            {
                if (_ShowLine)
                {
                    switch (unchecked((int)_LinesToShow))
                    {
                        case 1:
                            {
                                Rectangle rectangle;
                                if (_Alignment == Alignments.Left)
                                {
                                    Graphics graphics8 = graphics2;
                                    string s4 = Text;
                                    Font font4 = Font;
                                    SolidBrush brush6 = new SolidBrush(_TextColour);
                                    rectangle = new Rectangle(22, 2, Width, Height);
                                    graphics8.DrawString(s4, font4, brush6, rectangle, new StringFormat
                                    {
                                        Alignment = StringAlignment.Near,
                                        LineAlignment = StringAlignment.Near
                                    });
                                }
                                else if (_Alignment == Alignments.Center)
                                {
                                    Graphics graphics9 = graphics2;
                                    string s5 = Text;
                                    Font font5 = Font;
                                    SolidBrush brush7 = new SolidBrush(_TextColour);
                                    rectangle = new Rectangle(0, 0, Width, Height);
                                    graphics9.DrawString(s5, font5, brush7, rectangle, new StringFormat
                                    {
                                        Alignment = StringAlignment.Center,
                                        LineAlignment = StringAlignment.Center
                                    });
                                }
                                else
                                {
                                    Graphics graphics10 = graphics2;
                                    string s6 = Text;
                                    Font font6 = Font;
                                    SolidBrush brush8 = new SolidBrush(_TextColour);
                                    rectangle = new Rectangle(0, 0, Width - 5, Height);
                                    graphics10.DrawString(s6, font6, brush8, rectangle, new StringFormat
                                    {
                                        Alignment = StringAlignment.Far,
                                        LineAlignment = StringAlignment.Center
                                    });
                                }
                                Graphics graphics11 = graphics2;
                                SolidBrush brush9 = new SolidBrush(_RectColour);
                                rectangle = new Rectangle(5, 9, 14, 3);
                                graphics11.FillRectangle(brush9, rectangle);
                                break;
                            }
                        case 2:
                            {
                                Rectangle rectangle;
                                if (_Alignment == Alignments.Left)
                                {
                                    Graphics graphics3 = graphics2;
                                    string s = Text;
                                    Font font = Font;
                                    SolidBrush brush = new SolidBrush(_TextColour);
                                    rectangle = new Rectangle(22, 2, Width, Height);
                                    graphics3.DrawString(s, font, brush, rectangle, new StringFormat
                                    {
                                        Alignment = StringAlignment.Near,
                                        LineAlignment = StringAlignment.Near
                                    });
                                }
                                else if (_Alignment == Alignments.Center)
                                {
                                    Graphics graphics4 = graphics2;
                                    string s2 = Text;
                                    Font font2 = Font;
                                    SolidBrush brush2 = new SolidBrush(_TextColour);
                                    rectangle = new Rectangle(0, 0, Width, Height);
                                    graphics4.DrawString(s2, font2, brush2, rectangle, new StringFormat
                                    {
                                        Alignment = StringAlignment.Center,
                                        LineAlignment = StringAlignment.Center
                                    });
                                }
                                else
                                {
                                    Graphics graphics5 = graphics2;
                                    string s3 = Text;
                                    Font font3 = Font;
                                    SolidBrush brush3 = new SolidBrush(_TextColour);
                                    rectangle = new Rectangle(0, 0, Width - 22, Height);
                                    graphics5.DrawString(s3, font3, brush3, rectangle, new StringFormat
                                    {
                                        Alignment = StringAlignment.Far,
                                        LineAlignment = StringAlignment.Center
                                    });
                                }
                                Graphics graphics6 = graphics2;
                                SolidBrush brush4 = new SolidBrush(_RectColour);
                                rectangle = new Rectangle(5, 9, 14, 3);
                                graphics6.FillRectangle(brush4, rectangle);
                                Graphics graphics7 = graphics2;
                                SolidBrush brush5 = new SolidBrush(_RectColour);
                                rectangle = new Rectangle(Width - 20, 9, 14, 3);
                                graphics7.FillRectangle(brush5, rectangle);
                                break;
                            }
                    }
                }
                else
                {
                    Graphics graphics12 = graphics2;
                    string s7 = Text;
                    Font font7 = Font;
                    Brush white = Brushes.White;
                    Rectangle rectangle = new Rectangle(5, 2, Width, Height);
                    graphics12.DrawString(s7, font7, white, rectangle, new StringFormat
                    {
                        Alignment = StringAlignment.Near,
                        LineAlignment = StringAlignment.Near
                    });
                }
                if (_ShowBorder)
                {
                    Graphics graphics13 = graphics2;
                    Pen pen = new Pen(_BorderColour, 2f);
                    Point pt = new Point(0, 0);
                    Point pt2 = new Point(Width, 0);
                    graphics13.DrawLine(pen, pt, pt2);
                }
                graphics2.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics2 = null;
            }
        }
    }
}