using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.EstructurasDatos
{
    using System;
    using System.Collections.Generic;

    public class Lista<T>
    {
        private T[] _items;                  // almacenamiento interno
        public int Count { get; private set; } // número de elementos

        public Lista() { _items = new T[4]; Count = 0; } // inicia con capacidad 4

        public Lista(IEnumerable<T> origen) : this()    // copia elementos desde colección
        {
            foreach (var item in origen)
                Add(item);                                  // añade cada elemento
        }

        public void Add(T item)                             // agrega al final
        {
            if (Count == _items.Length)
                Array.Resize(ref _items, _items.Length * 2); // duplica capacidad
            _items[Count++] = item;                          // inserta y aumenta contador
        }

        public T this[int index]                             // indexador
        {
            get
            {
                if (index < 0 || index >= Count)
                    throw new ArgumentOutOfRangeException(); // valida índice
                return _items[index];                        // devuelve elemento
            }
            set
            {
                if (index < 0 || index >= Count)
                    throw new ArgumentOutOfRangeException(); // valida índice
                _items[index] = value;                       // asigna nuevo valor
            }
        }

        public void Sort(Comparison<T> cmp)                   // ordena con comparación
        {
            Array.Sort(_items, 0, Count, Comparer<T>.Create(cmp)); // usa Array.Sort
        }

        public T[] ToArray()                                  // convierte a array
        {
            var copia = new T[Count];                         // crea array del tamaño exacto
            Array.Copy(_items, copia, Count);                 // copia elementos
            return copia;                                     // devuelve copia
        }
    }
}
