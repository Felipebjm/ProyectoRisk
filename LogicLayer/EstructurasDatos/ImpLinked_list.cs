using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    using System;
    using System.Collections;

    namespace LinkedList
    {
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

        public class ImpLinkedList<T> : IEnumerable<T>
        {
            private Nodo<T> cabeza;
            private int _count; // contador O(1)

            public ImpLinkedList()
            {
                cabeza = null;
                _count = 0;
            }

            // Propiedad Count O(1)
            public int Count => _count;

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

                _count++;
            }

            // Eliminar un elemento (solo primera ocurrencia)
            public void Eliminar(T valor)
            {
                if (cabeza == null) return;

                if (cabeza.Valor.Equals(valor))
                {
                    cabeza = cabeza.Siguiente;
                    _count--;
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
                    _count--;
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

            // Imprimir (útil para debugging)
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

            // Método de conteo legacy (opcional). Ahora Count es O(1)
            public int Contar() => Count;

            // Implementación de IEnumerable<T> para soportar foreach y LINQ
            public IEnumerator<T> GetEnumerator()
            {
                Nodo<T> actual = cabeza;
                while (actual != null)
                {
                    yield return actual.Valor;
                    actual = actual.Siguiente;
                }
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }

}
