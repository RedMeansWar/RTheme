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
    public class RTabControl : TabControl
    {
        private static List<WeakReference> __ENCList = new List<WeakReference>();

        private Color _TextColour;

        private Color _BackTabColour;

        private Color _BaseColour;

        private Color _ActiveColour;

        private Color _BorderColour;

        private Color _UpLineColour;

        private Color _HorizLineColour;

        private StringFormat CenterSF;

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
        public Color UpLineColour
        {
            get
            {
                return _UpLineColour;
            }
            set
            {
                _UpLineColour = value;
            }
        }

        [Category("Colours")]
        public Color HorizontalLineColour
        {
            get
            {
                return _HorizLineColour;
            }
            set
            {
                _HorizLineColour = value;
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
        public Color BackTabColour
        {
            get
            {
                return _BackTabColour;
            }
            set
            {
                _BackTabColour = value;
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
        public Color ActiveColour
        {
            get
            {
                return _ActiveColour;
            }
            set
            {
                _ActiveColour = value;
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
            Alignment = TabAlignment.Top;
        }

        public RTabControl()
        {
            __ENCAddToList(this);
            _TextColour = Color.FromArgb(255, 255, 255);
            _BackTabColour = Color.FromArgb(54, 54, 54);
            _BaseColour = Color.FromArgb(35, 35, 35);
            _ActiveColour = Color.FromArgb(47, 47, 47);
            _BorderColour = Color.FromArgb(30, 30, 30);
            _UpLineColour = Color.FromArgb(0, 160, 199);
            _HorizLineColour = Color.FromArgb(23, 119, 151);
            CenterSF = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };
            SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, value: true);
            DoubleBuffered = true;
            Font = new Font("Segoe UI", 10f);
            SizeMode = TabSizeMode.Normal;
            Size size = new Size(240, 32);
            ItemSize = size;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            Graphics graphics2 = graphics;
            graphics2.SmoothingMode = SmoothingMode.HighQuality;
            graphics2.PixelOffsetMode = PixelOffsetMode.HighQuality;
            graphics2.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            graphics2.Clear(_BaseColour);
            try
            {
                SelectedTab.BackColor = _BackTabColour;
            }
            catch (Exception projectError)
            {
                ProjectData.SetProjectError(projectError);
                ProjectData.ClearProjectError();
            }
            try
            {
                SelectedTab.BorderStyle = BorderStyle.FixedSingle;
            }
            catch (Exception projectError2)
            {
                ProjectData.SetProjectError(projectError2);
                ProjectData.ClearProjectError();
            }
            Graphics graphics3 = graphics2;
            Pen pen = new Pen(_BorderColour, 2f);
            Rectangle rect = new Rectangle(0, 0, Width, Height);
            graphics3.DrawRectangle(pen, rect);
            checked
            {
                int num = TabCount - 1;
                int num2 = 0;
                while (true)
                {
                    int num3 = num2;
                    int num4 = num;
                    if (num3 > num4)
                    {
                        break;
                    }
                    Point point = new Point(GetTabRect(num2).Location.X, GetTabRect(num2).Location.Y);
                    Point location = point;
                    Size size = new Size(GetTabRect(num2).Width, GetTabRect(num2).Height);
                    Rectangle rectangle = new Rectangle(location, size);
                    Point location2 = rectangle.Location;
                    size = new Size(rectangle.Width, rectangle.Height);
                    Rectangle rectangle2 = new Rectangle(location2, size);
                    if (num2 == SelectedIndex)
                    {
                        graphics2.FillRectangle(new SolidBrush(_BaseColour), rectangle2);
                        Graphics graphics4 = graphics2;
                        SolidBrush brush = new SolidBrush(_ActiveColour);
                        Rectangle rect2 = new Rectangle(rectangle.X + 1, rectangle.Y - 3, rectangle.Width, rectangle.Height + 5);
                        graphics4.FillRectangle(brush, rect2);
                        Graphics graphics5 = graphics2;
                        string s = TabPages[num2].Text;
                        Font font = Font;
                        SolidBrush brush2 = new SolidBrush(_TextColour);
                        rect2 = new Rectangle(rectangle.X + 7, rectangle.Y, rectangle.Width - 3, rectangle.Height);
                        graphics5.DrawString(s, font, brush2, rect2, CenterSF);
                        Graphics graphics6 = graphics2;
                        Pen pen2 = new Pen(_HorizLineColour, 2f);
                        point = new Point(rectangle.X + 3, (int)Math.Round((double)rectangle.Height / 2.0 + 2.0));
                        Point pt = point;
                        Point pt2 = new Point(rectangle.X + 9, (int)Math.Round((double)rectangle.Height / 2.0 + 2.0));
                        graphics6.DrawLine(pen2, pt, pt2);
                        Graphics graphics7 = graphics2;
                        Pen pen3 = new Pen(_UpLineColour, 2f);
                        point = new Point(rectangle.X + 3, rectangle.Y - 3);
                        Point pt3 = point;
                        pt2 = new Point(rectangle.X + 3, rectangle.Height + 5);
                        graphics7.DrawLine(pen3, pt3, pt2);
                    }
                    else
                    {
                        graphics2.DrawString(TabPages[num2].Text, Font, new SolidBrush(_TextColour), rectangle2, CenterSF);
                    }
                    num2++;
                }
                graphics2.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics2 = null;
            }
        }
    }
}