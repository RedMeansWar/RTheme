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
    public class RListBoxWithBuiltInScrollBar : Control
    {
        public class RListBoxItem
        {
            public string Text
            {
                [DebuggerNonUserCode]
                get;
                [DebuggerNonUserCode]
                set;
            }

            [DebuggerNonUserCode]
            public RListBoxItem()
            {
            }

            public override string ToString()
            {
                return Text;
            }
        }

        private static List<WeakReference> __ENCList = new List<WeakReference>();

        private List<RListBoxItem> _Items;

        private readonly List<RListBoxItem> _SelectedItems;

        private bool _MultiSelect;

        private int ItemHeight;

        private readonly RVerticalScrollBar VerticalScrollbar;

        private Color _BaseColour;

        private Color _SelectedItemColour;

        private Color _NonSelectedItemColour;

        private Color _BorderColour;

        private Color _TextColour;

        private int _SelectedHeight;

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
        public int SelectedHeight
        {
            get
            {
                return _SelectedHeight;
            }
            set
            {
                if (value < 1)
                {
                    _SelectedHeight = Height;
                }
                else
                {
                    _SelectedHeight = value;
                }
                InvalidateScroll();
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
        public Color SelectedItemColour
        {
            get
            {
                return _SelectedItemColour;
            }
            set
            {
                _SelectedItemColour = value;
            }
        }

        [Category("Colours")]
        public Color NonSelectedItemColour
        {
            get
            {
                return _NonSelectedItemColour;
            }
            set
            {
                _NonSelectedItemColour = value;
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

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public RListBoxItem[] Items
        {
            get
            {
                return _Items.ToArray();
            }
            set
            {
                _Items = new List<RListBoxItem>(value);
                Invalidate();
                InvalidateScroll();
            }
        }

        public RListBoxItem[] SelectedItems => _SelectedItems.ToArray();

        public bool MultiSelect
        {
            get
            {
                return _MultiSelect;
            }
            set
            {
                _MultiSelect = value;
                if (_SelectedItems.Count > 1)
                {
                    _SelectedItems.RemoveRange(1, checked(_SelectedItems.Count - 1));
                }
                Invalidate();
            }
        }

        public override Font Font
        {
            get
            {
                return base.Font;
            }
            set
            {
                ItemHeight = checked((int)Math.Round(Graphics.FromHwnd(Handle).MeasureString("@", Font).Height));
                if (VerticalScrollbar != null)
                {
                    VerticalScrollbar._SmallChange = 1;
                    VerticalScrollbar._LargeChange = 1;
                }
                base.Font = value;
                InvalidateLayout();
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

        private void HandleScroll(object sender)
        {
            Invalidate();
        }

        private void InvalidateScroll()
        {
            Debug.Print(Conversions.ToString(Height));
            checked
            {
                if ((double)(int)Math.Round(Math.Round((double)(_Items.Count * ItemHeight) / (double)_SelectedHeight)) < (double)(_Items.Count * ItemHeight) / (double)_SelectedHeight)
                {
                    VerticalScrollbar._Maximum = (int)Math.Round(Math.Ceiling((double)(_Items.Count * ItemHeight) / (double)_SelectedHeight));
                }
                else if ((int)Math.Round(Math.Round((double)(_Items.Count * ItemHeight) / (double)_SelectedHeight)) == 0)
                {
                    VerticalScrollbar._Maximum = 1;
                }
                else
                {
                    VerticalScrollbar._Maximum = (int)Math.Round(Math.Round((double)(_Items.Count * ItemHeight) / (double)_SelectedHeight));
                }
                Invalidate();
            }
        }

        private void InvalidateLayout()
        {
            RVerticalScrollBar verticalScrollbar = VerticalScrollbar;
            checked
            {
                Point location = new Point(Width - VerticalScrollbar.Width - 2, 2);
                verticalScrollbar.Location = location;
                RVerticalScrollBar verticalScrollbar2 = VerticalScrollbar;
                Size size = new Size(18, Height - 4);
                verticalScrollbar2.Size = size;
                Invalidate();
            }
        }

        public void AddItem(string Items)
        {
            RListBoxItem RListBoxItem = new RListBoxItem();
            RListBoxItem.Text = Items;
            _Items.Add(RListBoxItem);
            Invalidate();
            InvalidateScroll();
        }

        public void AddItems(string[] Items)
        {
            foreach (string text in Items)
            {
                RListBoxItem RListBoxItem = new RListBoxItem();
                RListBoxItem.Text = text;
                _Items.Add(RListBoxItem);
            }
            Invalidate();
            InvalidateScroll();
        }

        public void RemoveItemAt(int index)
        {
            _Items.RemoveAt(index);
            Invalidate();
            InvalidateScroll();
        }

        public void RemoveItem(RListBoxItem item)
        {
            _Items.Remove(item);
            Invalidate();
            InvalidateScroll();
        }

        public void RemoveItems(RListBoxItem[] items)
        {
            foreach (RListBoxItem item in items)
            {
                _Items.Remove(item);
            }
            Invalidate();
            InvalidateScroll();
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            _SelectedHeight = Height;
            InvalidateScroll();
            InvalidateLayout();
            base.OnSizeChanged(e);
        }

        private void Vertical_MouseDown(object sender, MouseEventArgs e)
        {
            Focus();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            Focus();
            if (e.Button == MouseButtons.Left)
            {
                int num = checked(VerticalScrollbar.Value * (VerticalScrollbar.Maximum + (Height - ItemHeight)));
                int num2 = checked(e.Y + num) / ItemHeight;
                if (num2 > checked(_Items.Count - 1))
                {
                    num2 = -1;
                }
                if (num2 != -1)
                {
                    if ((Control.ModifierKeys == Keys.Control && _MultiSelect) ? true : false)
                    {
                        if (_SelectedItems.Contains(_Items[num2]))
                        {
                            _SelectedItems.Remove(_Items[num2]);
                        }
                        else
                        {
                            _SelectedItems.Add(_Items[num2]);
                        }
                    }
                    else
                    {
                        _SelectedItems.Clear();
                        _SelectedItems.Add(_Items[num2]);
                    }
                    Debug.Print(_SelectedItems[0].Text);
                }
                Invalidate();
            }
            base.OnMouseDown(e);
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            checked
            {
                int num = -(unchecked(checked(e.Delta * SystemInformation.MouseWheelScrollLines) / 120) * 1);
                int value = Math.Max(Math.Min(VerticalScrollbar.Value + num, VerticalScrollbar.Maximum), VerticalScrollbar.Minimum);
                VerticalScrollbar.Value = value;
                base.OnMouseWheel(e);
            }
        }

        public RListBoxWithBuiltInScrollBar()
        {
            __ENCAddToList(this);
            _Items = new List<RListBoxItem>();
            _SelectedItems = new List<RListBoxItem>();
            _MultiSelect = true;
            ItemHeight = 24;
            _BaseColour = Color.FromArgb(55, 55, 55);
            _SelectedItemColour = Color.FromArgb(50, 50, 50);
            _NonSelectedItemColour = Color.FromArgb(47, 47, 47);
            _BorderColour = Color.FromArgb(35, 35, 35);
            _TextColour = Color.FromArgb(255, 255, 255);
            _SelectedHeight = 1;
            SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.Selectable | ControlStyles.SupportsTransparentBackColor | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, value: true);
            DoubleBuffered = true;
            VerticalScrollbar = new RVerticalScrollBar();
            VerticalScrollbar._SmallChange = 1;
            VerticalScrollbar._LargeChange = 1;
            VerticalScrollbar.Scroll += HandleScroll;
            VerticalScrollbar.MouseDown += Vertical_MouseDown;
            Controls.Add(VerticalScrollbar);
            InvalidateLayout();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            Graphics graphics2 = graphics;
            graphics2.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            graphics2.SmoothingMode = SmoothingMode.HighQuality;
            graphics2.PixelOffsetMode = PixelOffsetMode.HighQuality;
            graphics2.Clear(_BaseColour);
            int num = checked(VerticalScrollbar.Value * (VerticalScrollbar.Maximum + (Height - ItemHeight)));
            int num2 = ((num != 0) ? (num / ItemHeight / VerticalScrollbar.Maximum) : 0);
            checked
            {
                int num3 = Math.Min(num2 + unchecked(Height / ItemHeight), _Items.Count - 1);
                graphics2.DrawLine(new Pen(_BorderColour, 2f), VerticalScrollbar.Location.X - 1, 0, VerticalScrollbar.Location.X - 1, Height);
                int num4 = _Items.Count - 1;
                int num5 = num2;
                while (true)
                {
                    int num6 = num5;
                    int num7 = num4;
                    if (num6 > num7)
                    {
                        break;
                    }
                    RListBoxItem RListBoxItem = Items[num5];
                    int num8 = num5 * ItemHeight + 1 - num + (int)Math.Round((double)ItemHeight / 2.0 - 8.0);
                    if (_SelectedItems.Contains(RListBoxItem))
                    {
                        Graphics graphics3 = graphics2;
                        SolidBrush brush = new SolidBrush(_SelectedItemColour);
                        Rectangle rect = new Rectangle(0, num5 * ItemHeight + 1 - num, Width - 19, ItemHeight - 1);
                        graphics3.FillRectangle(brush, rect);
                    }
                    else
                    {
                        Graphics graphics4 = graphics2;
                        SolidBrush brush2 = new SolidBrush(_NonSelectedItemColour);
                        Rectangle rect = new Rectangle(0, num5 * ItemHeight + 1 - num, Width - 19, ItemHeight - 1);
                        graphics4.FillRectangle(brush2, rect);
                    }
                    graphics2.DrawLine(new Pen(_BorderColour), 0, num5 * ItemHeight + 1 - num + ItemHeight - 1, Width - 18, num5 * ItemHeight + 1 - num + ItemHeight - 1);
                    graphics2.DrawString(RListBoxItem.Text, new Font("Segoe UI", 8f), new SolidBrush(_TextColour), 9f, num8);
                    graphics2.ResetClip();
                    num5++;
                }
                graphics2.DrawRectangle(new Pen(Color.FromArgb(35, 35, 35), 2f), 1, 1, Width - 2, Height - 2);
                graphics2.DrawLine(new Pen(_BorderColour, 2f), VerticalScrollbar.Location.X - 1, 0, VerticalScrollbar.Location.X - 1, Height);
                graphics2.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics2 = null;
            }
        }
    }
}