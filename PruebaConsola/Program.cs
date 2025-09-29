// See https://aka.ms/new-console-template for more information
using LogicLayer;
using LogicLayer.LinkedList;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CrazyRiskTest
{
    using System;
    using System.Linq;

    namespace CrazyRiskConsole
    {
        class Program
        {
            static void Main(string[] args)
            {
                // 1. Pedir nombre de cada jugador
                Console.Write("Ingrese el nombre del Jugador 1: ");
                string nombre1 = Console.ReadLine()?.Trim() ?? "Jugador 1";

                Console.Write("Ingrese el nombre del Jugador 2: ");
                string nombre2 = Console.ReadLine()?.Trim() ?? "Jugador 2";

                Console.Write("Ingrese el nombre del Jugador 3: ");
                string nombre3 = Console.ReadLine()?.Trim() ?? "Jugador 3";

                // 2. Crear la partida y asignar nombres
                var juego = new Gameplay();
                juego.Jugador1.Nombre = nombre1;
                juego.Jugador2.Nombre = nombre2;
                juego.Jugador3.Nombre = nombre3;

                // 3. Colocar 1 tropa en cada territorio asignado (mínimo 1 por territorio/continente)
                //    y restar esa tropa del pool del jugador.
                foreach (var territorio in juego.Territorios)
                {
                    var dueno = territorio.Dueno_territorio;
                    if (dueno == null)
                        continue;

                    // Asegurarse de que el jugador tenga tropas disponibles
                    if (dueno.TropasDisponibles <= 0)
                        throw new InvalidOperationException(
                            $"El jugador {dueno.Nombre} no tiene tropas suficientes para colocar en {territorio.Nombre}.");

                    territorio.Agregar_Tropas_Terr(1);
                    dueno.TropasDisponibles--;
                }

                // 4. Antes de la fase de ataque, permitir a cada jugador desplegar sus tropas restantes
                Console.WriteLine("\n== Distribución inicial de tropas adicionales ==");
                var jugadores = new[] { juego.Jugador1, juego.Jugador2, juego.Jugador3 };
                foreach (var jugador in jugadores)
                {
                    Console.WriteLine($"\nTurno de despliegue para {jugador.Nombre} " +
                                      $"(tropas disponibles: {jugador.TropasDisponibles})");

                    // Mientras le queden tropas, las coloca en territorios que controla
                    while (jugador.TropasDisponibles > 0)
                    {
                        Console.WriteLine("\nTerritorios que posees:");
                        foreach (var t in jugador.Territorios)
                        {
                            Console.WriteLine($"  ID {t.Id}: {t.Nombre} (tropas: {t.Tropas_territorio})");
                        }

                        Console.Write("Ingresa el ID del territorio donde colocar tropas: ");
                        if (!int.TryParse(Console.ReadLine(), out int id) ||
                            !jugador.Territorios.Any(t => t.Id == id))
                        {
                            Console.WriteLine("ID inválido. Intenta de nuevo.");
                            continue;
                        }

                        var destino = jugador.Territorios
                                            .First(t => t.Id == id);

                        Console.Write($"¿Cuántas tropas colocar en {destino.Nombre}? ");
                        if (!int.TryParse(Console.ReadLine(), out int cantidad) ||
                            cantidad < 1 ||
                            cantidad > jugador.TropasDisponibles)
                        {
                            Console.WriteLine("Cantidad inválida. Intenta de nuevo.");
                            continue;
                        }

                        // Ejecutar despliegue
                        destino.Agregar_Tropas_Terr(cantidad);
                        jugador.TropasDisponibles -= cantidad;

                        Console.WriteLine(
                            $"{jugador.Nombre} colocó {cantidad} tropas en {destino.Nombre}. " +
                            $"Quedan {jugador.TropasDisponibles} tropas.");
                    }

                    Console.WriteLine($"Despliegue de {jugador.Nombre} finalizado.");
                }

                // 5. La partida está lista: empieza la fase de refuerzos y ataque por turnos
                Console.WriteLine("\n== ¡Comienza la fase de refuerzos y ataque! ==");
                juego.IniciarJuego();

                // Aquí podrías entrar en un bucle llamando a juego.EjecutarTurno(...)
                // con los parámetros que definas (continentes, random, origen, destino, tropas...).
                // Por ejemplo:
                //
                // var continentes = new ImpLinkedList<Continente>();
                // continentes.Agregar(/* todos tus continentes */);
                // var rnd = new Random();
                // while (!partidaFinalizada)
                // {
                //     // mostrar menú; leer origen, destino, ejércitos...
                //     juego.EjecutarTurno(continentes, rnd, origen, destino, atq, def, mov);
                // }
            }
        }
    }
}