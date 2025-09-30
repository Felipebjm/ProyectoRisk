// See https://aka.ms/new-console-template for more information
using LogicLayer;
using LogicLayer.LinkedList;
using System;
using System.Collections.Generic;
using System.Linq;

using System;
using System.Linq;

namespace CrazyRiskConsole
{
    class Program
    {
        static void Main()
        {
            // 1) Lectura de nombres
            Console.Write("Jugador 1 nombre: "); 
            var n1 = Console.ReadLine()?.Trim();
            Console.Write("Jugador 2 nombre: ");
            var n2 = Console.ReadLine()?.Trim();
            Console.Write("Jugador 3 nombre: ");
            var n3 = Console.ReadLine()?.Trim();

            var juego = new Gameplay();
            if (!string.IsNullOrWhiteSpace(n1)) juego.Jugador1.Nombre = n1; // Asignación de nombres si no están vacios
            if (!string.IsNullOrWhiteSpace(n2)) juego.Jugador2.Nombre = n2; 
            if (!string.IsNullOrWhiteSpace(n3)) juego.Jugador3.Nombre = n3;

            // 2) Asignación minima: 1 tropa en cada territorio
            foreach (var t in juego.Territorios) // Recorre todos los territorios en juego.territorio 
            {
                var jugador_dueno = t.Dueno_territorio; // Obtiene el dueño del territorio
                if (jugador_dueno != null && jugador_dueno.TropasDisponibles > 0) // Si tiene dueño y tropas disponibles
                {
                    t.Agregar_Tropas_Terr(1); // Coloca 1 tropa en el territorio
                    jugador_dueno.TropasDisponibles--; // Reduce las tropas disponibles del jugador
                }
            }

            // 3) Distribución previa a la partida
            var jugadores = new[] { juego.Jugador1, juego.Jugador2, juego.Jugador3 };// Arreglo de 3 jugadores
            foreach (var j in jugadores)  //J es el jugador 
            {
                Console.WriteLine($"\n{j.Nombre}: coloca tus {j.TropasDisponibles} tropas.");
                while (j.TropasDisponibles > 0) // Mientras queden tropas por colocar
                {
                    Console.WriteLine("Territorios:"); 
                    foreach (var tt in j.Territorios) // Muestra los territorios del jugador
                        Console.WriteLine($"  {tt.Id}. {tt.Nombre} (tropas: {tt.Tropas_territorio})");

                    Console.Write("ID destino: "); 
                    if (!int.TryParse(Console.ReadLine(), out int id) || !j.Territorios.Any(x => x.Id == id)) // Verifica que el ID sea valido y pertenezca al jugador
                    {
                        Console.WriteLine("ID inválido."); continue;
                    }

                    Console.Write("Cantidad a colocar: ");
                    if (!int.TryParse(Console.ReadLine(), out int c) ||c < 1 || c > j.TropasDisponibles) // Verifica que la cantidad sea valida y C son las tropas 
                    {
                        Console.WriteLine("Cantidad inválida."); continue;
                    }

                    var dest = j.Territorios.First(x => x.Id == id); // Obtiene el territorio destino
                    dest.Agregar_Tropas_Terr(c);// Coloca las tropas "C" son las tropas a colocar
                    j.TropasDisponibles -= c;
                    Console.WriteLine($"Colocaste {c} tropas en {dest.Nombre}. Restan {j.TropasDisponibles}.");
                }
            }

            // 4) Inicio de la partida
            Console.WriteLine("\n=== ¡Empieza la partida! ===");
            juego.IniciarJuego();

            var rnd = new Random();
            var continentes = new ImpLinkedList<Continente>();
            bool terminado = false; 

            while (!terminado)
            {
                var jugador = juego.ObtenerJugadorEnTurno();
                Console.WriteLine($"\n--- Turno de {jugador.Nombre} ---");

                // Fase 1: Planeación
                MostrarTerritorios(jugador);
                Console.WriteLine("Fase Planeación:");
                Console.Write("Origen ID: ");
                int oId = int.Parse(Console.ReadLine()); // Lee el ID del territorio origen
                Console.Write("Destino ID: ");
                int dId = int.Parse(Console.ReadLine()); // Lee el ID del territorio destino
                Console.Write("Mover tropas: ");
                int qty = int.Parse(Console.ReadLine());
                var oT = juego.Territorios.First(t => t.Id == oId); // Busca el territorio origen en la lista de territorios Ot es territorio origen
                var dT = juego.Territorios.First(t => t.Id == dId); // Busca el territorio destino en la lista de territorios Dt es territorio destino
                juego.TurnoActual.FasePlaneacion(oT, dT, qty); // Ejecuta la fase de planeacion y qty es la cantidad de tropas a mover
                juego.TurnoActual.SiguienteFaseAutomatica();

                // Fase 2: Ataque (exactamente uno)
                MostrarTerritorios(jugador);
                Console.WriteLine("Fase Ataque:");
                Console.Write("Origen ID: ");
                oId = int.Parse(Console.ReadLine()); // Lee el ID del territorio origen (de donde viene el ataque) oId es territorio q ataca
                Console.Write("Destino ID: ");
                dId = int.Parse(Console.ReadLine()); // Lee el ID del territorio destino (a donde va el ataque) did es territorio q defiende
                Console.Write("Dados atacante (1–3): ");
                int atq = int.Parse(Console.ReadLine()); // Lee la cantidad de dados del atacante (1-3)
                Console.Write("Dados defensor (1–2): ");
                int def = int.Parse(Console.ReadLine()); // Lee la cantidad de dados del defensor (1-2)
                oT = juego.Territorios.First(t => t.Id == oId); //oId territorio q ataca
                dT = juego.Territorios.First(t => t.Id == dId); //dId territorio q defiende // Busca el territorio destino en la lista de territorios
                juego.TurnoActual.FaseAtaque(oT, dT, atq, def, rnd); //Llama la fase ataque de clase Turno
                juego.TurnoActual.SiguienteFaseAutomatica();

                // Fase 3: Refuerzo
                Console.WriteLine("Fase Refuerzo automática:"); 
                juego.TurnoActual.FaseRefuerzo(continentes);
                juego.TurnoActual.SiguienteFaseAutomatica();

                // Cambio de turno
                if (jugador.TerritoriosConq() == 30) //condicion de victoria
                {
                    Console.WriteLine($"\n¡{jugador.Nombre} controla todo el mapa y gana!");
                    terminado = true;
                }
                else
                {
                    juego.CambiarTurno();
                }
            }

            Console.WriteLine("Fin de la partida. Presiona ENTER para salir.");
            Console.ReadLine();
        }

        static void MostrarTerritorios(Jugador j)
        {
            Console.WriteLine("\nTus territorios:");
            foreach (var t in j.Territorios) // j es el jugador
                Console.WriteLine($"  {t.Id}. {t.Nombre} (tropas: {t.Tropas_territorio})"); //muestras el territorio y sus atributos
        }
    }
}