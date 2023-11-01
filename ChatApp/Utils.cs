using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp
{
    public static class Utils
    {
        public static int GetTextHeight(Label label)
        {
            using(Graphics g = label.CreateGraphics())
            {
                SizeF size = g.MeasureString(label.Text, label.Font, 220);
                return (int)Math.Ceiling(size.Height);
            }
        }
    }
}
