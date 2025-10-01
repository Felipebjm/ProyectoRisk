using LogicLayer.LinkedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public class Gameplay
{
    public Jugador Jugador1 { get; private set; }
    public Jugador Jugador2 { get; private set; }
    public Jugador Jugador3 { get; private set; } // Nuevo jugador

    public ImpLinkedList<Territorio> Territorios { get; private set; } 
    public ImpLinkedList<Continente> Continentes { get; private set; } 
    public Cola<Turno> turnos; // Cola para manejar los turnos de los jugadores
    public Turno TurnoActual { get; private set; } // Turno actual

    public Gameplay()
    {
        // Crear jugadores
        Jugador1 = new Jugador(1, "Jugador 1", ConsoleColor.Red);
        Jugador2 = new Jugador(2, "Jugador 2", ConsoleColor.Blue);
        Jugador3 = new Jugador(3, "Jugador 3", ConsoleColor.Green);
        // Tropas iniciales
        Jugador1.TropasDisponibles = 35;
        Jugador2.TropasDisponibles = 35;
        Jugador3.TropasDisponibles = 35;

        //Crear continentes
        CrearContinentes();

        //Crear territorios y asignarlos a su continente
        Territorios = new ImpLinkedList<Territorio>();
        for (int i = 1; i <= 30; i++) //cada iteracion i es el id del territorio
            {
            // Determina a qué continente pertenece el territorio i
            int continenteId = ObtenerContinenteIdPorTerritorio(i); // Metodo que devuelve el id del continente segun el id del territorio
            var continente = Continentes.First(c => c.Id == continenteId); // Busca el continente en la lista de continentes

             // Construye el territorio con referencia al continente
            var territorio = new Territorio(i, $"Territorio {i}", continente, 0,  0);
            Territorios.Agregar(territorio);

            // Registra el territorio dentro del continente
            continente.Anadir_Territorio(territorio);
        }

   

        // Asignar territorios alternadamente a los 3 jugadores
        int idx = 0;
        foreach (var territorio in Territorios)
        {
            if (idx % 3 == 0)
                territorio.AsignarDueno_Terr(Jugador1);
            else if (idx % 3 == 1)
                territorio.AsignarDueno_Terr(Jugador2);
            else
                territorio.AsignarDueno_Terr(Jugador3);

            idx++;
        }

        // Inicializar cola de turnos con 3 jugadores
        turnos = new Cola<Turno>(3);
        turnos.Enqueue(new Turno(Jugador1));
        turnos.Enqueue(new Turno(Jugador2));
        turnos.Enqueue(new Turno(Jugador3));

        CambiarTurno();
    }
    public void CrearContinentes()
    {
            Continentes = new ImpLinkedList<Continente>();

            Continentes.Agregar(new Continente(1, "América del Norte", 5, 6)); //id, nombre, bonus, cantidad de territorios
            Continentes.Agregar(new Continente(2, "Europa",4, 6));
            Continentes.Agregar(new Continente(3, "Asia", 2, 8));
            Continentes.Agregar(new Continente(4, "África", 3, 5));
            Continentes.Agregar(new Continente(5, "Oceanía", 2, 5));
            Continentes.Agregar(new Continente(6, "América del Sur", 2, 4));
     }
    public int ObtenerContinenteIdPorTerritorio(int idTerritorio) //Para obtener el id del continente segun el id del territorio
        {
        if (idTerritorio <= 6) return 1;  // America del Norte
        if (idTerritorio <= 12) return 2;  // Europa
        if (idTerritorio <= 20) return 3;  // Asia
        if (idTerritorio <= 25) return 4;  // África
        return 5;                          // Oceanía
    }
        public void CambiarTurno()
    {
        if (TurnoActual != null)
            TurnoActual.Jugador.Turno = false;

        TurnoActual = turnos.Dequeue();
        TurnoActual.Jugador.Turno = true;
        TurnoActual.ReiniciarFases();
        turnos.Enqueue(TurnoActual);

        Console.WriteLine($"\n--- Turno de {TurnoActual.Jugador.Nombre} ---");
    }

    public void IniciarJuego()
    {
        Console.WriteLine($"Comienza la partida entre {Jugador1.Nombre}, {Jugador2.Nombre} y {Jugador3.Nombre}.");
        Console.WriteLine($"{Jugador1.Nombre} tiene {Jugador1.TropasDisponibles} tropas y {Jugador1.TerritoriosConq()} territorios.");
        Console.WriteLine($"{Jugador2.Nombre} tiene {Jugador2.TropasDisponibles} tropas y {Jugador2.TerritoriosConq()} territorios.");
        Console.WriteLine($"{Jugador3.Nombre} tiene {Jugador3.TropasDisponibles} tropas y {Jugador3.TerritoriosConq()} territorios.");
        Console.WriteLine($"Empieza el turno de: {TurnoActual.Jugador.Nombre}");
    }

    public void EjecutarTurno(ImpLinkedList<Continente> continentes, Random random, Territorio origen, 
    Territorio destino,
    int ejercitosAtacante,
    int ejercitosDefensor,
    int cantidad)
                    
        {
        // Fase de refuerzo
        TurnoActual.FaseRefuerzo(this.Continentes);
        TurnoActual.SiguienteFase();

        // Fase de ataque
        TurnoActual.FaseAtaque(origen, destino, ejercitosAtacante, ejercitosDefensor, random);
        TurnoActual.SiguienteFase();

        // Fase de planeacion
        TurnoActual.FasePlaneacion(origen, destino, cantidad);
        TurnoActual.SiguienteFase();

        // Al terminar las 3 fases, pasa el turno al siguiente jugador
        CambiarTurno();
    }

    public Jugador ObtenerJugadorEnTurno()
    {
        return TurnoActual.Jugador;
    }
}
}
