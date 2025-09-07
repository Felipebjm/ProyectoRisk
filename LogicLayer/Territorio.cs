using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public class Territorio
    {
        public int Id { get; set; } // Id del territorio
        public string Nombre { get; set; }
        public Jugador? Dueno_territorio { get; set; } //Referencia al jugador que lo controla, si no tiene dueno es null
        public int Tropas { get; set; } //Cantidad de tropas en el territorio
        public Territorio[] Adyacentes { get; set; } // Array de los territorios adyacentes
        public Territorio[] Rutas_maritimas { get; set; } // Territorios con los que se une por ruta maritima
        public Continente Continente { get; set; } // El continente al que pertenece, refencia al objeto continente
        public int Cantidad_ady { get; set; } //Cantidad de terriorios adyacentes para poder crear el array
        public int Cantidad_rutasMaritimas { get; set; } //Cantidad de rutas maritimas para poder crear el array

        // Metodo constructor
        public Territorio(int id, string nombre, Continente continente, int cantidadAdyacentes,int rutasMaritimas)
        {
            Id = id;
            Nombre = nombre;
            Dueno_territorio = null;
            Cantidad_ady = cantidadAdyacentes; //Para crear el array
            Cantidad_rutasMaritimas = rutasMaritimas; 
            Tropas = 0;
            Adyacentes = new Territorio[cantidadAdyacentes]; // Sirve para crear el array con la cantidad de adyacentes
            Continente = continente;
        }
    }
}
