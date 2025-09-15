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
        public int Tropas_territorio { get; set; } //Cantidad de tropas en el territorio
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
            Tropas_territorio = 0;
            Adyacentes = new Territorio[cantidadAdyacentes]; // Sirve para crear el array con la cantidad de adyacentes
            Continente = continente;
        }

        public void Editar_Territorio_Adyacente(int indice, Territorio? nuevoAdyacente) //Para agrarlo solo se pone el indice
        {
            // Validar índice
            if (indice < 0 || indice >= Adyacentes.Length) //Para eliminarlo se pone null en nuevoAdyacente
                throw new ArgumentOutOfRangeException(nameof(indice),
                    $"El índice debe estar entre 0 y {Adyacentes.Length - 1}");
            Adyacentes[indice] = nuevoAdyacente; // Asignar nuevo territorio adyacente o null para eliminar
        }
        public bool ConfirmarAdyacencia_Terr(Territorio territorio) //Confirma si un territorio es adyacente
        {
            if (territorio == null)
                throw new ArgumentNullException(nameof(territorio));

            for (int i = 0; i < Adyacentes.Length; i++)
            {
                if (Adyacentes[i] == territorio) // Si encuentra el territorio adyacente retorna true
                    return true;
            }

            return false;
        }


        public void Restar_Tropas_Terr(int cantidad)
        {
            if (cantidad < 0) // Revisa que la cantidad no sea negativa
                throw new ArgumentOutOfRangeException(nameof(cantidad), "La cantidad de tropas a restar debe ser mayor o igual a cero.");

            if (cantidad > Tropas_territorio) // Revisa que no se reste más tropas de las que hay
                throw new InvalidOperationException( $"No se pueden quitar {cantidad} tropas; solo hay {Tropas_territorio} en '{Nombre}'.");

            Tropas_territorio -= cantidad; // Restar tropas
        }

        public void Agregar_Tropas_Terr(int cantidad) //Anade tropas al territorio
        {
            if (cantidad < 0)
                throw new ArgumentOutOfRangeException(nameof(cantidad));
            Tropas_territorio += cantidad;
        }

        public bool Verificar_Tropas_Terr()
        {
            if (Tropas_territorio >= 1) // Retorna true si hay 1 o mas tropas
            {
                return true; //Puede servir para verificar si un territorio tiene tropas antes de hacer algo 
            }
            else
            {
                return false; 
            }
        }
        public void AsignarDueno_Terr(Jugador? nuevoDueno)
        {
            if (Dueno_territorio == nuevoDueno) // Si el dueno actual es el mismo que el nuevo no hace nada
                return;

            if (Dueno_territorio != null) // Si hay un dueno actual quita este territorio de su lista

            {
                Dueno_territorio.PerderTerritorio(this);
            }

            // Asigna el nuevo dueno
            Dueno_territorio = nuevoDueno; //Para eliminar nada mas se pone null

            if (nuevoDueno != null)  // Si hay nuevo dueno anadir este territorio a su lista
            {
                nuevoDueno.Anadir_Territorio_Conquistado(this);
            }


        }




    }   
        
}
