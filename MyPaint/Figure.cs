using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPaint
{
    abstract class Figure
    {
        public Point Begin { get; set; }
        public Point End { get; set; }
        public Pen Pen { get; set; }
        public abstract void Draw(Graphics g);

        virtual public void Clear()
        {
            Begin = new Point(0, 0);
            End = new Point(0, 0);
        }
    }
          
}
