using LogicLayer.LinkedList;
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
        public int BonusRefuerzo { get; set; } //Bonus de refuerzo por tener el continente
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
        public void Anadir_Territorio(Territorio territorio) // Anade un territorio al array de territorios del continente
        {
            if (territorio == null)
                throw new ArgumentNullException(nameof(territorio)); // Verifica que el territorio no sea nulo

            // Buscar primera posición libre
            for (int i = 0; i < Territorios.Length; i++)
            {
                if (Territorios[i] == null) //Si el espacio esta libre mete ahi el territorio
                {
                    Territorios[i] = territorio;
                    return;
                }
            }

            throw new InvalidOperationException($"El array ya no tiene espacio;(((( ");  // Si no encontro espacio lanza excepción   
        }

        public void EliminarTerritorio(Territorio territorio)
        {
            if (territorio == null)
                throw new ArgumentNullException(nameof(territorio));

            // Buscar el territorio en el array
            for (int i = 0; i < Territorios.Length; i++)
            {
                if (Territorios[i] == territorio)
                {
                    Territorios[i] = null;
                    return;
                }
            }
            throw new InvalidOperationException($"El territorio '{territorio.Nombre}' no pertenece al continente '{Nombre}'."); //Si el territorio no esta
        }
        public int CantidadTerritorios() //Cuenta la cantidad de territorios que tiene el continente
        {
            int contador = 0;
            for (int i = 0; i < Territorios.Length; i++)
            {
                if (Territorios[i] != null)
                    contador++;
            }
            return contador;
        }
        public Jugador? GetDueno() //Devuelve el dueno del continente
        {
            return Dueno_Continente;
        }
        public void ActualizarDuenoContinente(Jugador? nuevoDueno) //Actualiza el dueno del continente
        {
            if (Dueno_Continente == nuevoDueno)
                return; // No hay cambio

            Dueno_Continente = nuevoDueno;

        }
        public bool ControlTotal(Jugador jugador)
        {
            return Territorios.All(t => t.Dueno_territorio == jugador);
        }
    }
}
        
