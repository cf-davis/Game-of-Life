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

        public DisplayWindow()
        {
            InitializeComponent();

            controller = new Controller();

            // --- register event handlers from controller here --

            controller.GameUpdate += Controller_GameUpdate;
        }

        private void Controller_GameUpdate()
        {
            // this should notify the GraphicsPanel to redraw
            Start_Button.Text = "updating...";
        }

        private void Start_Button_Click(object sender, EventArgs e)
        {
            // temp hashset-- should contain user input
            theGame = controller.StartSimulation(new HashSet<Cell>());
            Start_Button.Enabled = false;
        }
    }
}
