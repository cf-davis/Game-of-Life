using NUnit.Framework;
using GameModel;
using System.Collections.Generic;

namespace ModelTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        /// <summary>
        /// Creates a stable pattern of a specified form.  This pattern will not change 
        /// between generations when left alone.
        /// 
        /// Form codes and default sets:
        /// 1 - Block   {(0,0),(1,0),(0,1),(1,1)}
        /// 2 - BeeHive {(0,1),(1,0),(1,2),(2,0),(2,2),(3,1)}
        /// 
        /// Defaults to Block pattern
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        private HashSet<Cell> SetupStillLife(int form)
        {
            HashSet<Cell> cells = new HashSet<Cell>();

            switch (form)
            {
                case 1: default:
                    // block pattern
                    cells.Add(new Cell(0, 0));
                    cells.Add(new Cell(1, 0));
                    cells.Add(new Cell(0, 1));
                    cells.Add(new Cell(1, 1));
                    break;
                case 2:
                    // beehive pattern

                    // top row
                    cells.Add(new Cell(1, 2));
                    cells.Add(new Cell(2, 2));

                    // middle row
                    cells.Add(new Cell(0, 1));
                    cells.Add(new Cell(3, 1));

                    // bottom row
                    cells.Add(new Cell(1, 0));
                    cells.Add(new Cell(2, 0));
                    break;
            }

            return cells;
        }

        /// <summary>
        /// Creates a "blinker" pattern which, upon the game updating,
        /// should oscillate between {(0,1),(0,0),(0,-1)} and {(1,0),(0,0),(-1,0)}
        /// each update.  
        /// The period argument cycles which of the above sets are returned
        /// (1 for the first set, 2 for the second)
        /// The x and y arguments translate the pattern by the specified number of units
        /// in each direction
        /// </summary>
        /// <returns></returns>
        private HashSet<Cell> SetupBlinker(int period, int x, int y)
        {
            HashSet<Cell> cells = new HashSet<Cell>();

            if (period % 2 == 1)
            {
                // vertical bar
                cells.Add(new Cell(0 + x, -1 + y));
                cells.Add(new Cell(0 + x, 0 + y));
                cells.Add(new Cell(0 + x, 1 + y));
            }
            else
            {
                // horizontal bar
                cells.Add(new Cell(-1 + x, 0 + y));
                cells.Add(new Cell(0 + x, 0 + y));
                cells.Add(new Cell(1 + x, 0 + y));
            }
            

            return cells;
        }

        /// <summary>
        /// Creates a blinker pattern of a specified period with no offset from the origin
        /// </summary>
        /// <param name="period"></param>
        /// <returns></returns>
        private HashSet<Cell> SetupBlinker(int period)
        {
            return SetupBlinker(period, 0, 0);
        }

        /// <summary>
        /// Creates a "toad" pattern oscillator which will oscilate between 
        /// two six-celled patterns every other generation with a specifed offset
        /// from the origin
        /// </summary>
        /// <returns></returns>
        private HashSet<Cell> SetupToad(int period, int x, int y)
        {
            HashSet<Cell> cells = new HashSet<Cell>();

            if (period % 2 == 1)
            {
                // top row
                cells.Add(new Cell(-1 + x, 1 + y));
                cells.Add(new Cell(0 + x, 1 + y));
                cells.Add(new Cell(1 + x, 1 + y));

                // bottom row
                cells.Add(new Cell(0 + x, 0 + y));
                cells.Add(new Cell(-1 + x, 0 + y));
                cells.Add(new Cell(-2 + x, 0 + y));

            }
            else
            {
                // right wing
                cells.Add(new Cell(0 + x, 2 + y));
                cells.Add(new Cell(1 + x, 1 + y));
                cells.Add(new Cell(1 + x, 0 + y));

                // left wing
                cells.Add(new Cell(-2 + x, 1 + y));
                cells.Add(new Cell(-2 + x, 0 + y));
                cells.Add(new Cell(-1 + x, -1 + y));
            }

            return cells;
        }

        /// <summary>
        /// Creates a toad pattern of a specified period with no offset from the origin
        /// </summary>
        /// <param name="period"></param>
        /// <returns></returns>
        private HashSet<Cell> SetupToad(int period)
        {
            return SetupToad(period, 0, 0);
        }

        [Test]
        public void CheckCellHash()
        {
            Cell a = new Cell(0, 0);
            Cell b = new Cell(0, 0);
            Cell c = new Cell(1, 1);

            Assert.AreEqual(a.GetHashCode(), b.GetHashCode());
            Assert.AreNotEqual(a.GetHashCode(), c.GetHashCode());
        }

        [Test]
        public void CreateEmptyGame()
        {
            Life game = new Life();

            Assert.AreEqual(0, game.Generation);
            Assert.IsFalse(game.IsRunning);
        }

        [Test]
        public void CreateNonEmptyGame()
        {
            Life game = new Life(SetupBlinker(1));

            Assert.IsTrue(SetupBlinker(1).SetEquals(game.Cells));
            Assert.IsTrue(game.IsRunning);
        }

        [Test]
        public void SingleCellDies()
        {
            HashSet<Cell> cells = new HashSet<Cell>
            {
                new Cell(0, 0)
            };

            Life game = new Life(cells);

            game.UpdateCells();
            Assert.IsFalse(game.IsRunning);
            Assert.AreEqual(1, game.Generation);
        }

        [Test]
        public void EmptySimulationStopsUpdating()
        {
            HashSet<Cell> cells = new HashSet<Cell>
            {
                new Cell(0, 0)
            };

            Life game = new Life(cells);

            game.UpdateCells();
            Assert.AreEqual(1, game.Generation);

            game.UpdateCells();
            Assert.AreEqual(1, game.Generation);
        }

        [Test]
        public void UpdateStablePatterns()
        {
            Life game = new Life(SetupStillLife(1)); // test "Block"

            game.UpdateCells();
            Assert.IsTrue(SetupStillLife(1).SetEquals(game.Cells));

            game = new Life(SetupStillLife(2));  // test "BeeHive"

            game.UpdateCells();
            Assert.IsTrue(SetupStillLife(2).SetEquals(game.Cells));
        }

        [Test]
        public void UpdateBlinkerOscillator()
        {
            Life game = new Life(SetupBlinker(1));

            // update the pattern for 10 generations
            for (int i = 0; i < 10; i++)
            {
                Assert.AreEqual(i, game.Generation);
                game.UpdateCells();
                Assert.IsTrue(SetupBlinker(i).SetEquals(game.Cells));
            }

        }

        [Test]
        public void UpdateToadOscillator()
        {
            Life game = new Life(SetupToad(1));

            // update the pattern for 10 generations
            for (int i = 0; i < 10; i++)
            {
                Assert.AreEqual(i, game.Generation);
                game.UpdateCells();
                Assert.IsTrue(SetupToad(i).SetEquals(game.Cells));
            }

        }

    }
}