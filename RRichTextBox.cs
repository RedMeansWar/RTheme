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
    public class RRichTextBox : Control
    {
        private static List<WeakReference> __ENCList = new List<WeakReference>();

        [AccessedThroughProperty("TB")]
        private RichTextBox _TB;

        private Color _BaseColour;

        private Color _TextColour;

        private Color _BorderColour;

        protected virtual RichTextBox TB
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

        public override string Text
        {
            get
            {
                return TB.Text;
            }
            set
            {
                TB.Text = value;
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

        public void AppendText(string AppendingText)
        {
            TB.Focus();
            TB.AppendText(AppendingText);
            Invalidate();
        }

        protected override void OnBackColorChanged(EventArgs e)
        {
            base.OnBackColorChanged(e);
            TB.BackColor = BackColor;
            Invalidate();
        }

        protected override void OnForeColorChanged(EventArgs e)
        {
            base.OnForeColorChanged(e);
            TB.ForeColor = ForeColor;
            Invalidate();
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            RichTextBox tB = TB;
            Size size = checked(new Size(Width - 10, Height - 11));
            tB.Size = size;
        }

        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            TB.Font = Font;
        }

        public void TextChanges()
        {
            TB.Text = Text;
        }

        public RRichTextBox()
        {
            base.TextChanged += [SpecialName][DebuggerStepThrough] (object a0, EventArgs a1) =>
            {
                TextChanges();
            };
            __ENCAddToList(this);
            TB = new RichTextBox();
            _BaseColour = Color.FromArgb(42, 42, 42);
            _TextColour = Color.FromArgb(255, 255, 255);
            _BorderColour = Color.FromArgb(35, 35, 35);
            RichTextBox tB = TB;
            tB.Multiline = true;
            tB.BackColor = _BaseColour;
            tB.ForeColor = _TextColour;
            tB.Text = string.Empty;
            tB.BorderStyle = BorderStyle.None;
            RichTextBox richTextBox = tB;
            Point location = new Point(5, 5);
            richTextBox.Location = location;
            tB.Font = new Font("Segeo UI", 9f);
            RichTextBox richTextBox2 = tB;
            Size size = checked(new Size(Width - 10, Height - 10));
            richTextBox2.Size = size;
            tB = null;
            Controls.Add(TB);
            size = new Size(135, 35);
            Size = size;
            DoubleBuffered = true;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            Rectangle rectangle = checked(new Rectangle(0, 0, Width - 1, Height - 1));
            Graphics graphics2 = graphics;
            graphics2.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            graphics2.SmoothingMode = SmoothingMode.HighQuality;
            graphics2.PixelOffsetMode = PixelOffsetMode.HighQuality;
            graphics2.Clear(_BaseColour);
            graphics2.DrawRectangle(new Pen(_BorderColour, 2f), ClientRectangle);
            graphics2.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics2 = null;
        }
    }
}