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
    public class RComboBox : ComboBox
    {
        private static List<WeakReference> __ENCList = new List<WeakReference>();

        private int _StartIndex;

        private Color _BorderColour;

        private Color _BaseColour;

        private Color _FontColour;

        private Color _LineColour;

        private Color _SqaureColour;

        private Color _ArrowColour;

        private Color _SqaureHoverColour;

        private DrawHelper.MouseState State;

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
        public Color SqaureColour
        {
            get
            {
                return _SqaureColour;
            }
            set
            {
                _SqaureColour = value;
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
        public Color SqaureHoverColour
        {
            get
            {
                return _SqaureHoverColour;
            }
            set
            {
                _SqaureHoverColour = value;
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

        public int StartIndex
        {
            get
            {
                return _StartIndex;
            }
            set
            {
                _StartIndex = value;
                try
                {
                    base.SelectedIndex = value;
                }
                catch (Exception projectError)
                {
                    ProjectData.SetProjectError(projectError);
                    ProjectData.ClearProjectError();
                }
                Invalidate();
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

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            Invalidate();
            OnMouseClick(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            Invalidate();
            base.OnMouseUp(e);
        }

        public void ReplaceItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            e.Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            checked
            {
                Rectangle rect = new Rectangle(e.Bounds.X, e.Bounds.Y, e.Bounds.Width + 1, e.Bounds.Height + 1);
                try
                {
                    Graphics graphics = e.Graphics;
                    if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                    {
                        graphics.FillRectangle(new SolidBrush(_SqaureColour), rect);
                        graphics.DrawString(GetItemText(RuntimeHelpers.GetObjectValue(base.Items[e.Index])), Font, new SolidBrush(_FontColour), 1f, e.Bounds.Top + 2);
                    }
                    else
                    {
                        graphics.FillRectangle(new SolidBrush(_BaseColour), rect);
                        graphics.DrawString(GetItemText(RuntimeHelpers.GetObjectValue(base.Items[e.Index])), Font, new SolidBrush(_FontColour), 1f, e.Bounds.Top + 2);
                    }
                    graphics = null;
                }
                catch (Exception projectError)
                {
                    ProjectData.SetProjectError(projectError);
                    ProjectData.ClearProjectError();
                }
                e.DrawFocusRectangle();
                Invalidate();
            }
        }

        public RComboBox()
        {
            base.DrawItem += ReplaceItem;
            __ENCAddToList(this);
            _StartIndex = 0;
            _BorderColour = Color.FromArgb(35, 35, 35);
            _BaseColour = Color.FromArgb(42, 42, 42);
            _FontColour = Color.FromArgb(255, 255, 255);
            _LineColour = Color.FromArgb(23, 119, 151);
            _SqaureColour = Color.FromArgb(47, 47, 47);
            _ArrowColour = Color.FromArgb(30, 30, 30);
            _SqaureHoverColour = Color.FromArgb(52, 52, 52);
            State = DrawHelper.MouseState.None;
            SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, value: true);
            DoubleBuffered = true;
            BackColor = Color.Transparent;
            DrawMode = DrawMode.OwnerDrawFixed;
            DropDownStyle = ComboBoxStyle.DropDownList;
            Width = 163;
            Font = new Font("Segoe UI", 10f);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            Graphics graphics2 = graphics;
            graphics2.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            graphics2.SmoothingMode = SmoothingMode.HighQuality;
            graphics2.PixelOffsetMode = PixelOffsetMode.HighQuality;
            graphics2.Clear(BackColor);
            checked
            {
                try
                {
                    Rectangle rect = new Rectangle(Width - 25, 0, Width, Height);
                    Graphics graphics3 = graphics2;
                    SolidBrush brush = new SolidBrush(_BaseColour);
                    Rectangle rect2 = new Rectangle(0, 0, Width - 25, Height);
                    graphics3.FillRectangle(brush, rect2);
                    switch (unchecked((byte)State))
                    {
                        case 0:
                            graphics2.FillRectangle(new SolidBrush(_SqaureColour), rect);
                            break;
                        case 1:
                            graphics2.FillRectangle(new SolidBrush(_SqaureHoverColour), rect);
                            break;
                    }
                    Graphics graphics4 = graphics2;
                    Pen pen = new Pen(_LineColour, 2f);
                    Point point = new Point(Width - 26, 1);
                    Point pt = point;
                    Point pt2 = new Point(Width - 26, Height - 1);
                    graphics4.DrawLine(pen, pt, pt2);
                    try
                    {
                        Graphics graphics5 = graphics2;
                        string s = Text;
                        Font font = Font;
                        SolidBrush brush2 = new SolidBrush(_FontColour);
                        rect2 = new Rectangle(3, 0, Width - 20, Height);
                        graphics5.DrawString(s, font, brush2, rect2, new StringFormat
                        {
                            LineAlignment = StringAlignment.Center,
                            Alignment = StringAlignment.Near
                        });
                    }
                    catch (Exception projectError)
                    {
                        ProjectData.SetProjectError(projectError);
                        ProjectData.ClearProjectError();
                    }
                    Graphics graphics6 = graphics2;
                    Pen pen2 = new Pen(_BorderColour, 2f);
                    rect2 = new Rectangle(0, 0, Width, Height);
                    graphics6.DrawRectangle(pen2, rect2);
                    Point[] array = new Point[3];
                    ref Point reference = ref array[0];
                    pt2 = new Point(Width - 17, 11);
                    reference = pt2;
                    ref Point reference2 = ref array[1];
                    point = new Point(Width - 13, 5);
                    reference2 = point;
                    ref Point reference3 = ref array[2];
                    Point point2 = new Point(Width - 9, 11);
                    reference3 = point2;
                    Point[] points = array;
                    graphics2.FillPolygon(new SolidBrush(_BorderColour), points);
                    graphics2.DrawPolygon(new Pen(_ArrowColour), points);
                    array = new Point[3];
                    ref Point reference4 = ref array[0];
                    point2 = new Point(Width - 17, 15);
                    reference4 = point2;
                    ref Point reference5 = ref array[1];
                    pt2 = new Point(Width - 13, 21);
                    reference5 = pt2;
                    ref Point reference6 = ref array[2];
                    point = new Point(Width - 9, 15);
                    reference6 = point;
                    Point[] points2 = array;
                    graphics2.FillPolygon(new SolidBrush(_BorderColour), points2);
                    graphics2.DrawPolygon(new Pen(_ArrowColour), points2);
                }
                catch (Exception projectError2)
                {
                    ProjectData.SetProjectError(projectError2);
                    ProjectData.ClearProjectError();
                }
                graphics2.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics2 = null;
            }
        }
    }
}