using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public class Continente
    {
        public int Id { get; set; } // Id del continente
        public string Nombre { get; set; }
        public Territorio[] Territorios { get; set; } // Array de territorios del continente
        public int BonusRefuerzo { get; set; }
        public Jugador? Dueno_Continente { get; set; } //Referencia al jugador que lo controla, si no tiene dueno es null

        // Metodo constructor
        public Continente(int id, string nombre, int bonusRefuerzo, int cantidadTerritorios)
        {
            Id = id;
            Nombre = nombre;
            BonusRefuerzo = bonusRefuerzo;
            Territorios = new Territorio[cantidadTerritorios]; // 
            Dueno_Continente = null;
        }

    }
}
