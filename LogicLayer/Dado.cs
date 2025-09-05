using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    class Dado
    {
        public List<int> Valores { get; }

        public Dado(Random random)
        {
            var valoresUnicos = new HashSet<int>();
            while (valoresUnicos.Count < 3)
            {
                int numero = random.Next(1, 7); // 1 a 6 inclusive
                valoresUnicos.Add(numero);
            }
            Valores = new List<int>(valoresUnicos);
        }

        public int Sumar()
        {
            int suma = 0;
            foreach (var n in Valores)
                suma += n;
            return suma;
        }
    }

    class Program
    {
        static void Main()
        {
            var random = new Random();
            string? input;

            Console.WriteLine("Escribe /dice para tirar los dados o cualquier otra tecla para salir:");
            input = Console.ReadLine();

            while (input != null && input.Trim().Equals("/dice", StringComparison.OrdinalIgnoreCase))
            {
                LanzarDados(random);

                Console.WriteLine("Escribe /dice para tirar otra vez los dados o cualquier otra tecla para salir:");
                input = Console.ReadLine();
            }
        }

        static void LanzarDados(Random random)
        {
            var dado1 = new Dado(random);
            var dado2 = new Dado(random);

            Console.WriteLine("Números aleatorios diferentes entre 1 y 6 para dado1:");
            for (int i = 0; i < dado1.Valores.Count; i++)
            {
                Console.WriteLine(dado1.Valores[i]);
            }

            Console.WriteLine("Números aleatorios diferentes entre 1 y 6 para dado2:");
            for (int i = 0; i < dado2.Valores.Count; i++)
            {
                Console.WriteLine(dado2.Valores[i]);
            }

            int sumaDado1 = dado1.Sumar();
            int sumaDado2 = dado2.Sumar();

            Console.WriteLine($"Suma de dado1: {sumaDado1}");
            Console.WriteLine($"Suma de dado2: {sumaDado2}");

            if (sumaDado1 > sumaDado2)
            {
                Console.WriteLine("¿La suma de dado1 es mayor que la de dado2? True");
            }
            else if (sumaDado1 < sumaDado2)
            {
                Console.WriteLine("¿La suma de dado1 es mayor que la de dado2? False");
            }
            else
            {
                Console.WriteLine("Empate: ambas sumas son iguales.");
            }
        }
    }

}
