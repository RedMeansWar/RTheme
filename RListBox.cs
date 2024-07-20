using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace RTheme
{
    public class RListBox : Control
    {
        private static List<WeakReference> __ENCList = new List<WeakReference>();

        [AccessedThroughProperty("ListB")]
        private ListBox _ListB;

        private string[] _Items;

        private Color _BaseColour;

        private Color _SelectedColour;

        private Color _ListBaseColour;

        private Color _TextColour;

        private Color _BorderColour;

        protected virtual ListBox ListB
        {
            [DebuggerNonUserCode]
            get
            {
                return _ListB;
            }
            [MethodImpl(MethodImplOptions.Synchronized)]
            [DebuggerNonUserCode]
            set
            {
                DrawItemEventHandler value2 = Drawitem;
                if (_ListB != null)
                {
                    _ListB.DrawItem -= value2;
                }
                _ListB = value;
                if (_ListB != null)
                {
                    _ListB.DrawItem += value2;
                }
            }
        }

        [Category("Control")]
        public string[] Items
        {
            get
            {
                return _Items;
            }
            set
            {
                _Items = value;
                ListB.Items.Clear();
                ListB.Items.AddRange(value);
                Invalidate();
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
        public Color SelectedColour
        {
            get
            {
                return _SelectedColour;
            }
            set
            {
                _SelectedColour = value;
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
        public Color ListBaseColour
        {
            get
            {
                return _ListBaseColour;
            }
            set
            {
                _ListBaseColour = value;
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

        public string SelectedItem => Conversions.ToString(ListB.SelectedItem);

        public int SelectedIndex => ListB.SelectedIndex;

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

        public void Clear()
        {
            ListB.Items.Clear();
        }

        public void ClearSelected()
        {
            checked
            {
                int num = ListB.SelectedItems.Count - 1;
                while (true)
                {
                    int num2 = num;
                    int num3 = 0;
                    if (num2 >= num3)
                    {
                        ListB.Items.Remove(RuntimeHelpers.GetObjectValue(ListB.SelectedItems[num]));
                        num += -1;
                        continue;
                    }
                    break;
                }
            }
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            if (!Controls.Contains(ListB))
            {
                Controls.Add(ListB);
            }
        }

        public void AddRange(object[] items)
        {
            ListB.Items.Remove("");
            ListB.Items.AddRange(items);
        }

        public void AddItem(object item)
        {
            ListB.Items.Remove("");
            ListB.Items.Add(RuntimeHelpers.GetObjectValue(item));
        }

        public void Drawitem(object sender, DrawItemEventArgs e)
        {
            checked
            {
                if (e.Index >= 0)
                {
                    e.DrawBackground();
                    e.DrawFocusRectangle();
                    Graphics graphics = e.Graphics;
                    graphics.SmoothingMode = SmoothingMode.HighQuality;
                    graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                    if (Strings.InStr(e.State.ToString(), "Selected,") > 0)
                    {
                        Graphics graphics2 = graphics;
                        SolidBrush brush = new SolidBrush(_SelectedColour);
                        Rectangle rect = new Rectangle(e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height - 1);
                        graphics2.FillRectangle(brush, rect);
                        graphics.DrawString(" " + ListB.Items[e.Index].ToString(), new Font("Segoe UI", 9f, FontStyle.Bold), new SolidBrush(_TextColour), e.Bounds.X, e.Bounds.Y + 2);
                    }
                    else
                    {
                        Graphics graphics3 = graphics;
                        SolidBrush brush2 = new SolidBrush(_ListBaseColour);
                        Rectangle rect2 = new Rectangle(e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height);
                        graphics3.FillRectangle(brush2, rect2);
                        graphics.DrawString(" " + ListB.Items[e.Index].ToString(), new Font("Segoe UI", 8f), new SolidBrush(_TextColour), e.Bounds.X, e.Bounds.Y + 2);
                    }
                    graphics.Dispose();
                    graphics = null;
                }
            }
        }

        public RListBox()
        {
            __ENCAddToList(this);
            ListB = new ListBox();
            _Items = new string[1] { "" };
            _BaseColour = Color.FromArgb(42, 42, 42);
            _SelectedColour = Color.FromArgb(55, 55, 55);
            _ListBaseColour = Color.FromArgb(47, 47, 47);
            _TextColour = Color.FromArgb(255, 255, 255);
            _BorderColour = Color.FromArgb(35, 35, 35);
            SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, value: true);
            DoubleBuffered = true;
            ListB.DrawMode = DrawMode.OwnerDrawFixed;
            ListB.ScrollAlwaysVisible = false;
            ListB.HorizontalScrollbar = false;
            ListB.BorderStyle = BorderStyle.None;
            ListB.BackColor = _BaseColour;
            ListBox listB = ListB;
            Point location = new Point(3, 3);
            listB.Location = location;
            ListB.Font = new Font("Segoe UI", 8f);
            ListB.ItemHeight = 20;
            ListB.Items.Clear();
            ListB.IntegralHeight = false;
            Size size = new Size(130, 100);
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
            graphics2.Clear(BackColor);
            ListBox listB = ListB;
            checked
            {
                Size size = new Size(Width - 6, Height - 5);
                listB.Size = size;
                graphics2.FillRectangle(new SolidBrush(_BaseColour), rect);
                Graphics graphics3 = graphics2;
                Pen pen = new Pen(_BorderColour, 3f);
                Rectangle rect2 = new Rectangle(0, 0, Width, Height - 1);
                graphics3.DrawRectangle(pen, rect2);
                graphics2.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics2 = null;
            }
        }
    }
}