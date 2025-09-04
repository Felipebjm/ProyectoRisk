using System;
using System.Text.Json;
using System.IO;
using System.Collections.Generic;

class Cola
{
    private readonly string[] data;
    private int first;
    private int last;
    private int size;
    private readonly int capacity;

    public Cola(int cap)
    {
        capacity = cap;
        data = new string[capacity];
        first = 0;
        last = -1;
        size = 0;
    }

    public void Enqueue(string valor)
    {
        if (size == capacity)
        {
            Console.WriteLine("Cola llena");
            return;
        }
        last = (last + 1) % capacity;
        data[last] = valor;
        size++;
    }

    public void Imprimir()
    {
        if (size == 0)
        {
            Console.WriteLine("Cola vacía");
            return;
        }
        for (int j = 0, i = first; j < size; j++, i = (i + 1) % capacity)
            Console.WriteLine(data[i]);
    }

    public string[] ObtenerElementos()
    {
        var resultado = new string[size];
        for (int j = 0, i = first; j < size; j++, i = (i + 1) % capacity)
            resultado[j] = data[i];
        return resultado;
    }

    public static void MostrarDatosGuardados()
    {
        const string fileName = "movements.json";
        if (!File.Exists(fileName))
        {
            Console.WriteLine("El archivo movements.json no existe.");
            return;
        }

        var datos = JsonSerializer.Deserialize<string[]>(File.ReadAllText(fileName));
        Console.WriteLine("\nDatos guardados en movements.json:");
        if (datos == null || datos.Length == 0)
            Console.WriteLine("No hay datos guardados.");
        else
            foreach (var d in datos)
                Console.WriteLine(d);
    }
}
