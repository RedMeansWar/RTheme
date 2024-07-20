using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;

namespace RTheme
{
    [StandardModule]
    public sealed class DrawHelper
    {
        public enum MouseState : byte
        {
            None,
            Over,
            Down,
            Block
        }

        public static GraphicsPath RoundRectangle(Rectangle Rectangle, int Curve)
        {
            GraphicsPath graphicsPath = new GraphicsPath();
            checked
            {
                int num = Curve * 2;
                Rectangle rect = new Rectangle(Rectangle.X, Rectangle.Y, num, num);
                graphicsPath.AddArc(rect, -180f, 90f);
                rect = new Rectangle(Rectangle.Width - num + Rectangle.X, Rectangle.Y, num, num);
                graphicsPath.AddArc(rect, -90f, 90f);
                rect = new Rectangle(Rectangle.Width - num + Rectangle.X, Rectangle.Height - num + Rectangle.Y, num, num);
                graphicsPath.AddArc(rect, 0f, 90f);
                rect = new Rectangle(Rectangle.X, Rectangle.Height - num + Rectangle.Y, num, num);
                graphicsPath.AddArc(rect, 90f, 90f);
                Point pt = new Point(Rectangle.X, Rectangle.Height - num + Rectangle.Y);
                Point pt2 = new Point(Rectangle.X, Curve + Rectangle.Y);
                graphicsPath.AddLine(pt, pt2);
                return graphicsPath;
            }
        }

        public static GraphicsPath RoundRect(float x, float y, float w, float h, float r = 0.3f, bool TL = true, bool TR = true, bool BR = true, bool BL = true)
        {
            float num = Math.Min(w, h) * r;
            float num2 = x + w;
            float num3 = y + h;
            GraphicsPath graphicsPath = new GraphicsPath();
            GraphicsPath graphicsPath2 = graphicsPath;
            if (TL)
            {
                graphicsPath2.AddArc(x, y, num, num, 180f, 90f);
            }
            else
            {
                graphicsPath2.AddLine(x, y, x, y);
            }
            if (TR)
            {
                graphicsPath2.AddArc(num2 - num, y, num, num, 270f, 90f);
            }
            else
            {
                graphicsPath2.AddLine(num2, y, num2, y);
            }
            if (BR)
            {
                graphicsPath2.AddArc(num2 - num, num3 - num, num, num, 0f, 90f);
            }
            else
            {
                graphicsPath2.AddLine(num2, num3, num2, num3);
            }
            if (BL)
            {
                graphicsPath2.AddArc(x, num3 - num, num, num, 90f, 90f);
            }
            else
            {
                graphicsPath2.AddLine(x, num3, x, num3);
            }
            graphicsPath2.CloseFigure();
            graphicsPath2 = null;
            return graphicsPath;
        }
    }

    public class RColorTable : ProfessionalColorTable
    {
        private Color _BackColour;

        private Color _BorderColour;

        private Color _SelectedColour;

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
        public Color BackColour
        {
            get
            {
                return _BackColour;
            }
            set
            {
                _BackColour = value;
            }
        }

        public override Color ButtonSelectedBorder => _BackColour;

        public override Color CheckBackground => _BackColour;

        public override Color CheckPressedBackground => _BackColour;

        public override Color CheckSelectedBackground => _BackColour;

        public override Color ImageMarginGradientBegin => _BackColour;

        public override Color ImageMarginGradientEnd => _BackColour;

        public override Color ImageMarginGradientMiddle => _BackColour;

        public override Color MenuBorder => _BorderColour;

        public override Color MenuItemBorder => _BackColour;

        public override Color MenuItemSelected => _SelectedColour;

        public override Color SeparatorDark => _BorderColour;

        public override Color ToolStripDropDownBackground => _BackColour;

        public RColorTable()
        {
            _BackColour = Color.FromArgb(42, 42, 42);
            _BorderColour = Color.FromArgb(35, 35, 35);
            _SelectedColour = Color.FromArgb(47, 47, 47);
        }
    }
}
