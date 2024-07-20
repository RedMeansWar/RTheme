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
    #pragma warning disable
    public class ROnOffSwitch : Control
    {
        public enum Toggles
        {
            Toggled,
            NotToggled
        }

        public delegate void ToggledChangedEventHandler();

        private static List<WeakReference> __ENCList = new List<WeakReference>();

        private Toggles _Toggled;

        private int MouseXLoc;

        private int ToggleLocation;

        private Color _BaseColour;

        private Color _BorderColour;

        private Color _TextColour;

        private Color _NonToggledTextColour;

        private Color _ToggledColour;

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

        [Category("Colours")]
        public Color NonToggledTextColourderColour
        {
            get
            {
                return _NonToggledTextColour;
            }
            set
            {
                _NonToggledTextColour = value;
            }
        }

        [Category("Colours")]
        public Color ToggledColour
        {
            get
            {
                return _ToggledColour;
            }
            set
            {
                _ToggledColour = value;
            }
        }

        public Toggles Toggled
        {
            get
            {
                return _Toggled;
            }
            set
            {
                _Toggled = value;
                Invalidate();
            }
        }

        [method: DebuggerNonUserCode]
        public event ToggledChangedEventHandler ToggledChanged;

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

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            MouseXLoc = e.Location.X;
            Invalidate();
            if ((e.X < checked(Width - 40) && e.X > 40) ? true : false)
            {
                Cursor = Cursors.IBeam;
            }
            else
            {
                Cursor = Cursors.Arrow;
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (MouseXLoc > checked(Width - 39))
            {
                _Toggled = Toggles.Toggled;
                ToggledValue();
            }
            else if (MouseXLoc < 39)
            {
                _Toggled = Toggles.NotToggled;
                ToggledValue();
            }
            Invalidate();
        }

        private void ToggledValue()
        {
            checked
            {
                if (_Toggled != 0)
                {
                    if (ToggleLocation < 100)
                    {
                        ToggleLocation += 10;
                    }
                }
                else if (ToggleLocation > 0)
                {
                    ToggleLocation -= 10;
                }
                Invalidate();
            }
        }

        public ROnOffSwitch()
        {
            __ENCAddToList(this);
            _Toggled = Toggles.NotToggled;
            ToggleLocation = 0;
            _BaseColour = Color.FromArgb(42, 42, 42);
            _BorderColour = Color.FromArgb(35, 35, 35);
            _TextColour = Color.FromArgb(255, 255, 255);
            _NonToggledTextColour = Color.FromArgb(155, 155, 155);
            _ToggledColour = Color.FromArgb(23, 119, 151);
            SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer, value: true);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            Graphics graphics2 = graphics;
            graphics2.Clear(Parent.FindForm().BackColor);
            graphics2.SmoothingMode = SmoothingMode.HighQuality;
            graphics2.PixelOffsetMode = PixelOffsetMode.HighQuality;
            graphics2.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            graphics2.InterpolationMode = InterpolationMode.HighQualityBicubic;
            Graphics graphics3 = graphics2;
            SolidBrush brush = new SolidBrush(_BaseColour);
            Rectangle rect = new Rectangle(0, 0, 39, Height);
            graphics3.FillRectangle(brush, rect);
            Graphics graphics4 = graphics2;
            SolidBrush brush2 = new SolidBrush(_BaseColour);
            checked
            {
                rect = new Rectangle(Width - 40, 0, Width, Height);
                graphics4.FillRectangle(brush2, rect);
                Graphics graphics5 = graphics2;
                SolidBrush brush3 = new SolidBrush(_BaseColour);
                rect = new Rectangle(38, 9, Width - 40, 5);
                graphics5.FillRectangle(brush3, rect);
                Point[] array = new Point[13];
                ref Point reference = ref array[0];
                Point point = new Point(0, 0);
                reference = point;
                ref Point reference2 = ref array[1];
                Point point2 = new Point(39, 0);
                reference2 = point2;
                ref Point reference3 = ref array[2];
                Point point3 = new Point(39, 9);
                reference3 = point3;
                ref Point reference4 = ref array[3];
                Point point4 = new Point(Width - 40, 9);
                reference4 = point4;
                ref Point reference5 = ref array[4];
                Point point5 = new Point(Width - 40, 0);
                reference5 = point5;
                ref Point reference6 = ref array[5];
                Point point6 = new Point(Width - 2, 0);
                reference6 = point6;
                ref Point reference7 = ref array[6];
                Point point7 = new Point(Width - 2, Height - 1);
                reference7 = point7;
                ref Point reference8 = ref array[7];
                Point point8 = new Point(Width - 40, Height - 1);
                reference8 = point8;
                ref Point reference9 = ref array[8];
                Point point9 = new Point(Width - 40, 14);
                reference9 = point9;
                ref Point reference10 = ref array[9];
                Point point10 = new Point(39, 14);
                reference10 = point10;
                ref Point reference11 = ref array[10];
                Point point11 = new Point(39, Height - 1);
                reference11 = point11;
                ref Point reference12 = ref array[11];
                Point point12 = new Point(0, Height - 1);
                reference12 = point12;
                array[12] = default(Point);
                Point[] points = array;
                graphics2.DrawLines(new Pen(_BorderColour, 2f), points);
                if (_Toggled == Toggles.Toggled)
                {
                    Graphics graphics6 = graphics2;
                    SolidBrush brush4 = new SolidBrush(_ToggledColour);
                    rect = new Rectangle((int)Math.Round((double)Width / 2.0), 10, (int)Math.Round((double)Width / 2.0 - 38.0), 3);
                    graphics6.FillRectangle(brush4, rect);
                    Graphics graphics7 = graphics2;
                    SolidBrush brush5 = new SolidBrush(_ToggledColour);
                    rect = new Rectangle(Width - 39, 2, 36, Height - 5);
                    graphics7.FillRectangle(brush5, rect);
                }
                if (_Toggled == Toggles.Toggled)
                {
                    Graphics graphics8 = graphics2;
                    Font font = new Font("Microsoft Sans Serif", 7f, FontStyle.Bold);
                    SolidBrush brush6 = new SolidBrush(_TextColour);
                    rect = new Rectangle(2, -1, (int)Math.Round((double)(Width - 20) + 6.666666666666667), Height);
                    graphics8.DrawString("ON", font, brush6, rect, new StringFormat
                    {
                        Alignment = StringAlignment.Far,
                        LineAlignment = StringAlignment.Center
                    });
                    Graphics graphics9 = graphics2;
                    Font font2 = new Font("Microsoft Sans Serif", 7f, FontStyle.Bold);
                    SolidBrush brush7 = new SolidBrush(_NonToggledTextColour);
                    rect = new Rectangle(7, -1, (int)Math.Round((double)(Width - 20) + 6.666666666666667), Height);
                    graphics9.DrawString("OFF", font2, brush7, rect, new StringFormat
                    {
                        Alignment = StringAlignment.Near,
                        LineAlignment = StringAlignment.Center
                    });
                }
                else if (_Toggled == Toggles.NotToggled)
                {
                    Graphics graphics10 = graphics2;
                    Font font3 = new Font("Microsoft Sans Serif", 7f, FontStyle.Bold);
                    SolidBrush brush8 = new SolidBrush(_TextColour);
                    rect = new Rectangle(7, -1, (int)Math.Round((double)(Width - 20) + 6.666666666666667), Height);
                    graphics10.DrawString("OFF", font3, brush8, rect, new StringFormat
                    {
                        Alignment = StringAlignment.Near,
                        LineAlignment = StringAlignment.Center
                    });
                    Graphics graphics11 = graphics2;
                    Font font4 = new Font("Microsoft Sans Serif", 7f, FontStyle.Bold);
                    SolidBrush brush9 = new SolidBrush(_NonToggledTextColour);
                    rect = new Rectangle(2, -1, (int)Math.Round((double)(Width - 20) + 6.666666666666667), Height);
                    graphics11.DrawString("ON", font4, brush9, rect, new StringFormat
                    {
                        Alignment = StringAlignment.Far,
                        LineAlignment = StringAlignment.Center
                    });
                }
                Graphics graphics12 = graphics2;
                Pen pen = new Pen(_BorderColour, 2f);
                Point point13 = new Point((int)Math.Round((double)Width / 2.0), 0);
                Point pt = point13;
                point12 = new Point((int)Math.Round((double)Width / 2.0), Height);
                graphics12.DrawLine(pen, pt, point12);
                graphics2 = null;
            }
        }
    }
}