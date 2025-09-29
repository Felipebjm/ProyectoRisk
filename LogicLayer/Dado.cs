using LogicLayer.LinkedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public class Dado
    {
        public ImpLinkedList<int> Valores { get; }

        public Dado(int cantidad, Random random) 
        {
            Valores = new ImpLinkedList<int>();
            for (int i = 0; i < cantidad; i++)
            {
                int numero = random.Next(1, 7); // 1 a 6 inclusive
                Valores.Agregar(numero);
            }
        }

        public void Imprimir()
        {
            Valores.Imprimir();
        }
    }
}

