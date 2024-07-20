using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace RTheme
{
    public class RContextMenu : ContextMenuStrip
    {
        private static List<WeakReference> __ENCList = new List<WeakReference>();

        private Color _FontColour;

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

        public RContextMenu()
        {
            __ENCAddToList(this);
            _FontColour = Color.FromArgb(55, 255, 255);
            Renderer = new ToolStripProfessionalRenderer(new RColorTable());
            ShowCheckMargin = false;
            ShowImageMargin = false;
            ForeColor = Color.FromArgb(255, 255, 255);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            base.OnPaint(e);
        }
    }
}