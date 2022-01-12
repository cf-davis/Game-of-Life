using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        private Life game;

        public Controller()
        {

            
        }

        public Life StartSimulation(HashSet<Cell> initCells)
        {
            game = new Life(initCells);
            UpdateSimulation();

            return game;
        }

        private void UpdateSimulation()
        {
            /*
             * - set a tick rate and an infitite loop that constantly updates
             * the cells in the model and notifies the view
             * - end simulation when model hashset is empty (maybe not here)
             * 
             */

            while (game.IsRunning)
            {
                // add some delay here-- research best way to do that

                game.UpdateCells();
                GameUpdate?.Invoke();
            }

        }

    }
}
