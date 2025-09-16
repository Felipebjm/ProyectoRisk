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
    public class Cartas
    {
        public int Id { get; set; } // Identificador  de la carta 

        public TipoCarta Tipo { get; set; } // Tipo de carta Infanteria, Caballeria o Artilleria

        // Indica si la carta ya fue canjeada
        public bool Usada { get; set; }

        // Jugador que actualmente posee la carta (puede ser null si está en el mazo)
        public Jugador? Propietario { get; set; }

    }
}
