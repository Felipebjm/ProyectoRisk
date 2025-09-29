using LogicLayer.LinkedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    internal class MazoCartas
    {
        // Lista principal de cartas disponibles en el mazo
        public ImpLinkedList<Carta> Mazo_deCartas { get; private set; }

        private Random rng; // Generador aleatorio para barajar o seleccionar cartas

        public MazoCartas() // Constructor que crea la lista vacia y el generador aleatorio
        {
            Mazo_deCartas = new ImpLinkedList<Carta>();
            rng = new Random();
        }
        public void CrearCartas(ImpLinkedList<Territorio> territorios)
        {
            int id = 53; // IDs del 53 al 94 (42 cartas)

            foreach (var territorio in territorios)
            {
                if (id > 94) break;

                // Alterna Infanteria, Caballeria, Artilleria
                TipoCarta tipo = (TipoCarta)((id - 53) % 3);

                // Crea la carta asociada al territorio
                var carta = new Carta(id, tipo, territorio);

                Mazo_deCartas.Agregar(carta);
                id++;
            }
        }


    }
}
