using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyPaint
{
    public partial class Form1 : Form
    {
        Point begin, end;
        bool isButtomPressed = false;
        Pen p = new Pen(Color.Black, 2);
        List<Figure> Figures = new List<Figure>();
        MyLine currentLine = new MyLine();
        MyLine currentErase = new MyLine();
        MyPen currentPencil = new MyPen();
        MyRectangle currentRectangle = new MyRectangle();
        MyEllipse currentEllipse = new MyEllipse();
        ToolsType tools = ToolsType.None;
        Bitmap bmp;
      
        public Form1()
        {
            InitializeComponent();
            currentPencil.Pen = p;
            currentLine.Pen = p;
            currentRectangle.Pen = p;
            currentEllipse.Pen = p;
            currentErase.Pen = new Pen(Brushes.Transparent, 4);
            bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
           
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            isButtomPressed = true;
            switch(tools)
            {
                case ToolsType.Pen:
                    begin = e.Location;
                    break;
                case ToolsType.Line:
                    begin = e.Location;
                    currentLine.Begin = begin;
                    break;
                case ToolsType.Rectangle:
                    begin = e.Location;
                    currentRectangle.Begin = e.Location;
                    break;
                case ToolsType.Ellipse:
                    begin = e.Location;
                    currentEllipse.Begin = e.Location;
                    break;
                case ToolsType.Eraser:
                    begin = e.Location;
                    break;
            }           
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {          
            isButtomPressed = false;           
                switch (tools)
                {
                    case ToolsType.Line:
                        Figures.Add(new MyLine() { Begin = begin, End = end, Pen = p });
                        currentLine.Clear();
                        break;
                    case ToolsType.Rectangle:
                        Figures.Add(new MyRectangle() { Begin = begin, End = end, Pen = p });
                        currentRectangle.Clear();
                        break;
                    case ToolsType.Ellipse:
                        Figures.Add(new MyEllipse() { Begin = begin, End = end, Pen = p });
                        currentEllipse.Clear();
                        break;
                }           
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            string str = string.Format("Coordinates - X: {0}  Y: {1}", e.X, e.Y);
            toolStripStatusLabel1.Text = str;
            if (toolStripComboBox1.SelectedItem != null)
            {
                p = new Pen(colorDialog1.Color, float.Parse(toolStripComboBox1.SelectedItem.ToString()));
            }
            if (toolStripComboBox1.SelectedItem == null)
            {
                p = new Pen(colorDialog1.Color, float.Parse(toolStripComboBox1.Items[1].ToString()));
            }
            p.EndCap = LineCap.Round;
            p.StartCap = LineCap.Round;
          
            if (isButtomPressed)
            {
                switch(tools)
                {
                    case ToolsType.Pen:
                        end = e.Location;
                        Figures.Add(new MyPen { Begin = begin, End = end, Pen = p });          
                        begin = end;
                        break;
                    case ToolsType.Line:                     
                        end = e.Location;
                        currentLine.End = end;                       
                        break;
                    case ToolsType.Rectangle:
                        end = e.Location;
                        currentRectangle.End = e.Location; 
                        currentRectangle = new MyRectangle { Begin = begin, End = end, Pen = p };
                        break;
                    case ToolsType.Ellipse:
                        end = e.Location;
                        currentEllipse.End = e.Location;
                        currentEllipse = new MyEllipse { Begin = begin, End = end, Pen = p };
                        break;
                    case ToolsType.Eraser:
                        end = e.Location;
                        Figures.Add(new MyEraser { Begin = begin, End = end, Pen = new Pen(BackColor, 6) });
                        begin = end;
                        break;
                }
              
                pictureBox1.Invalidate();
            }           
        }
        private void ellipse_Click(object sender, EventArgs e)
        {
            tools = ToolsType.Ellipse;
        }
        private void line_Click(object sender, EventArgs e)
        {
            tools = ToolsType.Line;
        }
        private void pen_Click(object sender, EventArgs e)
        {
            tools = ToolsType.Pen;
        }
        private void rectangle_Click(object sender, EventArgs e)
        {
            tools = ToolsType.Rectangle;
        }
        private void brush_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
        }
        private void clear_Click(object sender, EventArgs e)
        {
            Figures.Clear();
            pictureBox1.Invalidate();
            p = new Pen(Color.Black);
        }
        private void erase_Click(object sender, EventArgs e)
        {
            tools = ToolsType.Eraser;
        }

        private void save_Click(object sender, EventArgs e)
        {          
            saveFileDialog1.Filter = "png files (*.png)|*.png";
            saveFileDialog1.ShowDialog();
            if (saveFileDialog1.FileName != "" )
            {
                pictureBox1.DrawToBitmap(bmp, new Rectangle(Point.Empty, bmp.Size));
                bmp.Save(saveFileDialog1.FileName);                
            }           
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            pictureBox1.Load(openFileDialog1.FileName);           
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want to save image?", "Message", MessageBoxButtons.YesNoCancel);
            if (result == DialogResult.Yes)
            {
                saveFileDialog1.Filter = "png files (*.png)|*.png";
                saveFileDialog1.ShowDialog();
                if (saveFileDialog1.FileName != "")
                {
                    pictureBox1.DrawToBitmap(bmp, new Rectangle(Point.Empty, bmp.Size));
                    bmp.Save(saveFileDialog1.FileName);
                }
            }
            if (result == DialogResult.No)
            {
                Figures.Clear();
                pictureBox1.Invalidate();
                p = new Pen(Color.Black);
            }
            if (result == DialogResult.Cancel)
            {
                return;
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "png files (*.png)|*.png";
            saveFileDialog1.ShowDialog();
            if (saveFileDialog1.FileName != "")
            {
                pictureBox1.DrawToBitmap(bmp, new Rectangle(Point.Empty, bmp.Size));
                bmp.Save(saveFileDialog1.FileName);
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "png files (*.png)|*.png|jpg files (*.jpg)|*.jpg|All files (*.*)|*.*";
            saveFileDialog1.ShowDialog();
            if (saveFileDialog1.FileName != "")
            {
                pictureBox1.DrawToBitmap(bmp, new Rectangle(Point.Empty, bmp.Size));
                bmp.Save(saveFileDialog1.FileName);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pencilToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tools = ToolsType.Pen;
        }

        private void brushToolStripMenuItem_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
        }

        private void lineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tools = ToolsType.Line;
        }

        private void rectangleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tools = ToolsType.Rectangle;
        }

        private void ellipseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tools = ToolsType.Ellipse;
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.HighQuality;
            currentPencil.Draw(g);
            currentLine.Draw(g);
            currentErase.Draw(g);
            currentRectangle.Draw(g);
            currentEllipse.Draw(g);
            foreach (var item in Figures)
            {
                item.Draw(g);              
            }
        }
    }
}
