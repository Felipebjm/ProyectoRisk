using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public enum TipoCarta // Tipo de carta
    {
        Infanteria, //0
        Caballeria,//1
        Artilleria //2
    }
    public class Carta
    {
        public int Id { get; set; } // Identificador  de la carta 

        public TipoCarta Tipo { get; set; } // Tipo de carta Infanteria, Caballeria o Artilleria

        // Indica si la carta ya fue canjeada
        public bool Usada { get; set; }

        // Jugador que actualmente posee la carta (puede ser null si está en el mazo)
        public Jugador? Propietario { get; set; }
        public Territorio? TerritorioAsociado { get; set; } // Territorio asociado a la carta null si no tiene:v

        public Carta(int id, TipoCarta tipo, Territorio? territorioAso = null) // Constructor
        {
            Id = id;
            Tipo = tipo;
            Usada = false;       // siempre empieza como no usada
            Propietario = null;  // al inicio no tiene dueño
            TerritorioAsociado = territorioAso;
        }

        public void AsignarDuenoCarta(Jugador jugador)
        {
            Propietario = jugador;
            jugador.AgregarCarta(this); // sincroniza con la lista del jugador
        }
        public void LiberarDuenoCarta()
        {
            Propietario = null;
        }

    }
}
