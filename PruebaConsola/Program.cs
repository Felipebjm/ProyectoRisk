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
            // 1) Lectura de nombres de los 3 jugadores en consola
            Console.Write("Jugador 1 nombre: ");

            // n1 almacena el nombre ingresado para el primer jugador,sin espacios al inicio o final  
            var n1 = Console.ReadLine()?.Trim();
            Console.Write("Jugador 2 nombre: "); 
            var n2 = Console.ReadLine()?.Trim();
            Console.Write("Jugador 3 nombre: ");
            
            var n3 = Console.ReadLine()?.Trim();

           
            // Crear la instancia del juego que inicializa jugadores, territorios y continentes
            var juego = new Gameplay();

            // Si el usuario ingreso un nombre valido, sobreescribe el nombre por defecto
            if (!string.IsNullOrWhiteSpace(n1)) // valida que no sea nulo o solo espacios
                juego.Jugador1.Nombre = n1;
            if (!string.IsNullOrWhiteSpace(n2))
                juego.Jugador2.Nombre = n2;
            if (!string.IsNullOrWhiteSpace(n3))
                juego.Jugador3.Nombre = n3;

            // 2) Asignacion minima se coloca una tropa en cada territorio para cada jugador
            foreach (var t in juego.Territorios) // t es un controlador del foreach
            {
                // jugador_dueno se refiere al objeto Jugador dueno de este territorio
                var jugador_dueno = t.Dueno_territorio;

                // verifica que exista un dueno y que le queden tropas disponibles
                if (jugador_dueno != null && jugador_dueno.TropasDisponibles > 0)
                {
                    // agregamos una tropa al territorio
                    t.Agregar_Tropas_Terr(1);
                    // reducimos en uno las tropas disponibles del jugador
                    jugador_dueno.TropasDisponibles--;
                }
            }

            // 3) Distribucion previa a la partida: cada jugador coloca el resto de sus tropas
            // jugadores es un arreglo con las tres instancias de Jugador en turno
            var jugadores = new[] { juego.Jugador1, juego.Jugador2, juego.Jugador3 };
            foreach (var j in jugadores) // j es el jugador que esta colocando tropas
            {
                // aviso de cuantas tropas le quedan por colocar al jugador
                Console.WriteLine($"\n{j.Nombre}: coloca tus {j.TropasDisponibles} tropas.");

                // mientras el jugador tenga tropas disponibles, repetimos el bucle
                while (j.TropasDisponibles > 0)
                {
                    Console.WriteLine("Territorios:");
                    // muestra en pantalla la lista de territorios que controla el jugador
                    foreach (var tt in j.Territorios) // tt es el territorio en la lista del jugador
                        Console.WriteLine($"  {tt.Id}. {tt.Nombre} (tropas: {tt.Tropas_territorio})");

                    Console.Write("ID destino: ");
                    // Se lee la linea y verifica que sea un numero valido
                    if (!int.TryParse(Console.ReadLine(), out int id) ||
                        !j.Territorios.Any(x => x.Id == id)) // comprueba que el ID este en la lista de territorios del jugador
                
                    {
                        Console.WriteLine("ID invalido.");
                        continue; // vuelve a pedir datos si el ID no es correcto
                    }

                    Console.Write("Cantidad a colocar: ");
                    // leemos la cantidad de tropas a colocar y comprobamos rango valido
                    if (!int.TryParse(Console.ReadLine(), out int c) ||c < 1 || c > j.TropasDisponibles) // comprueba que sea un numero y que este en el rango c es la cantidad
                    {
                        Console.WriteLine("Cantidad invalida.");
                        continue; // vuelve a pedir si la cantidad es fuera de rango
                    }

                    // dest es el territorio seleccionado por su ID
                    var dest = j.Territorios.First(x => x.Id == id); // busca en la lista del jugador el territorio con ese ID

                    // agrega c tropas a ese territorio  
                    dest.Agregar_Tropas_Terr(c); //c es la cantidad de tropas a agregar

                    // resta c de las tropas disponibles del jugador
                    j.TropasDisponibles -= c;
                    Console.WriteLine($"Colocaste {c} tropas en {dest.Nombre}. Restan {j.TropasDisponibles}.");
                }
            }

            // 4) Inicio de la partida despues de colocar todas las tropas

            Console.WriteLine("\n=== Empieza la partida! ===");
            // muestra informacion inicial de turno y jugadores
            juego.IniciarJuego();

            // rnd es el generador de numeros aleatorios usado para resolver los dados
            var rnd = new Random();

            // terminado indica cuando un jugador ha ganado y finaliza el juego
            bool terminado = false;

            // bucle principal: se repite hasta que alguien controle todos los territorios
            while (!terminado)
            {
                // jugador es el jugador activo en este turno
                var jugador = juego.ObtenerJugadorEnTurno();
                Console.WriteLine($"\n--- Turno de {jugador.Nombre} ---");

                // Fase 1: Planeacion de movimientos entre territorios propios
                MostrarTerritorios(jugador);
                Console.WriteLine("Fase Planeacion:");
                Console.Write("Origen ID: ");

                // oId guarda el ID del territorio de origen
                int oId = int.Parse(Console.ReadLine()); //es de donde vienen las tropas

                Console.Write("Destino ID: ");
                // dId guarda el ID del territorio de destino
                int dId = int.Parse(Console.ReadLine()); //es a donde van las tropas

                Console.Write("Mover tropas: ");
                // qty es la cantidad de tropas que se van a mover
                int qty = int.Parse(Console.ReadLine());
                // oT y dT son referencias a los objetos Territorio en la lista global

                var oT = juego.Territorios.First(t => t.Id == oId); //busca el primer territorio que coincida con el ID
                var dT = juego.Territorios.First(t => t.Id == dId); //busca el primer territorio que coincida con el ID
                // ejecuta la fase de planeacion con los territorios y cantidad indicada

                juego.TurnoActual.FasePlaneacion(oT, dT, qty); //territorio origen, territorio destino, cantidad
                // avanza automaticamente a la siguiente fase
                juego.TurnoActual.SiguienteFaseAutomatica();

                // Fase 2: Ataque, un solo ataque por turno
                MostrarTerritorios(jugador); // muestra los territorios del jugador
                Console.WriteLine("Fase Ataque:");
                Console.Write("Origen ID: ");
                oId = int.Parse(Console.ReadLine()); // oId es de donde vienen las tropas

                Console.Write("Destino ID: ");
                dId = int.Parse(Console.ReadLine()); // dId es a donde van las tropas

                Console.Write("Dados atacante (1-3): ");
                // atq es el numero de dados que lanzara el atacante
                int atq = int.Parse(Console.ReadLine());

                Console.Write("Dados defensor (1-2): ");
                // def es el numero de dados que lanzara el defensor
                int def = int.Parse(Console.ReadLine());

                oT = juego.Territorios.First(t => t.Id == oId); // busca el primer territorio que coincida con el ID
                dT = juego.Territorios.First(t => t.Id == dId); 

                // resuelve el combate con valores de dados y generador aleatorio
                juego.TurnoActual.FaseAtaque(oT, dT, atq, def, rnd); //territorio origen, territorio destino, dados atacante, dados defensor, random
                juego.TurnoActual.SiguienteFaseAutomatica(); // avanza automaticamente a la siguiente fase



                // Fase 3: Refuerzo automatico usando los continentes definidos
                Console.WriteLine("Fase Refuerzo automatica:");

                // ahora pasa la lista de continentes creada dentro de Gameplay
                juego.TurnoActual.FaseRefuerzo(juego.Continentes);
                juego.TurnoActual.SiguienteFaseAutomatica();



                // Revisa si el jugador controla todos los territorios para ganar
                if (jugador.TerritoriosConq() == juego.Territorios.Count()) // si el numero de territorios del jugador es igual al total
                {
                    Console.WriteLine($"\n¡{jugador.Nombre} controla todo el mapa y gana");
                    terminado = true; // salimos del bucle
                }
                else
                {
                    // si no gano, cambiamos al siguiente turno
                    juego.CambiarTurno();
                }
            }

            // Mensaje final antes de cerrar la aplicacion
            Console.WriteLine("Fin de la partida. Presiona ENTER para salir.");
            Console.ReadLine();
        }

        // Metodo de ayuda: muestra en consola los territorios que controla un jugador
        static void MostrarTerritorios(Jugador j)
        {
            Console.WriteLine("\nTus territorios:");
            foreach (var t in j.Territorios)
                Console.WriteLine($"  {t.Id}. {t.Nombre} (tropas: {t.Tropas_territorio})");
        }
    }
}