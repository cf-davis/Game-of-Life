using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GameModel;

namespace Display
{

    class GraphicsPanel : Panel
    {

        // this should handle drawing everthing to the Form, see example from ps8

        private Life theGame;

        public GraphicsPanel(Life _theGame)
        {
            DoubleBuffered = true;
            theGame = _theGame;
            BackColor = Color.Black;


        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
        }

    }
}
