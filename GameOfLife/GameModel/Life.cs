﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameModel
{
    public class Life
    {

        // a collection of all "living" cells in the game
        public HashSet<Cell> Cells { get; private set; }

        // the current zero-based iteration of the simulation
        public int Generation { get; private set; }

        // temporary constructor; should update to take in a provided set of cells 
        public Life() : this(new HashSet<Cell>())
        {
            
        }

        public Life(HashSet<Cell> _cells)
        {
            Cells = _cells;
            Generation = 0;
        }

        /// <summary>
        /// Evaluates the condition af each living Cell and their adjacent Cells to determine
        /// which Cells remain alive in the next iteration of the simulation.  The rules for
        /// determining alive Cells are as follows:
        ///
        /// 1. Any live cell with two or three live neighbours survives.
        /// 2. Any dead cell with three live neighbours becomes a live cell.
        /// 3. All other live cells die in the next generation. Similarly, all other dead cells stay dead.
        /// (https://en.wikipedia.org/wiki/Conway%27s_Game_of_Life)
        /// </summary>
        public void UpdateCells()
        {

            /*
             * loop through live cells:
             * - check adjacent cells of each cell:
             *      - add dead cells to a seperate list
             *      - apply rules based on live cells
             * - check adjacents of dead cells and apply rules
             * 
             * update generation (this.Cells)
             */

            HashSet<Cell> nextGeneration = new HashSet<Cell>();

            foreach (Cell c in Cells)
            {
                HashSet<Cell> deadCells = GetDeadAdjacents(c);

                // check for 2 or 3 live Cells adjavcent to this Cell
                if (deadCells.Count == 5 || deadCells.Count == 6)
                {
                    nextGeneration.Add(c);
                }

                foreach (Cell deadCell in deadCells)
                {
                    if (!nextGeneration.Contains(deadCell))
                        if (GetDeadAdjacents(deadCell).Count == 5)
                            nextGeneration.Add(deadCell);
                }

            }

            Cells = nextGeneration;
            Generation++;
        }

        /// <summary>
        /// Retuns the number of "dead" Cells adjacent to the
        /// provided Cell
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        private HashSet<Cell> GetDeadAdjacents(Cell c)
        {
            HashSet<Cell> deadCells = new HashSet<Cell>();

            for (int x = -1; x <= 1; x++)
                for (int y = -1; y <= 1; y++)
                {
                    if (x == 0 && y == 0)
                        continue;

                    Cell adj = new Cell(c.X + x, c.Y + y);
                    if (!Cells.Contains(adj))
                        deadCells.Add(adj);
                        
                }

            return deadCells;
        }


    }

    /// <summary>
    /// Represents a Cell with grid coordinates and an "alive" flag 
    /// in the Game of Life
    /// </summary>
    public struct Cell
    {

        // positional data of the Cell in the grid
        public long X { get; }
        public long Y { get; }

        //// flags if a Cell will remain alive after a simulation update
        //public bool alive; 

        // Cell[] adjacent; // adjacent Cells can be found from the position

        public Cell(long xPos, long yPos)
        {
            X = xPos;
            Y = yPos;
            // alive = true;
        }

        public override string ToString() => "{" + X + ", " + Y + "}";

        public override int GetHashCode() => ToString().GetHashCode();

        public override bool Equals(object obj)
        {
            if (obj is null || !(obj is Cell))
                return false;

            return GetHashCode() == obj.GetHashCode();
        }

    }
}
