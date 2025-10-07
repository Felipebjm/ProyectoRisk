using System;
using System.Text.Json;
using System.IO;
using System.Collections.Generic;

using System;

public class Cola<T>
{
    private readonly T[] data;
    private int first;
    private int last;
    private int size;
    private readonly int capacity;
    public int Count
    {
        get { return size; }
    }
    public Cola(int cap)
    {
        if (cap <= 0)
            throw new ArgumentException("La capacidad debe ser mayor que cero.", nameof(cap));

        capacity = cap;
        data = new T[capacity];
        first = 0;
        last = -1;
        size = 0;
    }

    public bool IsEmpty => size == 0;
    public bool IsFull => size == capacity;

    public void Enqueue(T valor) // Agregar al final
    {
        if (IsFull)
        {
            Console.WriteLine("Cola llena");
            return;
        }

        last = (last + 1) % capacity;
        data[last] = valor;
        size++;
    }

    public T Dequeue() // Quitar del frente
    {
        if (IsEmpty)
            throw new InvalidOperationException("Cola vacía");

        T valor = data[first];
        data[first] = default;          // Opcional: libera la referencia si T es referencia
        first = (first + 1) % capacity;
        size--;
        return valor;
    }

    public T Peek() // Ver el primer elemento sin quitarlo
    {
        if (IsEmpty)
            throw new InvalidOperationException("Cola vacía");

        return data[first];
    }

    public void Imprimir() // Imprimir todos los elementos
    {
        if (IsEmpty)
        {
            Console.WriteLine("Cola vacía");
            return;
        }

        for (int j = 0, i = first; j < size; j++, i = (i + 1) % capacity)
            Console.WriteLine(data[i]);
    }

    public T[] ObtenerElementos() // Obtener todos los elementos en un array
    {
        var resultado = new T[size];
        for (int j = 0, i = first; j < size; j++, i = (i + 1) % capacity)
            resultado[j] = data[i];
        return resultado;
    }
}