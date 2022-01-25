using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GameModel;

namespace GameController
{
    public class Controller
    {
        /*
         * - Create a new game and wait for the display to 
         * invoke methods
         * - Create events to notify display of game updates
         * - Update game on regular cycle 
         */

        public delegate void GameUpdateHandler();
        public event GameUpdateHandler GameUpdate;

        public Life TheGame { get; private set; }

        public Controller()
        {
            TheGame = new Life();
        }

        public void UpdateInitialCells(int x, int y, int cellSize, Size window, bool addCell)
        {

            // convert mouse coords to cartesian
            x -= (window.Width/ 2);
            y = (window.Height / 2 - y);

            // scale position by size of cells
            x /= cellSize;
            y /= cellSize;

            Cell c = new Cell(x, y);

            // try to add/delete a cell based on addCell flag
            lock (TheGame.cellLock)
            {
                if (addCell)
                {
                    if (!TheGame.Cells.Contains(c))
                        TheGame.Cells.Add(c);
                }
                else
                {
                    if (TheGame.Cells.Contains(c))
                        TheGame.Cells.Remove(c);
                }
            }

            GameUpdate?.Invoke();
        }

        public void StartSimulation()
        {
            TheGame.SetInitialCells(TheGame.Cells); 
            Thread update = new Thread(new ThreadStart(UpdateSimulation));
            update.Start();
        }

        private void UpdateSimulation()
        {
            /*
             * - set a tick rate and an infitite loop that constantly updates
             * the cells in the model and notifies the view
             * - end simulation when model hashset is empty (maybe not here)
             * 
             */

            Stopwatch delay = new Stopwatch();

            int frameDelay = 17; // ms between frames
            int updateDelay = 200; // ms between cell updates

            while (TheGame.IsRunning)
            {
                //add some delay here-- research a better way than the busy loop
                // temporary busy loop just to make sure everything works
                delay.Start();

                while (delay.ElapsedMilliseconds < updateDelay)
                {
                    if (delay.ElapsedMilliseconds % frameDelay == 0)
                        GameUpdate?.Invoke();
                }

                delay.Reset();

                TheGame.UpdateCells();
                GameUpdate?.Invoke();
            }

        }

    }
}
