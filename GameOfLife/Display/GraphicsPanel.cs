﻿using System;
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
        private int cellBorder;
        private const int CellOffset = 1;

        // potentially useful stuff --

        private Point camera;
        private const int PanSpeed = 10;
        private const int ZoomSpeed = 2;

        public enum Direction
        { 
            Up, Down, Left, Right
        };

        // Pen CellColor
        // ZoomDistance
        // Point CameraPosition?


        public GraphicsPanel(Life _theGame)
        {
            theGame = _theGame;
            camera = new Point(0, 0);

            DoubleBuffered = true;
            BackColor = Color.Black;

            cellBorder = cellSize / 10;
        }

        public void ZoomOut()
        {
            if (cellSize <= 2)
                return;

            cellSize -= ZoomSpeed;
            cellBorder = cellSize / 10;
        }

        public void ZoomIn()
        {
            if (cellSize >= 50)
                return;

            cellSize += ZoomSpeed;
            cellBorder = cellSize / 10;
        }

        public void Pan(Direction d)
        { 
            switch (d)
            {
                case Direction.Up:
                    camera.Y += PanSpeed;
                    break;
                case Direction.Down:
                    camera.Y -= PanSpeed;
                    break;
                case Direction.Right:
                    camera.X -= PanSpeed;
                    break;
                case Direction.Left:
                    camera.X += PanSpeed;
                    break;
            }

        }

        private void DrawCell(PaintEventArgs e, Cell c)
        {
            System.Drawing.Drawing2D.Matrix matrix = e.Graphics.Transform.Clone();

            e.Graphics.TranslateTransform(c.X, c.Y);

            int cellX = (int)c.X * (cellSize - CellOffset) - (cellSize / 2);
            int cellY = -(int)c.Y * (cellSize + CellOffset) - (cellSize / 2);

            Rectangle cell = new Rectangle(cellX, cellY, cellSize - cellBorder, cellSize - cellBorder);

            e.Graphics.FillRectangle(new SolidBrush(Color.White), cell);

            e.Graphics.Transform = matrix;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            //DrawDebugGrid(e);

            DrawDebugCenterlines(e);

            // center camera view (default to (0,0) "world" space)
            // change zeros to some camera position variable eventually
            e.Graphics.TranslateTransform((float)camera.X + (Size.Width / 2),
                    (float)camera.Y + (Size.Height / 2));

            // draw each cell in it's position on the grid
            lock (theGame.cellLock)
            {
                foreach (Cell c in theGame.Cells)
                {
                    DrawCell(e, c);
                }
            }
        }

        private void DrawDebugCenterlines(PaintEventArgs e)
        {
            // vertical line
            e.Graphics.DrawLine(new Pen(new SolidBrush(Color.Yellow)),
                Size.Width / 2, 0, Size.Width / 2, Size.Height);

            // horizontal line
            e.Graphics.DrawLine(new Pen(new SolidBrush(Color.Yellow)),
                0, Size.Height / 2, Size.Width, Size.Height / 2);
        }

        private void DrawDebugGrid(PaintEventArgs e)
        {

            for (int x = cellSize; x <= Width; x += cellSize)
            {
                int xPos = x - (cellSize / 2);
                e.Graphics.DrawLine(new Pen(new SolidBrush(Color.Purple)), xPos, 0, xPos, Height);
            }

            for (int y = cellSize; y <= Height; y += cellSize)
            {
                int yPos = y - (cellSize / 2);
                e.Graphics.DrawLine(new Pen(new SolidBrush(Color.Purple)), 0, yPos, Width, yPos);
            }

        }

    }
}
