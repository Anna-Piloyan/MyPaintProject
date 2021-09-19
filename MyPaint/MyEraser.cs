using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPaint
{
    class MyEraser : Figure
    {
        public override void Draw(Graphics g)
        {
            g.DrawLine(Pen, Begin, End);
        }
    }
}
