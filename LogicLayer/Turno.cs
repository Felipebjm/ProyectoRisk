using LogicLayer.EstructurasDatos;
using LogicLayer.LinkedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public enum FaseTurno // Enum para las fases del turno
    {
        Planeacion,
        Ataque,
        Refuerzo
    }

    public class Turno
    {
        public Jugador Jugador { get; }
        private Cola<FaseTurno> fases; // Cola para manejar las fases del turno
        public FaseTurno FaseActual { get; private set; }
        public bool movimientoRealizado; //True si se hizo un moviento        

        public Turno(Jugador jugador) // Constructor: el jugador que recibe tiene el turno
        {
            Jugador = jugador;
            fases = new Cola<FaseTurno>(3); // Capacidad fija de 3 fases

            fases.Enqueue(FaseTurno.Planeacion); 
            fases.Enqueue(FaseTurno.Ataque);
            fases.Enqueue(FaseTurno.Refuerzo);

            FaseActual = fases.Peek(); // La fase actual es la primera en la cola
            movimientoRealizado = false; // No se ha realizado movimiento al iniciar el turno
        }

        public void SiguienteFase() 
        {
            fases.Enqueue(fases.Dequeue()); // Mueve la fase actual al final de la cola
            FaseActual = fases.Peek(); // Actualiza la fase actual
            if (FaseActual == FaseTurno.Planeacion) 
                movimientoRealizado = false; // Reinicia el movimiento para la fase de planeación
        }

        public void ReiniciarFases() // Reinicia las fases al inicio del turno
        {
            //fases = new Cola<FaseTurno>(new[] { FaseTurno.Refuerzo, FaseTurno.Ataque, FaseTurno.Planeacion }); 
            fases = new Cola<FaseTurno>(3); 
            fases.Enqueue(FaseTurno.Refuerzo);
            fases.Enqueue(FaseTurno.Ataque);
            fases.Enqueue(FaseTurno.Planeacion);

            FaseActual = fases.Peek(); 
            movimientoRealizado = false;
        }

        // ==================== FASE DE REFUERZO ====================
        public void FaseRefuerzo(ImpLinkedList<Continente> continentes) //Verifica que sea la fase correcta y aplica refuerzos
        {
            if (!EsFase(FaseTurno.Refuerzo)) return; // Verifica que sea la fase correcta
            AplicarRefuerzos(continentes);  //LLama al metodo que aplica los refuerzos
        }

        // ==================== FASE DE ATAQUE ====================
        public bool FaseAtaque(Territorio origen, Territorio destino, int ejercitosAtacante, int ejercitosDefensor, Random random) //Destino es el territorio que se ataca
        {
            if (!EsFase(FaseTurno.Ataque)) return false;
            return Atacar(origen, destino, ejercitosAtacante, ejercitosDefensor, random); //Llama al metodo que realiza el ataque
        }

        // ==================== FASE DE PLANEACI N ====================
        public bool FasePlaneacion(Territorio origen, Territorio destino, int cantidad) //Destino es hacia donde se mueven las tropas y origen de donde 
        {
            if (!EsFase(FaseTurno.Planeacion)) return false;
            return MoverTropasPlaneacion(origen, destino, cantidad); //Llama al metodo que mueve las tropas
        }

        // -------------------- M todos internos --------------------

        public bool EsFase(FaseTurno fase) // Verifica si es la fase actual
        {
            if (FaseActual != fase) 
            {
                Console.WriteLine($"No es la fase de {fase}.");
                return false;
            }
            return true;
        }

        public void AplicarRefuerzos(ImpLinkedList<Continente> continentes)
        {
            // 1) Calcular refuerzo base: territorios controlados / 3
            int totalTerritorios = Jugador.TerritoriosConq();
            int refuerzoBase = totalTerritorios / 3;

            // 2) Calcular bonificacion por continentes completos
            int bonusContinente = 0;
            foreach (var cont in continentes) // Recorre todos los continentes
            {
                if (cont.ControlTotal(Jugador))
                    bonusContinente += cont.BonusRefuerzo; //BonusRefuerzo es un atributo de continente
            }

            // 3) Asignar refuerzos al jugador
            int totalRefuerzos = refuerzoBase + bonusContinente;
            Jugador.TropasDisponibles += totalRefuerzos;

            Console.WriteLine(
                $"{Jugador.Nombre} recibe {refuerzoBase} tropas por territorios " +
                $"+ {bonusContinente} por continentes = {totalRefuerzos} refuerzos.");
        }


        public bool Atacar(Territorio origen, Territorio destino, int ejercitosAtacante, int ejercitosDefensor, Random random) // Realiza el ataque
        {
            if (!ValidarAtaque(origen, destino, ejercitosAtacante, ejercitosDefensor)) // Valida si el ataque es posible llmando a validarAtaque
                return false;

            var atacanteDados = LanzarDados(ejercitosAtacante, random); // Lanza los dados para el atacante y defensor
            var defensorDados = LanzarDados(ejercitosDefensor, random);

            Console.WriteLine("Dados atacante: " + string.Join(", ", atacanteDados)); // Muestra los resultados de los dados
            Console.WriteLine("Dados defensor: " + string.Join(", ", defensorDados));

            int comparaciones = Math.Min(atacanteDados.Count, defensorDados.Count); // Numero de comparaciones (máximo 2)
            for (int i = 0; i < comparaciones; i++)
            {
                if (atacanteDados[i] > defensorDados[i])  //Compara los valores de los dados
                {
                    destino.Restar_Tropas_Terr(1); // Si el valor de defensor es menor, pierde una tropa
                    Console.WriteLine($"El defensor pierde 1 tropa en {destino.Nombre}.");
                }
                else
                {
                    origen.Restar_Tropas_Terr(1); // Sie el valor del atacante es menor o igual, pierde una tropa
                    Console.WriteLine($"El atacante pierde 1 tropa en {origen.Nombre}.");
                }
            }

            if (destino.Tropas_territorio == 0) // Si el defensor se queda sin tropas el atacante conquista el territorio
                ConquistarTerritorio(origen, destino, ejercitosAtacante);

            return true;
        }

        private bool ValidarAtaque(Territorio origen, Territorio destino, int ejercitosAtacante, int ejercitosDefensor) // Valida si el ataque es posible
        {
            if (origen.Dueno_territorio != Jugador || origen.Tropas_territorio < 2) // El territorio de origen debe pertenecer al jugador y tener al menos 1 tropas
            {
                Console.WriteLine("No puedes atacar desde este territorio."); //Si la condicon no se cumple retorna false
                return false;
            }
            if (destino.Dueno_territorio == null || destino.Dueno_territorio == Jugador) // Verifica que el territorio destino sea enemigo
            {
                Console.WriteLine("Debes atacar un territorio enemigo.");
                return false;
            }
            if (!origen.ConfirmarAdyacencia_Terr(destino)) // Verifica que los territorios sean adyacentes
            {
                Console.WriteLine("Solo puedes atacar territorios adyacentes.");
                return false;
            }
            int maxAtacante = Math.Min(3, origen.Tropas_territorio - 1); 
            int maxDefensor = Math.Min(2, destino.Tropas_territorio);

            if (ejercitosAtacante < 1 || ejercitosAtacante > maxAtacante) // Verifica que la cantidad de tropas usadas sea valida
            {
                Console.WriteLine($"El atacante solo puede usar entre 1 y {maxAtacante} ej rcitos.");
                return false;
            }
            if (ejercitosDefensor < 1 || ejercitosDefensor > maxDefensor) // Verifica que la cantidad de tropas usadas sea valida
            {
                Console.WriteLine($"El defensor solo puede usar entre 1 y {maxDefensor} ej rcitos.");
                return false;
            }
            return true;
        }

        private Lista<int> LanzarDados(int cantidad, Random random)
        {
            var dado = new Dado(cantidad, random);
            var valores = new Lista<int>(dado.Valores);
            valores.Sort((a, b) => b.CompareTo(a));
            return valores;
        }

        public void ConquistarTerritorio(Territorio origen, Territorio destino, int ejercitosAtacante)
        {
            Console.WriteLine($"{Jugador.Nombre} ha conquistado {destino.Nombre}!");

            int minTropasMover = ejercitosAtacante; // Mover al menos las tropas usadas en el ataque
            int maxTropasMover = origen.Tropas_territorio - 1; // Dejar al menos una tropa en el origen
            int tropasAMover = Math.Min(minTropasMover, maxTropasMover); 

            destino.AsignarDueno_Terr(Jugador); // Cambia el dueño del territorio llamando el metodo de territorio
            destino.Agregar_Tropas_Terr(tropasAMover); // Mueve las tropas al territorio conquistado
            origen.Restar_Tropas_Terr(tropasAMover);

            Console.WriteLine($"{Jugador.Nombre} mueve {tropasAMover} tropas de {origen.Nombre} a {destino.Nombre}.");
        }

        public bool MoverTropasPlaneacion(Territorio origen, Territorio destino, int cantidad) 
        {
            if (movimientoRealizado) // Solo se permite un movimiento por fase de planeacion
            {
                Console.WriteLine("Ya has realizado un movimiento de tropas en esta fase.");
                return false;
            }
            if (origen.Dueno_territorio != Jugador || destino.Dueno_territorio != Jugador) // Ambos territorios deben pertenecer al jugador
            {
                Console.WriteLine("Ambos territorios deben pertenecerte.");
                return false;
            }
            if (!origen.ConfirmarAdyacencia_Terr(destino)) // Los territorios deben ser adyacentes
            {
                Console.WriteLine("Los territorios deben ser adyacentes.");
                return false;
            }
            if (cantidad < 1 || origen.Tropas_territorio <= cantidad) // Debe mover al menos una tropa y dejar al menos una en el origen
            {
                Console.WriteLine("Debes dejar al menos una tropa en el territorio de origen.");
                return false;
            }

            origen.Restar_Tropas_Terr(cantidad); //Resta las tropas del territorio de origen
            destino.Agregar_Tropas_Terr(cantidad); //Anade las tropas al territorio destino
            movimientoRealizado = true;

            Console.WriteLine($"{Jugador.Nombre} mueve {cantidad} tropas de {origen.Nombre} a {destino.Nombre}.");
            return true;
        }
    }


}
