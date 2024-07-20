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
    public class RThemeContainer : ContainerControl
    {
        private static List<WeakReference> __ENCList = new List<WeakReference>();

        private bool _AllowClose;

        private bool _AllowMinimize;

        private bool _AllowMaximize;

        private int _FontSize;

        private readonly Font _Font;

        private bool _ShowIcon;

        private DrawHelper.MouseState State;

        private int MouseXLoc;

        private int MouseYLoc;

        private bool CaptureMovement;

        private const int MoveHeight = 35;

        private Point MouseP;

        private Color _FontColour;

        private Color _BaseColour;

        private Color _ContainerColour;

        private Color _BorderColour;

        private Color _HoverColour;

        [Category("Control")]
        public int FontSize
        {
            get
            {
                return _FontSize;
            }
            set
            {
                _FontSize = value;
            }
        }

        [Category("Control")]
        public bool AllowMinimize
        {
            get
            {
                return _AllowMinimize;
            }
            set
            {
                _AllowMinimize = value;
            }
        }

        [Category("Control")]
        public bool AllowMaximize
        {
            get
            {
                return _AllowMaximize;
            }
            set
            {
                _AllowMaximize = value;
            }
        }

        [Category("Control")]
        public bool ShowIcon
        {
            get
            {
                return _ShowIcon;
            }
            set
            {
                _ShowIcon = value;
            }
        }

        [Category("Control")]
        public bool AllowClose
        {
            get
            {
                return _AllowClose;
            }
            set
            {
                _AllowClose = value;
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
        public Color ContainerColour
        {
            get
            {
                return _ContainerColour;
            }
            set
            {
                _ContainerColour = value;
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

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            CaptureMovement = false;
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

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            MouseXLoc = e.Location.X;
            MouseYLoc = e.Location.Y;
            Invalidate();
            if (CaptureMovement)
            {
                Parent.Location = Control.MousePosition - (Size)MouseP;
            }
            if ((e.X < checked(Width - 90) && e.Y > 35) ? true : false)
            {
                Cursor = Cursors.Arrow;
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
                if ((MouseXLoc > Width - 39 && MouseXLoc < Width - 16 && MouseYLoc < 22) ? true : false)
                {
                    if (_AllowClose)
                    {
                        Environment.Exit(0);
                    }
                }
                else if ((MouseXLoc > Width - 64 && MouseXLoc < Width - 41 && MouseYLoc < 22) ? true : false)
                {
                    if (_AllowMaximize)
                    {
                        switch (unchecked((int)FindForm().WindowState))
                        {
                            case 2:
                                FindForm().WindowState = FormWindowState.Normal;
                                break;
                            case 0:
                                FindForm().WindowState = FormWindowState.Maximized;
                                break;
                        }
                    }
                }
                else if ((MouseXLoc > Width - 89 && MouseXLoc < Width - 66 && MouseYLoc < 22) ? true : false)
                {
                    if (_AllowMinimize)
                    {
                        switch (unchecked((int)FindForm().WindowState))
                        {
                            case 0:
                                FindForm().WindowState = FormWindowState.Minimized;
                                break;
                            case 2:
                                FindForm().WindowState = FormWindowState.Minimized;
                                break;
                        }
                    }
                }
                else
                {
                    bool num = e.Button == MouseButtons.Left;
                    Rectangle rectangle = new Rectangle(0, 0, Width - 90, 35);
                    Rectangle rectangle2 = rectangle;
                    if (num & rectangle2.Contains(e.Location))
                    {
                        CaptureMovement = true;
                        MouseP = e.Location;
                    }
                    else
                    {
                        bool num2 = e.Button == MouseButtons.Left;
                        rectangle2 = new Rectangle(Width - 90, 22, 75, 13);
                        rectangle = rectangle2;
                        if (num2 & rectangle.Contains(e.Location))
                        {
                            CaptureMovement = true;
                            MouseP = e.Location;
                        }
                        else
                        {
                            bool num3 = e.Button == MouseButtons.Left;
                            rectangle2 = new Rectangle(Width - 15, 0, 15, 35);
                            rectangle = rectangle2;
                            if (num3 & rectangle.Contains(e.Location))
                            {
                                CaptureMovement = true;
                                MouseP = e.Location;
                            }
                            else
                            {
                                Focus();
                            }
                        }
                    }
                }
                State = DrawHelper.MouseState.Down;
                Invalidate();
            }
        }

        public RThemeContainer()
        {
            __ENCAddToList(this);
            _AllowClose = true;
            _AllowMinimize = true;
            _AllowMaximize = true;
            _FontSize = 12;
            _Font = new Font("Segoe UI", _FontSize);
            _ShowIcon = true;
            State = DrawHelper.MouseState.None;
            CaptureMovement = false;
            ref Point mouseP = ref MouseP;
            mouseP = new Point(0, 0);
            _FontColour = Color.FromArgb(255, 255, 255);
            _BaseColour = Color.FromArgb(35, 35, 35);
            _ContainerColour = Color.FromArgb(54, 54, 54);
            _BorderColour = Color.FromArgb(60, 60, 60);
            _HoverColour = Color.FromArgb(42, 42, 42);
            SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, value: true);
            DoubleBuffered = true;
            BackColor = _BaseColour;
            Dock = DockStyle.Fill;
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            ParentForm.FormBorderStyle = FormBorderStyle.None;
            ParentForm.AllowTransparency = false;
            ParentForm.TransparencyKey = Color.Fuchsia;
            ParentForm.FindForm().StartPosition = FormStartPosition.CenterScreen;
            Dock = DockStyle.Fill;
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            Graphics graphics2 = graphics;
            graphics2.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            graphics2.SmoothingMode = SmoothingMode.HighQuality;
            graphics2.PixelOffsetMode = PixelOffsetMode.HighQuality;
            Graphics graphics3 = graphics2;
            SolidBrush brush = new SolidBrush(_BaseColour);
            Rectangle rect = new Rectangle(0, 0, Width, Height);
            graphics3.FillRectangle(brush, rect);
            Graphics graphics4 = graphics2;
            SolidBrush brush2 = new SolidBrush(_ContainerColour);
            checked
            {
                rect = new Rectangle(2, 35, Width - 4, Height - 37);
                graphics4.FillRectangle(brush2, rect);
                Graphics graphics5 = graphics2;
                Pen pen = new Pen(_BorderColour);
                rect = new Rectangle(0, 0, Width, Height);
                graphics5.DrawRectangle(pen, rect);
                Point[] array = new Point[4];
                ref Point reference = ref array[0];
                Point point = new Point(Width - 90, 0);
                reference = point;
                ref Point reference2 = ref array[1];
                Point point2 = new Point(Width - 90, 22);
                reference2 = point2;
                ref Point reference3 = ref array[2];
                Point point3 = new Point(Width - 15, 22);
                reference3 = point3;
                ref Point reference4 = ref array[3];
                Point point4 = new Point(Width - 15, 0);
                reference4 = point4;
                Point[] points = array;
                graphics2.DrawLines(new Pen(_BorderColour), points);
                graphics2.DrawLine(new Pen(_BorderColour), Width - 65, 0, Width - 65, 22);
                DrawHelper.MouseState mouseState = State;
                if (mouseState == DrawHelper.MouseState.Over)
                {
                    if ((MouseXLoc > Width - 39 && MouseXLoc < Width - 16 && MouseYLoc < 22) ? true : false)
                    {
                        Graphics graphics6 = graphics2;
                        SolidBrush brush3 = new SolidBrush(_HoverColour);
                        rect = new Rectangle(Width - 39, 0, 23, 22);
                        graphics6.FillRectangle(brush3, rect);
                    }
                    else if ((MouseXLoc > Width - 64 && MouseXLoc < Width - 41 && MouseYLoc < 22) ? true : false)
                    {
                        Graphics graphics7 = graphics2;
                        SolidBrush brush4 = new SolidBrush(_HoverColour);
                        rect = new Rectangle(Width - 64, 0, 23, 22);
                        graphics7.FillRectangle(brush4, rect);
                    }
                    else if ((MouseXLoc > Width - 89 && MouseXLoc < Width - 66 && MouseYLoc < 22) ? true : false)
                    {
                        Graphics graphics8 = graphics2;
                        SolidBrush brush5 = new SolidBrush(_HoverColour);
                        rect = new Rectangle(Width - 89, 0, 23, 22);
                        graphics8.FillRectangle(brush5, rect);
                    }
                }
                graphics2.DrawLine(new Pen(_BorderColour), Width - 40, 0, Width - 40, 22);
                graphics2.DrawLine(new Pen(_FontColour), Width - 33, 6, Width - 22, 16);
                graphics2.DrawLine(new Pen(_FontColour), Width - 33, 16, Width - 22, 6);
                graphics2.DrawLine(new Pen(_FontColour), Width - 83, 16, Width - 72, 16);
                graphics2.DrawLine(new Pen(_FontColour), Width - 58, 16, Width - 47, 16);
                graphics2.DrawLine(new Pen(_FontColour), Width - 58, 16, Width - 58, 6);
                graphics2.DrawLine(new Pen(_FontColour), Width - 47, 16, Width - 47, 6);
                graphics2.DrawLine(new Pen(_FontColour), Width - 58, 6, Width - 47, 6);
                graphics2.DrawLine(new Pen(_FontColour), Width - 58, 7, Width - 47, 7);
                if (_ShowIcon)
                {
                    Graphics graphics9 = graphics2;
                    Icon icon = FindForm().Icon;
                    rect = new Rectangle(6, 6, 22, 22);
                    graphics9.DrawIcon(icon, rect);
                    Graphics graphics10 = graphics2;
                    string s = Text;
                    Font font = _Font;
                    SolidBrush brush6 = new SolidBrush(_FontColour);
                    RectangleF layoutRectangle = new RectangleF(31f, 0f, Width - 110, 35f);
                    graphics10.DrawString(s, font, brush6, layoutRectangle, new StringFormat
                    {
                        LineAlignment = StringAlignment.Center,
                        Alignment = StringAlignment.Near
                    });
                }
                else
                {
                    Graphics graphics11 = graphics2;
                    string s2 = Text;
                    Font font2 = _Font;
                    SolidBrush brush7 = new SolidBrush(_FontColour);
                    RectangleF layoutRectangle = new RectangleF(3f, 0f, Width - 110, 35f);
                    graphics11.DrawString(s2, font2, brush7, layoutRectangle, new StringFormat
                    {
                        LineAlignment = StringAlignment.Center,
                        Alignment = StringAlignment.Near
                    });
                }
                graphics2.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics2 = null;
            }
        }
    }
}