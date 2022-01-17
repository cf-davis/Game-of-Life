using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GameModel;

namespace Display
{

    class GraphicsPanel : Panel
    {

        // this should handle drawing everthing to the Form, see example from ps8

        private Life theGame;

        private int cellSize = 10;
        private const int CellOffset = 2;

        // potentially useful stuff --
        
        // Pen CellColor
        // ZoomDistance
        // Point CameraPosition?


        public GraphicsPanel(Life _theGame)
        {
            theGame = _theGame;

            DoubleBuffered = true;
            BackColor = Color.Black;

        }

        private void DrawCell(PaintEventArgs e, Cell c)
        {
            // translate grid position to image position (ie (0,0) should be center screen)

            System.Drawing.Drawing2D.Matrix matrix = e.Graphics.Transform.Clone();

            e.Graphics.TranslateTransform((int)c.X, (int)c.Y);
            //e.Graphics.RotateTransform(0);

            int cellX = (int)c.X * (cellSize) - (cellSize / 2);
            int cellY = -(int)c.Y * (cellSize + CellOffset) - (cellSize / 2);

            // probrably shouldn't cast to an int here...
            Rectangle cell = new Rectangle(cellX, cellY, cellSize, cellSize);

            e.Graphics.FillRectangle(new SolidBrush(Color.White), cell);

            e.Graphics.Transform = matrix;
        }

        protected override void OnPaint(PaintEventArgs e)
        {

            e.Graphics.DrawLine(new Pen(new SolidBrush(Color.Purple)), 
                Size.Width / 2, 0, Size.Width / 2, Size.Height);

            e.Graphics.DrawLine(new Pen(new SolidBrush(Color.Purple)),
                0, Size.Height / 2, Size.Width, Size.Height / 2);

            // center camera view (default to (0,0) "world" space)
            // change zeros to some camera position variable eventually
            e.Graphics.TranslateTransform((float)0 + (Size.Width / 2),
                    (float)0 + (Size.Height / 2));

            // draw each cell in it's position on the grid
            lock (theGame.cellLock)
            {
                foreach (Cell c in theGame.Cells)
                {
                    DrawCell(e, c);
                }
            }
        }

    }
}
