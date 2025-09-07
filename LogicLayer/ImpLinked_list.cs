using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    using System;

    namespace LinkedList
    {
        // Clase Nodo genérico
        public class Nodo<T>
        {
            public T Valor { get; set; }
            public Nodo<T> Siguiente { get; set; }

            public Nodo(T valor)
            {
                Valor = valor;
                Siguiente = null;
            }
        }

        // Clase Lista Enlazada genérica
        public class ImpLinkedList<T>
        {
            private Nodo<T> cabeza;

            public ImpLinkedList()
            {
                cabeza = null;
            }
           
            // Añadir elemento al final
            public void Agregar(T valor)
            {
                Nodo<T> nuevo = new Nodo<T>(valor);

                if (cabeza == null)
                {
                    cabeza = nuevo;
                }
                else
                {
                    Nodo<T> actual = cabeza;
                    while (actual.Siguiente != null)
                    {
                        actual = actual.Siguiente;
                    }
                    actual.Siguiente = nuevo;
                }
            }

            // Eliminar un elemento
            public void Eliminar(T valor)
            {
                if (cabeza == null) return;

                if (cabeza.Valor.Equals(valor))
                {
                    cabeza = cabeza.Siguiente;
                    return;
                }

                Nodo<T> actual = cabeza;
                while (actual.Siguiente != null && !actual.Siguiente.Valor.Equals(valor))
                {
                    actual = actual.Siguiente;
                }

                if (actual.Siguiente != null)
                {
                    actual.Siguiente = actual.Siguiente.Siguiente;
                }
            }

            // Buscar un elemento
            public bool Buscar(T valor)
            {
                Nodo<T> actual = cabeza;
                while (actual != null)
                {
                    if (actual.Valor.Equals(valor))
                        return true;

                    actual = actual.Siguiente;
                }
                return false;
            }

            // Mostrar la lista
            public void Imprimir()
            {
                Nodo<T> actual = cabeza;
                while (actual != null)
                {
                    Console.Write(actual.Valor + "  ");
                    actual = actual.Siguiente;
                }
                Console.WriteLine("null");
            }
        }
    }

}
