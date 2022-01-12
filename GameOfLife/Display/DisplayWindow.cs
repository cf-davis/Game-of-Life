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

namespace Display
{
    public partial class DisplayWindow : Form
    {

        // holds the Controller used to communicate user input to the model
        private Controller controller;

        // pseudo-readonly refernce to the model used for graphics updating
        private Life theGame;

        // used to draw the game to the form
        private GraphicsPanel gPanel;

        public DisplayWindow()
        {
            InitializeComponent();

            controller = new Controller();

            gPanel = new GraphicsPanel(theGame);
            gPanel.Location = new Point(0, 0);
            gPanel.Size = Size;

            Controls.Add(gPanel);
            gPanel.Hide();

            // --- register event handlers from controller here --

            controller.GameUpdate += Controller_GameUpdate;
        }

        private void Controller_GameUpdate()
        {
            // this should notify the GraphicsPanel to redraw
            gPanel.Invalidate();
        }

        private void Start_Button_Click(object sender, EventArgs e)
        {
            // *temporary input cells (will freeze gui until new thread is made)*
            //HashSet<Cell> cells = new HashSet<Cell>
            //{
            //    new Cell(0, -1),
            //    new Cell(0, 0),
            //    new Cell(0, 1)
            //};

            // temp hashset-- should contain user input
            theGame = controller.StartSimulation(new HashSet<Cell>());
            Start_Button.Enabled = false;
            Start_Button.Hide();

            gPanel.Show();
        }
    }
}
