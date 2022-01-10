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
        /// Creates a "blinker" pattern which, upon the game updating,
        /// should oscillate between {(0,1),(0,0),(0,-1)} and {(1,0),(0,0),(-1,0)}
        /// each update.  The state of the returned blinker is vertical (the 
        /// former of the listed sets)
        /// </summary>
        /// <returns></returns>
        private HashSet<Cell> SetupSimpleOscillator()
        {
            HashSet<Cell> cells = new HashSet<Cell>
            {
                new Cell(0, 1),
                new Cell(0, 0),
                new Cell(0, -1)
            };

            return cells;
        }

        [Test]
        public void CreateEmptyGame()
        {
            Life game = new Life();

            Assert.AreEqual(0, game.Cells.Count);
        }

        [Test]
        public void CreateNonEmptyGame()
        {
            HashSet<Cell> verticalOsc = new HashSet<Cell>()
            {
                new Cell(0, -1),
                new Cell(0, 0),
                new Cell(0, 1)
            };

            Life game = new Life(SetupSimpleOscillator());

            Assert.AreEqual(verticalOsc, game.Cells);
        }

        [Test]
        public void UpdateSimpleOscillator()
        {
            HashSet<Cell> verticalOsc = new HashSet<Cell>()
            {
                new Cell(0, -1),
                new Cell(0, 0),
                new Cell(0, 1)
            };

            HashSet<Cell> horizontalOsc = new HashSet<Cell>()
            {
                new Cell(-1, 0),
                new Cell(0, 0),
                new Cell(1, 0)
            };

            Life game = new Life(SetupSimpleOscillator());

            game.UpdateCells();
            Assert.AreEqual(horizontalOsc, game.Cells);

            game.UpdateCells();
            Assert.AreEqual(verticalOsc, game.Cells);

        }

    }
}