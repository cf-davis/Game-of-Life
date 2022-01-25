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

        private bool mousePressed = false;

        public DisplayWindow(Controller _controller)
        {
            InitializeComponent();

            controller = _controller;

            gPanel = new GraphicsPanel(controller.TheGame)
            {
                Location = new Point(0, 0),
                Size = new Size(ClientRectangle.Width, ClientRectangle.Height),
                Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | 
                    AnchorStyles.Right)
            };

            Controls.Add(gPanel);
            gPanel.Focus();
            //gPanel.Hide();

            // --- register event handlers from controller here --

            FormClosing += CloseGameWindow;
            KeyDown += GameWidow_KeyDown;

            gPanel.MouseDown += GraphicsPanel_OnMouseDown;
            gPanel.MouseUp += GPanel_MouseUp;
            gPanel.MouseMove += GPanel_MouseMove;

            controller.GameUpdate += Controller_GameUpdate;
        }

        private void GPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (!controller.TheGame.IsRunning && mousePressed)
            {
                bool addCell = e.Button == MouseButtons.Left;
                controller.UpdateInitialCells(e.X, e.Y, gPanel.CellSize, gPanel.Size, addCell);
            }

        }

        private void GraphicsPanel_OnMouseDown(object sender, MouseEventArgs e)
        {
            mousePressed = true;
            GPanel_MouseMove(sender, e); // capture single clicks as well as mouse holds
        }

        private void GPanel_MouseUp(object sender, MouseEventArgs e)
        {
            mousePressed = false;
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
            controller.StartSimulation();

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

    }
}
