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
    public class RTextBox : Control
    {
        public enum Styles
        {
            Rounded,
            NotRounded
        }

        private static List<WeakReference> __ENCList = new List<WeakReference>();

        private DrawHelper.MouseState State;

        [AccessedThroughProperty("TB")]
        private TextBox _TB;

        private Color _BaseColour;

        private Color _TextColour;

        private Color _BorderColour;

        private Styles _Style;

        private HorizontalAlignment _TextAlign;

        private int _MaxLength;

        private bool _ReadOnly;

        private bool _UseSystemPasswordChar;

        private bool _Multiline;

        protected virtual TextBox TB
        {
            [DebuggerNonUserCode]
            get
            {
                return _TB;
            }
            [MethodImpl(MethodImplOptions.Synchronized)]
            [DebuggerNonUserCode]
            set
            {
                _TB = value;
            }
        }

        [Category("Options")]
        public HorizontalAlignment TextAlign
        {
            get
            {
                return _TextAlign;
            }
            set
            {
                _TextAlign = value;
                if (TB != null)
                {
                    TB.TextAlign = value;
                }
            }
        }

        [Category("Options")]
        public int MaxLength
        {
            get
            {
                return _MaxLength;
            }
            set
            {
                _MaxLength = value;
                if (TB != null)
                {
                    TB.MaxLength = value;
                }
            }
        }

        [Category("Options")]
        public bool ReadOnly
        {
            get
            {
                return _ReadOnly;
            }
            set
            {
                _ReadOnly = value;
                if (TB != null)
                {
                    TB.ReadOnly = value;
                }
            }
        }

        [Category("Options")]
        public bool UseSystemPasswordChar
        {
            get
            {
                return _UseSystemPasswordChar;
            }
            set
            {
                _UseSystemPasswordChar = value;
                if (TB != null)
                {
                    TB.UseSystemPasswordChar = value;
                }
            }
        }

        [Category("Options")]
        public bool Multiline
        {
            get
            {
                return _Multiline;
            }
            set
            {
                _Multiline = value;
                checked
                {
                    if (TB != null)
                    {
                        TB.Multiline = value;
                        if (value)
                        {
                            TB.Height = Height - 11;
                        }
                        else
                        {
                            Height = TB.Height + 11;
                        }
                    }
                }
            }
        }

        [Category("Options")]
        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value;
                if (TB != null)
                {
                    TB.Text = value;
                }
            }
        }

        [Category("Options")]
        public override Font Font
        {
            get
            {
                return base.Font;
            }
            set
            {
                base.Font = value;
                checked
                {
                    if (TB != null)
                    {
                        TB.Font = value;
                        TextBox tB = TB;
                        Point location = new Point(3, 5);
                        tB.Location = location;
                        TB.Width = Width - 6;
                        if (!_Multiline)
                        {
                            Height = TB.Height + 11;
                        }
                    }
                }
            }
        }

        public Styles Style
        {
            get
            {
                return _Style;
            }
            set
            {
                _Style = value;
            }
        }

        [Category("Colours")]
        public Color BackgroundColour
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

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            if (!Controls.Contains(TB))
            {
                Controls.Add(TB);
            }
        }

        private void OnBaseTextChanged(object s, EventArgs e)
        {
            Text = TB.Text;
        }

        private void OnBaseKeyDown(object s, KeyEventArgs e)
        {
            if ((e.Control && e.KeyCode == Keys.A) ? true : false)
            {
                TB.SelectAll();
                e.SuppressKeyPress = true;
            }
            if ((e.Control && e.KeyCode == Keys.C) ? true : false)
            {
                TB.Copy();
                e.SuppressKeyPress = true;
            }
        }

        protected override void OnResize(EventArgs e)
        {
            TextBox tB = TB;
            Point location = new Point(5, 5);
            tB.Location = location;
            checked
            {
                TB.Width = Width - 10;
                if (_Multiline)
                {
                    TB.Height = Height - 11;
                }
                else
                {
                    Height = TB.Height + 11;
                }
                base.OnResize(e);
            }
        }

        public void SelectAll()
        {
            TB.Focus();
            TB.SelectAll();
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
            TB.Focus();
            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            State = DrawHelper.MouseState.None;
            Invalidate();
        }

        public RTextBox()
        {
            __ENCAddToList(this);
            State = DrawHelper.MouseState.None;
            _BaseColour = Color.FromArgb(42, 42, 42);
            _TextColour = Color.FromArgb(255, 255, 255);
            _BorderColour = Color.FromArgb(35, 35, 35);
            _Style = Styles.NotRounded;
            _TextAlign = HorizontalAlignment.Left;
            _MaxLength = 32767;
            SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, value: true);
            DoubleBuffered = true;
            BackColor = Color.Transparent;
            TB = new TextBox();
            TB.Height = 190;
            TB.Font = new Font("Segoe UI", 10f);
            TB.Text = Text;
            TB.BackColor = Color.FromArgb(42, 42, 42);
            TB.ForeColor = Color.FromArgb(255, 255, 255);
            TB.MaxLength = _MaxLength;
            TB.Multiline = false;
            TB.ReadOnly = _ReadOnly;
            TB.UseSystemPasswordChar = _UseSystemPasswordChar;
            TB.BorderStyle = BorderStyle.None;
            TextBox tB = TB;
            Point location = new Point(5, 5);
            tB.Location = location;
            TB.Width = checked(Width - 35);
            TB.TextChanged += OnBaseTextChanged;
            TB.KeyDown += OnBaseKeyDown;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            Rectangle rectangle = new Rectangle(0, 0, Width, Height);
            Graphics graphics2 = graphics;
            graphics2.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            graphics2.SmoothingMode = SmoothingMode.HighQuality;
            graphics2.PixelOffsetMode = PixelOffsetMode.HighQuality;
            graphics2.Clear(BackColor);
            TB.BackColor = Color.FromArgb(42, 42, 42);
            TB.ForeColor = Color.FromArgb(255, 255, 255);
            switch ((int)_Style)
            {
                case 0:
                    {
                        GraphicsPath graphicsPath = DrawHelper.RoundRectangle(rectangle, 6);
                        graphics2.FillPath(new SolidBrush(Color.FromArgb(42, 42, 42)), graphicsPath);
                        graphics2.DrawPath(new Pen(new SolidBrush(Color.FromArgb(35, 35, 35)), 2f), graphicsPath);
                        graphicsPath.Dispose();
                        break;
                    }
                case 1:
                    {
                        Graphics graphics3 = graphics2;
                        SolidBrush brush = new SolidBrush(Color.FromArgb(42, 42, 42));
                        Rectangle rect = checked(new Rectangle(0, 0, Width - 1, Height - 1));
                        graphics3.FillRectangle(brush, rect);
                        Graphics graphics4 = graphics2;
                        Pen pen = new Pen(new SolidBrush(Color.FromArgb(35, 35, 35)), 2f);
                        rect = new Rectangle(0, 0, Width, Height);
                        graphics4.DrawRectangle(pen, rect);
                        break;
                    }
            }
            graphics2.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics2 = null;
        }
    }
}