using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoListManager
{
    public static class Util
    {
        public static Color FromRGBA(int rgb, byte alpha = 255)
        {
            return Color.FromArgb(rgb | (alpha << 24));
        }
    }
}
