using LogicLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InterfazGrafica
{
    public partial class PantallaJuego : Form
    {
        public Gameplay juego;
        public Dictionary<int, Point> posiciones;
        public Random rnd = new Random();

        public PantallaJuego(string nombreJugador1, string nombreJugador2, string nombreJugador3)
        {
            InitializeComponent();
           
        }
        
        private void btnEjecutar_Click(object sender, EventArgs e)
        {

        }
    }
}
