using LogicLayer.LinkedList;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public class Jugador
    {
        // Identidad
        public int Id { get; set; }  // Identificador único del jugador
        public string Nombre { get; set; } 
        public ConsoleColor Color { get; set; }

        public ImpLinkedList<Territorio> Territorios { get; set; } // Territorios controlados por el jugador
        public int TropasDisponibles { get; set; } 
        public List<Cartas> Cartas { get; set; } // Cartas que tiene del jugador    //### Hay que crear la clase cartas
        public ImpLinkedList<Continente> ContinentesControlados { get; set; } //Los continentes que tiene el jugador
        public bool Turno { get; set; } //Indica si es el turno del jugador
        public int TerritoriosConquistados { get; set; }
        public int TropasTotales { get; set; }
   
        public bool Cambiar_cartas { get; set; } //Indica si el jugador esta obligado a cambiar cartas

        //Conexion
        //public EstadoConexion EstadoConexion { get; set; } //Estado de conexion del jugador


        // Metodo constructor
        public Jugador(int id, string nombre, ConsoleColor color) //No estan los territorios pq cuando se crea el jugador no tiene pero se puede poner para probar
        {
            Id = id;
            Nombre = nombre;
            Color = color;

            // Inicializar colecciones vacías
            Territorios = new ImpLinkedList<Territorio>();
            Cartas = new List<Cartas>(); //### Hay que crear la clase cartas
            ContinentesControlados = new ImpLinkedList<Continente>();

            // Valores iniciales
            TropasDisponibles = 0;
            Turno = false;
            TerritoriosConquistados = 0;
            TropasTotales = 0;
            Cambiar_cartas = false;

            // Estado de conexión inicial
            //EstadoConexion = EstadoConexion.Conectado;

        }

        //Metodos
    }
}
