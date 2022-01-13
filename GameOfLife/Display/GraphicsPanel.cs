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

        // potentially useful stuff --
        // int CellSize
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

            //e.Graphics.TranslateTransform(Size.Width, Size.Height);

            // probrably shouldn't cast to an int here...
            Rectangle cell = new Rectangle((int)c.X, (int)c.Y, 10, 10);

            e.Graphics.DrawRectangle(new Pen(Color.White), cell);

            //e.Graphics.Transform = matrix;
        }

        protected override void OnPaint(PaintEventArgs e)
        {

            // draw each cell in it's position on the grid

            foreach (Cell c in theGame.Cells)
            {
                DrawCell(e, c);
                e.Graphics.DrawString(c.ToString(), DefaultFont, new SolidBrush(Color.White), 10, 10);
            }

        }

    }
}
