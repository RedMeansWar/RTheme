using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.ComponentModel;
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using static RTheme.DrawHelper;

namespace RTheme
{
    [DefaultEvent("CheckedChanged")]
    public class RSlideCheckbox : Control
    {
        protected bool isChecked;
        protected int mouseState;
        protected Timer animationTimer;
        protected float animationProgress = 0f;
        protected const float animationIncrement = 0.1f;

        protected Color activeColor = Color.Green;
        protected Color inactiveColor = Color.Red;
        protected Color toggleColor = Color.White;
        protected Color borderColor = Color.FromArgb(45, 45, 45);

        protected string labelText = string.Empty;
        protected Font labelFont = SystemFonts.DefaultFont;
        protected Color labelForeColor = Color.White;
        protected FontStyle labelFontStyle = FontStyle.Regular;
        protected MouseState State;

        protected static List<WeakReference> __ENCList = new();

        public delegate void CheckedChangedEventHandler(object sender);


        [Category("Color")]
        public Color ActiveColor
        {
            get => activeColor;
            set
            {
                activeColor = value;
                Invalidate();
            }
        }

        [Category("Color")]
        public Color InactiveColor
        {
            get => inactiveColor;
            set
            {
                inactiveColor = value;
                Invalidate();
            }
        }

        [Category("Color")]
        public Color ToggleColor
        {
            get => toggleColor;
            set
            {
                toggleColor = value;
                Invalidate();
            }
        }

        [Category("Color")]
        public Color BorderColor
        {
            get => borderColor;
            set
            {
                borderColor = value;
                Invalidate();
            }
        }

        [Category("Behavior")]
        public bool Checked
        {
            get => isChecked;
            set
            {
                isChecked = value;
                animationProgress = 0f;
                animationTimer.Start();
                Invalidate();
            }
        }

        [Category("Behavior")]
        [Browsable(true)]
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

        public RSlideCheckbox()
        {
            DoubleBuffered = true;
            Size = new Size(60, 30);
            State = MouseState.None;
            animationTimer = new();
            animationTimer.Interval = 15; // Adjust for smoother/faster animation
            animationTimer.Tick += (s, e) => Animate();
            this.Click += ToggleSwitch_Click;
            this.Resize += OnResize;
            this.MouseEnter += OnMouseEnter;
            this.MouseDown += OnMouseDown;
            this.MouseUp += OnMouseUp;
            this.MouseLeave += OnMouseLeave;
            labelText = this.Name;
        }

        private void ToggleSwitch_Click(object sender, EventArgs e)
        {
            Checked = !Checked;
            CheckedChanged?.Invoke(this);
        }

        private void OnResize(object sender, EventArgs e)
        {
            Width = Width - 2;
            Height = Height - 2;
            Invalidate();
        }

        private void OnMouseDown(object sender, EventArgs e)
        {
            State = MouseState.Down;
            Invalidate();
        }

        private void OnMouseUp(object sender, EventArgs e)
        {
            State = MouseState.Over;
            Invalidate();
        }

        private void OnMouseEnter(object sender, EventArgs e)
        {
            State = MouseState.Over;
            Invalidate();
        }

        private void OnMouseLeave(object sender, EventArgs e)
        {
            State = MouseState.None;
            Invalidate();
        }

        private void Animate()
        {
            animationProgress += animationIncrement;
            if (animationProgress >= 1f)
            {
                animationProgress = 1f;
                animationTimer.Stop();
            }
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;
            Graphics g2 = g;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle rect = new(1, 1, Width - 2, Height - 2);

            GraphicsPath path = new();

            path.AddArc(new(1, 1, Height - 2, Height - 2), 90, 180);
            path.AddArc(new(Width - Height - 1, 1, Height - 2, Height - 2), -90, 180);
            path.CloseFigure();

            using (Brush brush = new SolidBrush(isChecked ? activeColor : inactiveColor))
            {
                g.FillPath(brush, path);
            }

            // Toggle
            int toggleDiameter = Height - 6;
            int toggleX = isChecked ? (int)((Width - Height) * animationProgress) + 3 : 3 + (int)((Width - Height) * (1 - animationProgress));

            using (Brush brush = new SolidBrush(toggleColor))
            {
                g.FillEllipse(brush, new(toggleX, 3, toggleDiameter, toggleDiameter));
            }

            using (Pen pen = new(borderColor, 2))
            {
                g.DrawPath(pen, path);
            }
        }

    }
}

/*
 * protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;
            Graphics g2 = g;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle rect = new(1, 1, Width - 2, Height - 2);

            GraphicsPath path = new();

            path.AddArc(new(1, 1, Height - 2, Height - 2), 90, 180);
            path.AddArc(new(Width - Height - 1, 1, Height - 2, Height - 2), -90, 180);
            path.CloseFigure();

            using (Brush brush = new SolidBrush(isChecked ? activeColor : inactiveColor))
            {
                g.FillPath(brush, path);
            }

            // Toggle
            int toggleDiameter = Height - 6;
            int toggleX = isChecked ? (int)((Width - Height) * animationProgress) + 3 : 3 + (int)((Width - Height) * (1 - animationProgress));

            using (Brush brush = new SolidBrush(toggleColor))
            {
                g.FillEllipse(brush, new(toggleX, 3, toggleDiameter, toggleDiameter));
            }

            using (Pen pen = new(borderColor, 2))
            {
                g.DrawPath(pen, path);
            }
        }
*/