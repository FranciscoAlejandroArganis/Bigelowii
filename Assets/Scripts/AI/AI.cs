using UnityEngine;

/// <summary>
/// Componente encargado de manejar la inteligencia artificial
/// </summary>
public class AI : MonoBehaviour
{

    /// <summary>
    /// Enumeración de los estados de la IA
    /// <list type="bullet">
    /// <item><c>Dormant</c>: la IA está inactiva</item>
    /// <item><c>DecisionMaking</c>: la IA está tomando una decisión</item>
    /// </list>
    /// </summary>
    public enum State
    {
        Dormant,
        DecisionMaking
    }

    /// <summary>
    /// Estado actual de la IA
    /// </summary>
    public static State state;

    /// <summary>
    /// Agente que está actuando actualmente
    /// </summary>
    public static Agent agent;

    /// <summary>
    /// Tiempo actual del cambio
    /// <para>Empieza en 0 y termina en 1</para>
    /// </summary>
    private float time;

    /// <summary>
    /// Velocidad actual del cambio
    /// <para>Determina qué tan rápido se completa el cambio</para>
    /// </summary>
    public float speed;

    public void Update()
    {
        if (Level.state == Level.State.AI)
        {
            switch (state)
            {
                case State.Dormant:
                    time += speed * Time.deltaTime;
                    if (time >= 1)
                        state = State.DecisionMaking;
                    break;
                case State.DecisionMaking:
                    agent.MakeDecision();
                    time = 0;
                    state = State.Dormant;
                    break;
            }
        }
    }

}
