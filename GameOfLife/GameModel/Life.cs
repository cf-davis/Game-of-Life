using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameModel
{
    public class Life
    {

        // a collection of all "living" cells in the game
        public HashSet<Cell> Cells { get; }

        // the current zero-based iteration of the simulation
        public int Iteration { get; }

        // temporary constructor; should update to take in a provided set of cells 
        public Life() : this(new HashSet<Cell>())
        {
            
        }

        public Life(HashSet<Cell> _cells)
        {
            Cells = _cells;
            Iteration = 0;
        }

        /// <summary>
        /// Evaluates the condition af each living Cell and their adjacent Cells to determine
        /// which Cells remain alive in the next iteration of the simulation.  The rules for
        /// determining alive Cells are as follows:
        ///
        /// 1. Any live cell with two or three live neighbours survives.
        /// 2. Any dead cell with three live neighbours becomes a live cell.
        /// 3. All other live cells die in the next generation.Similarly, all other dead cells stay dead.
        /// (https://en.wikipedia.org/wiki/Conway%27s_Game_of_Life)
        /// </summary>
        public void UpdateCells()
        {
            throw new NotImplementedException();   
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

        // flags if a Cell will remain alive after a simulation update
        public bool alive; 

        // Cell[] adjacent; // adjacent Cells can be found from the position

        public Cell(long xPos, long yPos)
        {
            X = xPos;
            Y = yPos;
            alive = true;
        }

        public override string ToString() => "{" + X + ", " + Y + "}";

    }
}
