namespace InterfazGrafica
{
    public partial class PantallaInicio : Form
    {
        public PantallaInicio()
        {
            InitializeComponent();
        }

        

        private void btnIniciar_Click(object sender, EventArgs e)
        {
            //Leer y eliminar los espacios en los nombres
            string n1 = txtJugador1.Text.Trim();
            string n2 = txtJugador2.Text.Trim();
            string n3 = txtJugador3.Text.Trim();

            //Asignar nombres default si estan vacios
            if (string.IsNullOrEmpty(n1))
            {
                n1 = "Jugador 1";
            }
            if (string.IsNullOrEmpty(n2))
            {
                n2 = "Jugador 2";
            }
            if (string.IsNullOrEmpty(n3))
            {
                n3 = "Jugador 3";
            }
            new PantallaJuego(n1, n2, n3).Show(); //Mostrar la pantalla del juego
            this.Hide(); //Para ocultar la pantalla de inicio


        }
    }
}
