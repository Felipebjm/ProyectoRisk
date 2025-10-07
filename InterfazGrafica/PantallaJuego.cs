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
        //El juego, las coordenadas de los territorios y dados
        public Gameplay juego;
        public Dictionary<int, Point> posiciones;
        public Random rnd = new Random();
        public Cola<Jugador> colaDistribucion; // Cola para la distribución de territorios
        public bool distribucionActiva = false; // Variable para controlar si la distribución está activa

        // NUEVO: guardamos los botones creados para actualizar directamente
        private Dictionary<int, Button> botonesTerritorio = new Dictionary<int, Button>();

        //Metodo constructor que recibe los 3 nombres de los jugadores e incializa el juego
        public PantallaJuego(string nombreJugador1, string nombreJugador2, string nombreJugador3)
        {
            InitializeComponent();
            InicializarJuego(nombreJugador1, nombreJugador2, nombreJugador3);
        }



        //Metodo que monta el modelo de juego y la UI
        public void InicializarJuego(string nombreJugador1, string nombreJugador2, string nombreJugador3)
        {
            juego = new Gameplay(); //Inicializa el juego y asigna los nombres
            juego.Jugador1.Nombre = nombreJugador1;
            juego.Jugador2.Nombre = nombreJugador2;
            juego.Jugador3.Nombre = nombreJugador3;
            juego.IniciarJuego(); //Inicia el juego

            foreach (var t in juego.Territorios) //Asigna 1 tropa a cada territorio
            {
                t.Agregar_Tropas_Terr(1);
                t.Dueno_territorio.TropasDisponibles -= 1; //Resta las tropas disponibles del jugador
            }

            // Inicializar la cola para la distribucion de territorios
            colaDistribucion = new Cola<Jugador>(3);
            colaDistribucion.Enqueue(juego.Jugador1);
            colaDistribucion.Enqueue(juego.Jugador2);
            colaDistribucion.Enqueue(juego.Jugador3);

            // MARCA IMPORTANTE: activamos la fase de distribución aquí
            distribucionActiva = true;

            // Controles durante la distribución:
            // Queremos que el jugador seleccione el territorio destino y la cantidad.
            txtIdOrigen.Enabled = false;            // no usamos origen durante la colocación inicial
            txtIdDestino.Enabled = true;            // el click en un botón llenará el destino
            txtCantidad.Enabled = true;
            btnEjecutar.Enabled = true;

            // Deshabilitar controles que no se usan hasta que termine la distribución
            txtDadosA.Enabled = false;
            txtDadosD.Enabled = false;

            var jugadorInicial = colaDistribucion.Peek();
            txtAnuncios.Text = $"Colocación inicial: {jugadorInicial.Nombre}, distribuye tus tropas. Te quedan {jugadorInicial.TropasDisponibles} tropas.\r\n";

            //Define las posiciones de los territorios en el panel(los botones)
            posiciones = new Dictionary<int, Point>
        {
            {1,new Point(106,80) },
            {2,new Point(150,143) },
            {3,new Point(93,199) },
            {4,new Point(78,252) },
            {5,new Point(173,214) },
            {6,new Point(125,270) },
            {7,new Point(333,23) },
            {8,new Point(397,140) },
            {9,new Point(450,116) },
            {10,new Point(485,89) },
            {11,new Point(652,63) },
            {12,new Point(457,47) },
            {13,new Point(596,116) },
            {14,new Point(589,147) },
            {15,new Point(549,165) },
            {16,new Point(560,208) },
            {17,new Point(643,188) },
            {18,new Point(706,125) },
            {19,new Point(734,221) },
            {20,new Point(802,153) },
            {21,new Point(418,187) },
            {22,new Point(397,221) },
            {23,new Point(495,227) },
            {24,new Point(473,275) },
            {25,new Point(485,342) },
            {26,new Point(743,317) },
            {27,new Point(849,360) },
            {28,new Point(959,331) },
            {29,new Point(818,282) },
            {30,new Point(825,251) },
            {31,new Point(206,277)},
            {32,new Point(244,315) },
            {33,new Point(164,350) },
            {34,new Point(218,405) },
            {35,new Point(218,405) }
        };

            //Generar un boton por territorio 
            GenerarBotonesTerritorio();

            //Actualizar la UI con los datos del juego
            RefrescarUI();
        }

        public void AvanzarDistribucion(int idDestino, int cantidad)
        {
            var jugador = colaDistribucion.Peek(); //Jugador actual

            var destino = jugador.Territorios.FirstOrDefault(t => t.Id == idDestino); //Busca el territorio en los territorios del jugador
            if (destino == null)
            {
                MessageBox.Show("El territorio seleccionado no te pertenece. Elige otro.");
                return;
            }

            if (cantidad < 1 || cantidad > jugador.TropasDisponibles)
            {
                MessageBox.Show("No tienes suficientes tropas");
                return;
            }

            //Colocar las tropas
            destino.Agregar_Tropas_Terr(cantidad);
            jugador.TropasDisponibles -= cantidad;

            //Actualizar la UI
            RefrescarUI();

            //Verificar si el jugador actual ya no tiene tropas disponibles
            if (jugador.TropasDisponibles == 0)
            {
                colaDistribucion.Dequeue(); //Pasa al siguiente jugador
                if (colaDistribucion.Count == 0)
                {
                    // Termina la distribución: habilitamos el flujo normal
                    distribucionActiva = false;
                    txtAnuncios.Text += "Colocación inicial completada. Comienza el juego.\r\n\r\n";

                    // Habilitar controles normales
                    txtIdOrigen.Enabled = true;
                    txtIdDestino.Enabled = true;
                    txtCantidad.Enabled = true;
                    txtDadosA.Enabled = true;
                    txtDadosD.Enabled = true;

                    // (Opcional) establecer la fase/turno inicial si el modelo lo requiere:
                    // Por ejemplo, si quieres iniciar en Planeacion explícitamente:
                    // juego.TurnoActual.FaseActual = FaseTurno.Planeacion;

                    RefrescarUI();
                    return;
                }
            }

            // Si aún quedan jugadores por distribuir, mostrar el jugador actual y sus tropas
            if (distribucionActiva && colaDistribucion.Count > 0)
            {
                var actual = colaDistribucion.Peek();
                txtAnuncios.Text = $"{actual.Nombre}: te quedan {actual.TropasDisponibles} tropas.\r\n";
            }
        }

        //Metodo que genera los botones de los territorios en el panel
        public void GenerarBotonesTerritorio()
        {
            // BORRAR botones previos (si se vuelve a inicializar) — evita duplicados
            foreach (var b in botonesTerritorio.Values)
                this.Controls.Remove(b);
            botonesTerritorio.Clear();

            foreach (var t in juego.Territorios) //Recorre todos los territorios del juego
            {
                var btn = new Button //Crea un boton por cada territorio
                {
                    Tag = t.Id, //El id del territorio en el Tag
                    Name = "btnTerr" + t.Id,
                    Size = new Size(60, 30),
                    Location = posiciones[t.Id], //La posicion del territorio en el panel
                    FlatStyle = FlatStyle.Flat,
                    Text = $"{t.Id}:{t.Tropas_territorio}", // INICIALIZAR texto
                    BackColor = t.Dueno_territorio.Color
                };
                btn.Click += BotonTerritorio_Click;
                this.Controls.Add(btn); //Agrega el boton al panel

                // Guardamos referencia para actualizaciones rápidas
                botonesTerritorio[t.Id] = btn;
            }
        }


        //Actualiza el texto, color de los botones y los controles 
        public void RefrescarUI()
        {
            //Muestra las tropas disponibles de cada jugador
            txtTropasJ1.Text = juego.Jugador1.TropasDisponibles.ToString();
            txtTropasJ2.Text = juego.Jugador2.TropasDisponibles.ToString();
            txtTropasJ3.Text = juego.Jugador3.TropasDisponibles.ToString();

            bool normal = !distribucionActiva;
            txtIdOrigen.Enabled = normal;
            txtIdDestino.Enabled = normal || distribucionActiva; // destino activo durante distribucion
            txtCantidad.Enabled = normal || distribucionActiva;   // si no es refuerzo quizá quieras desactivar
            txtDadosA.Enabled = normal;
            txtDadosD.Enabled = normal;

            txtTurno.Text = juego.TurnoActual.Jugador.Nombre; //Muestra el jugador en turno

            switch (juego.TurnoActual.FaseActual) //Actualiza el texto de la fase segun sea el caso
            {
                case FaseTurno.Planeacion:
                    txtFase.Text = "Planeacion";
                    break;
                case FaseTurno.Ataque:
                    txtFase.Text = "Ataque";
                    break;
                case FaseTurno.Refuerzo:
                    txtFase.Text = "Refuerzo";
                    break;
            }

            // Refrescar únicamente los botones de territorios creados (más robusto)
            foreach (var kv in botonesTerritorio)
            {
                int id = kv.Key;
                var btn = kv.Value;
                var territorio = juego.Territorios.First(t => t.Id == id); //Busca el territorio por su id
                btn.Text = $"{territorio.Id}:{territorio.Tropas_territorio}";
                btn.BackColor = territorio.Dueno_territorio.Color;
                btn.Refresh(); // fuerza repintado (opcional)
            }
        }

        public void BotonTerritorio_Click(object sender, EventArgs e)
        {
            // 1) Sender debe ser Button
            if (!(sender is Button btn)) return;

            // 2) Tag debe ser un int
            if (!(btn.Tag is int id)) return;

            // A partir de aquí id es seguro
            if (distribucionActiva)
            {
                // Durante la distribución el click selecciona el destino
                txtIdDestino.Text = id.ToString();
            }
            else
            {
                // Flujo normal: llenar origin y destination para facilitar operaciones
                txtIdOrigen.Text = id.ToString();
                txtIdDestino.Text = id.ToString();
            }
        }


        //Logica del boton ejecutar
        public void btnEjecutar_Click(object sender, EventArgs e)
        {
            // 1) FASE DE DISTRIBUCIÓN INICIAL
            if (distribucionActiva)
            {
                // Leemos destino y cantidad desde los TextBox (NOTA: durante distribucion usamos txtIdDestino)
                int idDest = int.TryParse(txtIdDestino.Text, out var d) ? d : 0;
                int cantidad = int.TryParse(txtCantidad.Text, out var c) ? c : 0;

                AvanzarDistribucion(idDest, cantidad);

                // Limpiar inputs
                txtIdDestino.Clear();
                txtCantidad.Clear();
                return;
            }

            // 2) FLUJO NORMAL DE JUEGO
            int idO = int.TryParse(txtIdOrigen.Text, out var o) ? o : 0;
            int idD = int.TryParse(txtIdDestino.Text, out var dd) ? dd : 0;
            int qty = int.TryParse(txtCantidad.Text, out var q) ? q : 0;
            int atq = int.TryParse(txtDadosA.Text, out var a) ? a : 0;
            int def = int.TryParse(txtDadosD.Text, out var df) ? df : 0;

            var origen = juego.Territorios.FirstOrDefault(t => t.Id == idO);
            var destino = juego.Territorios.FirstOrDefault(t => t.Id == idD);
            if (origen == null || destino == null)
            {
                MessageBox.Show("Selecciona un territorio válido.");
                return;
            }

            switch (juego.TurnoActual.FaseActual)
            {
                case FaseTurno.Planeacion:
                    juego.TurnoActual.FasePlaneacion(origen, destino, qty);
                    juego.TurnoActual.SiguienteFaseAutomatica();
                    txtAnuncios.Text = $"{juego.TurnoActual.Jugador.Nombre} movió {qty} tropas.";
                    break;

                case FaseTurno.Ataque:
                    {
                        if (origen.Dueno_territorio == destino.Dueno_territorio)
                        {
                            MessageBox.Show("No puedes atacar tus propios territorios.");
                            return;
                        }

                        if (origen.Tropas_territorio <= 1)
                        {
                            MessageBox.Show("Necesitas al menos 2 tropas para atacar.");
                            return;
                        }

                        // Determinar cantidad de dados según las tropas y los cuadros de texto
                        int dadosAtacante = Math.Min(3, Math.Min(atq > 0 ? atq : 3, origen.Tropas_territorio - 1));
                        int dadosDefensor = Math.Min(2, def > 0 ? def : 2);

                        // Simular tiradas
                        var tiradaAtacante = Enumerable.Range(0, dadosAtacante).Select(_ => rnd.Next(1, 7)).OrderByDescending(v => v).ToList();
                        var tiradaDefensor = Enumerable.Range(0, dadosDefensor).Select(_ => rnd.Next(1, 7)).OrderByDescending(v => v).ToList();

                        txtAnuncios.Text += $"Atacante tira: {string.Join(", ", tiradaAtacante)}\r\n";
                        txtAnuncios.Text += $"Defensor tira: {string.Join(", ", tiradaDefensor)}\r\n";

                        // Resolver batalla
                        int comparaciones = Math.Min(dadosAtacante, dadosDefensor);
                        for (int i = 0; i < comparaciones; i++)
                        {
                            if (tiradaAtacante[i] > tiradaDefensor[i])
                                destino.Tropas_territorio--;
                            else
                                origen.Tropas_territorio--;
                        }

                        // Si el defensor quedó sin tropas → conquista
                        if (destino.Tropas_territorio <= 0)
                        {
                            destino.AsignarDueno_Terr(origen.Dueno_territorio);
                            destino.Tropas_territorio = Math.Max(1, qty > 0 ? qty : 1); // mover 1 tropa
                            origen.Tropas_territorio -= destino.Tropas_territorio;
                            txtAnuncios.Text += $"¡{origen.Dueno_territorio.Nombre} conquista el territorio {destino.Id}!\r\n";
                        }

                        RefrescarUI();

                        juego.TurnoActual.SiguienteFase();
                        // Si después de avanzar la fase es Refuerzo, cambiar turno
                        if (juego.TurnoActual.FaseActual == FaseTurno.Refuerzo)
                        {
                            juego.CambiarTurno();
                            txtAnuncios.Text += $" Turno de {juego.TurnoActual.Jugador.Nombre}.";
                        }

                        break;
                    }


                case FaseTurno.Refuerzo:
                    juego.TurnoActual.FaseRefuerzo(juego.Continentes);
                    juego.CambiarTurno();
                    txtAnuncios.Text = $"Refuerzo automático calculado. Turno de {juego.TurnoActual.Jugador.Nombre}.";
                    break;
            }

            RefrescarUI();

            // Comprobar victoria
            var jugador = juego.TurnoActual.Jugador;
            if (jugador.TerritoriosConq() == juego.Territorios.Count)
            {
                txtAnuncios.Text = $"{jugador.Nombre} ha ganado la partida!";
                btnEjecutar.Enabled = false;
            }
        }

    }

}
