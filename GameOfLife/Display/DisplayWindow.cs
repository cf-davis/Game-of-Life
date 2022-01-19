using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GameController;
using GameModel;
using static Display.GraphicsPanel;

namespace Display
{
    public partial class DisplayWindow : Form
    {

        // holds the Controller used to communicate user input to the model
        private Controller controller;

        // used to draw the game to the form
        private GraphicsPanel gPanel;

        public DisplayWindow(Controller _controller)
        {
            InitializeComponent();

            controller = _controller;

            gPanel = new GraphicsPanel(controller.TheGame)
            {
                Location = new Point(0, 0),
                Size = new Size(ClientRectangle.Width, ClientRectangle.Height)
            };

            Controls.Add(gPanel);
            //gPanel.Hide();

            // --- register event handlers from controller here --

            FormClosing += CloseGameWindow;
            KeyDown += GameWidow_KeyDown;

            controller.GameUpdate += Controller_GameUpdate;
        }


        private void CloseGameWindow(object sender, FormClosingEventArgs e)
        {
            Application.Exit();

            // stop any background threads
            Environment.Exit(0);
        }

        private void Controller_GameUpdate()
        {
            // this should notify the GraphicsPanel to redraw
            gPanel.Invalidate();
        }

        private void Start_Button_Click(object sender, EventArgs e)
        {

            HashSet<Cell> cells = new HashSet<Cell>();

            // a 24x16 block of cells
            for (int x = -12; x <= 12; x++)
                for (int y = -8; y <= 8; y++)
                    cells.Add(new Cell(x, y));

            //HashSet<Cell> cells = new HashSet<Cell>
            //{
            //    new Cell(0, 0)
            //};

            //HashSet<Cell> cells = SetupStillLife(1);

            //cells.UnionWith(SetupBlinker(1, 4, 4));
            //cells.UnionWith(SetupToad(1, -4, -7));

            //cells.Add(new Cell(5, -6));
            //cells.Add(new Cell(4, -5));
            //cells.Add(new Cell(5, -4));

            // temp hashset-- should contain user input
            controller.StartSimulation(cells);

            Start_Button.Enabled = false;
            Start_Button.Hide();

            gPanel.Show();
        }

        private void GameWidow_KeyDown(object sender, KeyEventArgs e)
        {

            switch (e.KeyCode)
            {
                // temp controls
                case Keys.Z:
                    gPanel.ZoomOut();
                    break;
                case Keys.X:
                    gPanel.ZoomIn();
                    break;
                case Keys.W:
                    gPanel.Pan(Direction.Up);
                    break;
                case Keys.A:
                    gPanel.Pan(Direction.Left);
                    break;
                case Keys.S:
                    gPanel.Pan(Direction.Down);
                    break;
                case Keys.D:
                    gPanel.Pan(Direction.Right);
                    break;
            }

        }

        //////////////////////////////////////////////////////////////////////
        // DELETE BELOW -- used for some lazy testing
        //////////////////////////////////////////////////////////////////////

        private HashSet<Cell> SetupStillLife(int form)
        {
            HashSet<Cell> cells = new HashSet<Cell>();

            switch (form)
            {
                case 1:
                default:
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

    }
}
