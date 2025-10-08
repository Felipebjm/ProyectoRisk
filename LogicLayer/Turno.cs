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
        // jugador que tiene este turno
        public Jugador Jugador { get; }

        // cola que guarda el orden de las fases (Planeacion, Ataque, Refuerzo)
        private Cola<FaseTurno> fases;

        // fase actual del turno
        public FaseTurno FaseActual { get; private set; }

        // indicador si ya se realizo el ataque en este turno
        private bool ataqueRealizado;

        // indicador si ya se realizo el movimiento de tropas en la fase de planeacion
        public bool movimientoRealizado;

        // constructor: recibe el jugador y prepara las fases en orden inicial
        public Turno(Jugador jugador)
        {
            Jugador = jugador;
            ReiniciarFases();
        }

        // reinicia la cola de fases al inicio del turno en el orden:
        // Planeacion -> Ataque -> Refuerzo
        public void ReiniciarFases()
        {
            fases = new Cola<FaseTurno>(3);
            fases.Enqueue(FaseTurno.Planeacion);
            fases.Enqueue(FaseTurno.Ataque);
            fases.Enqueue(FaseTurno.Refuerzo);

            FaseActual = fases.Peek();
            movimientoRealizado = false;
            ataqueRealizado = false;
        }

        // avanza a la siguiente fase y resetea los flags segun corresponda
        public void SiguienteFase()
        { 
            fases.Enqueue(fases.Dequeue()); // rota la cola
            FaseActual = fases.Peek();

            // al volver a Planeacion, permito mover tropas otra vez
            if (FaseActual == FaseTurno.Planeacion)
                movimientoRealizado = false;

            // al iniciar Ataque, permito un nuevo ataque
            if (FaseActual == FaseTurno.Ataque)
                ataqueRealizado = false;
        }

        // Fase de planeacion: mover tropas entre territorios propios
        public bool FasePlaneacion(Territorio origen, Territorio destino, int cantidad)
        {
            if (FaseActual != FaseTurno.Planeacion)
            {
                Console.WriteLine("No es fase de planeacion.");
                return false;
            }

            return MoverTropasPlaneacion(origen, destino, cantidad);
        }

        // Fase de ataque: permite exactamente un ataque por turno
        public bool FaseAtaque(Territorio origen, Territorio destino, int atq, int def, Random rnd)
        {
            if (FaseActual != FaseTurno.Ataque)
            {
                Console.WriteLine("No es fase de ataque.");
                return false;
            }
            if (ataqueRealizado)
            {
                Console.WriteLine("Ya realizaste tu ataque este turno.");
                return false;
            }

            bool resultado = Atacar(origen, destino, atq, def, rnd);
            ataqueRealizado = true;
            return resultado;
        }

        // Fase de refuerzo: aplica refuerzos automaticos
        public void FaseRefuerzo(ImpLinkedList<Continente> continentes)
        {
            if (FaseActual != FaseTurno.Refuerzo)
            {
                Console.WriteLine("No es fase de refuerzo.");
                return;
            }

            AplicarRefuerzos(continentes);
        }

        // ===================== metodos internos =====================

        // aplica refuerzos basados en numero de territorios y bonus de continentes
        private void AplicarRefuerzos(ImpLinkedList<Continente> continentes)
        {
            int refuerzoBase = Jugador.TerritoriosConq() / 3; //
            int bonus = 0;
            foreach (var cont in continentes)
            {
                if (cont.ControlTotal(Jugador)) // si el jugador controla todo el continente
                    bonus += cont.BonusRefuerzo; // suma el bonus del continente
            }

            int total = refuerzoBase + bonus;
            Jugador.TropasDisponibles += total;
            Console.WriteLine($"{Jugador.Nombre} recibe {refuerzoBase} por territorios + {bonus} por continentes = {total} refuerzos.");
        }

        // realiza el combate, ajusta tropas y gestiona conquistas o perdidas
        private bool Atacar(Territorio origen, Territorio destino, int ejercitosAtacante, int ejercitosDefensor, Random random)
        {
            // valida que el origen sea del jugador y tenga al menos 2 tropas
            if (origen.Dueno_territorio != Jugador || origen.Tropas_territorio < 2)
            {
                Console.WriteLine("No puedes atacar desde este territorio.");
                return false;
            }

            // valida que el destino no sea del mismo jugador
            if (destino.Dueno_territorio == Jugador)
            {
                Console.WriteLine("No puedes atacar un territorio que ya es tuyo.");
                return false;
            }

            int maxAtq = Math.Min(3, origen.Tropas_territorio - 1);
            int maxDef = Math.Min(2, destino.Tropas_territorio);

            if (ejercitosAtacante < 1 || ejercitosAtacante > maxAtq)
            {
                Console.WriteLine($"Atacante: dados invalidos (1 a {maxAtq}).");
                return false;
            }
            if (ejercitosDefensor < 1 || ejercitosDefensor > maxDef)
            {
                Console.WriteLine($"Defensor: dados invalidos (1 a {maxDef}).");
                return false;
            }

            var dadosAtq = LanzarDados(ejercitosAtacante, random);
            var dadosDef = LanzarDados(ejercitosDefensor, random);

            Console.WriteLine($"Dados atacante: {string.Join(", ", dadosAtq)}");
            Console.WriteLine($"Dados defensor:   {string.Join(", ", dadosDef)}");

            int rondas = Math.Min(dadosAtq.Count, dadosDef.Count);
            for (int i = 0; i < rondas; i++)
            {
                if (dadosAtq[i] > dadosDef[i])
                {
                    // defensor pierde tropas
                    destino.Restar_Tropas_Terr(1);
                    Console.WriteLine($"Defensor pierde 1 tropa en {destino.Nombre}.");

                    // si el defensor se queda sin tropas, conquista el territorio
                    if (destino.Tropas_territorio == 0)
                        ConquistarTerritorio(origen, destino, ejercitosAtacante);
                }
                else
                {
                    // atacante pierde tropas
                    origen.Restar_Tropas_Terr(1);
                    Console.WriteLine($"Atacante pierde 1 tropa en {origen.Nombre}.");

                    // si el atacante se queda sin tropas, pierde el territorio
                    if (origen.Tropas_territorio == 0)
                    {
                        Console.WriteLine($"{Jugador.Nombre} pierde {origen.Nombre} por quedarse sin tropas.");
                        origen.AsignarDueno_Terr(null);
                    }
                }
            }

            return true;
        }

        // lanza los dados y los ordena de mayor a menor
        private Lista<int> LanzarDados(int cantidad, Random random)
        {
            var d = new Dado(cantidad, random);
            var lista = new Lista<int>(d.Valores);
            lista.Sort((a, b) => b.CompareTo(a));
            return lista;
        }

        // maneja la conquista de un territorio tras eliminar todas sus tropas
        private void ConquistarTerritorio(Territorio origen, Territorio destino, int ejercitosAtacante)
        {
            Console.WriteLine($"{Jugador.Nombre} ha conquistado {destino.Nombre}!");
            int mover = Math.Min(ejercitosAtacante, origen.Tropas_territorio - 1); // debe dejar al menos 1 tropa en origen
            destino.AsignarDueno_Terr(Jugador);
            destino.Agregar_Tropas_Terr(mover);
            origen.Restar_Tropas_Terr(mover);
            Console.WriteLine($"{Jugador.Nombre} mueve {mover} tropas de {origen.Nombre} a {destino.Nombre}.");
        }

        // mueve tropas entre territorios propios durante la fase de planeacion
        private bool MoverTropasPlaneacion(Territorio origen, Territorio destino, int cantidad)
        {
            if (movimientoRealizado)
            {
                Console.WriteLine("Ya moviste tropas en esta fase.");
                return false;
            }
            if (origen.Dueno_territorio != Jugador || destino.Dueno_territorio != Jugador)
            {
                Console.WriteLine("Ambos territorios deben ser tuyos.");
                return false;
            }
            if (cantidad < 1 || origen.Tropas_territorio <= cantidad)
            {
                Console.WriteLine("Debes dejar al menos 1 tropa en el origen.");
                return false;
            }

            origen.Restar_Tropas_Terr(cantidad);
            destino.Agregar_Tropas_Terr(cantidad);
            movimientoRealizado = true;
            Console.WriteLine($"{Jugador.Nombre} mueve {cantidad} tropas de {origen.Nombre} a {destino.Nombre}.");
            return true;
        }
        public void SiguienteFaseAutomatica()
        {
            SiguienteFase();
        }

    }


}
