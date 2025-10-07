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
        public System.Drawing.Color Color { get; set; }

        public ImpLinkedList<Territorio> Territorios { get; set; } // Territorios controlados por el jugador
        public int TropasDisponibles { get; set; } 
        public ImpLinkedList<Carta> Cartas { get; set; } // Cartas que tiene del jugador   
        public ImpLinkedList<Continente> ContinentesControlados { get; set; } //Los continentes que tiene el jugador
        public bool Turno { get; set; } //Indica si es el turno del jugador
        public int TerritoriosConquistados { get; set; }
        public int TropasTotales { get; set; }
   
        public bool Cambiar_cartas { get; set; } //Indica si el jugador esta obligado a cambiar cartas

        //Conexion
        //public EstadoConexion EstadoConexion { get; set; } //Estado de conexion del jugador


        // Metodo constructor
        public Jugador(int id, string nombre, System.Drawing.Color color) //No estan los territorios pq cuando se crea el jugador no tiene pero se puede poner para probar
        {
            Id = id;
            Nombre = nombre;
            Color = color;

            // Inicializar colecciones vacías
            Territorios = new ImpLinkedList<Territorio>();
            Cartas = new ImpLinkedList<Carta>(); //### Hay que crear la clase cartas
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
        public void EscogerNombre(string nuevoNombre)
        {
            if (!string.IsNullOrWhiteSpace(nuevoNombre)) //Esto es para evitar que el nombre tengo espacios en blanco o este vacio
            {
                Nombre = nuevoNombre.Trim();
            }
            else
            {
                throw new ArgumentException("El nombre no puede estar vacío.");
            }
        }
        public void Anadir_Territorio_Conquistado(Territorio territorio)
        {
            if (territorio == null)
                throw new ArgumentNullException(nameof(territorio));

            if (!Territorios.Buscar(territorio)) //Busca el territorio para evitar que este 2 veces
            {
                Territorios.Agregar(territorio); // Anadir a la lista enlazada

                //territorio.Dueno_territorio = this; // Asignar propietario

                // Para actualizar contadores
                TerritoriosConquistados++;
                TropasTotales += territorio.Tropas_territorio;
            }
        }
        public void PerderTerritorio(Territorio territorio)
        {
            if (territorio == null)
                throw new ArgumentNullException(nameof(territorio));
            

            if (Territorios.Buscar(territorio)) // Verificar que el jugador si controla este territorio
            {
                // Quitar de la lista enlazada
                Territorios.Eliminar(territorio);

                //territorio.Dueno_territorio = null;  // Quitar la referencia de dueño


               TropasTotales = TropasTotales - territorio.Tropas_territorio; // Ajustar estadssticas de las tropas

            }
        }
        public int TerritoriosConq() //Cuenta los territorios  
        {
            return Territorios.Contar();  //Llama al metodo contar de la lista enlazada
        }

        public void Modificar_Tropas(int cantidad) // Metodo para modificar las tropas totales
        {
    
            int nuevoTotal = TropasTotales + cantidad; //Si el jugador pierde tropas la cantidad de entrada es negativa 

            if (nuevoTotal < 0)
            {
                throw new InvalidOperationException("El numero de tropas no puede ser negativo");
            }

            TropasTotales = nuevoTotal;
        }

        public void Anadir_Continente(Continente continente) //Anade un continente a la lista de continentes controlados
        {
            if (continente == null)
                throw new ArgumentNullException(nameof(continente));

            // Evitar duplicados
            if (!ContinentesControlados.Buscar(continente)) //Revisa que no este ya en la lista
            {
                ContinentesControlados.Agregar(continente);
            }
        }
        public void Perder_Continente(Continente continente) //Elimina un continente de la lista de continentes controlados
        {
            if (continente == null)
                throw new ArgumentNullException(nameof(continente));
            if (ContinentesControlados.Buscar(continente)) //Revisa que el jugador si controle ese continente
            {
                ContinentesControlados.Eliminar(continente);
            }
        }
        public void ObtenerBonusContinente(List<Continente> continentes)
        {
            foreach (var continente in continentes)
            {
                if (continente.ControlTotal(this))
                {
                    TropasDisponibles += continente.BonusRefuerzo;
                }
            }
        }
        public void AgregarCarta(Carta carta)
        {
            Cartas.Agregar(carta);
            carta.AsignarDuenoCarta(this);
        }

        // Método simple para eliminar una carta
        public void EliminarCarta(Carta carta)
        {
            Cartas.Eliminar(carta);
            carta.LiberarDuenoCarta();
        }



    }
}
