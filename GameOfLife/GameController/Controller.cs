using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        public void StartSimulation(HashSet<Cell> initCells)
        {
            TheGame.SetInitialCells(initCells);
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
            int updateDelay = 500; // ms between cell updates

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
