using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPaint
{
    class MyRectangle : Figure
    {     
        public int Width { get; set; }
        public int Height { get; set; }
        public override void Draw(Graphics g)
        {
            int x, y;
            Width = Math.Abs(Begin.Y - End.Y);
            Height = Math.Abs(Begin.X - End.Y);
            if (Begin.X < End.X) { x = Begin.X; }
            else { x = End.X; }
            if (Begin.Y < End.Y) { y = Begin.Y; }
            else { y = End.Y; }

            g.DrawRectangle(Pen, new Rectangle(new Point (x, y), new Size(Width,Height)));
        }

    }
}
