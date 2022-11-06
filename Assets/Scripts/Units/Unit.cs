using System;
using UnityEngine;

/// <summary>
/// Unidad
/// </summary>
public abstract class Unit : MonoBehaviour
{

    /// <summary>
    /// La salud actual de la unidad
    /// </summary>
    public uint health;

    /// <summary>
    /// La salud máxima de la unidad
    /// </summary>
    public uint maxHealth;

    /// <summary>
    /// Las unidades de tiempo que tiene que esperar entre cada turno
    /// </summary>
    public int delay;

    /// <summary>
    /// Las unidades de tiempo que tiene que esperar antes de su primer turno
    /// </summary>
    public int initialDelay;

    /// <summary>
    /// Máxima distancia que la unidad se puede mover en un turno
    /// </summary>
    public uint movement;

    /// <summary>
    /// Codifica las acciones que ha tomado la unidad en su turno actual
    /// <para>Los primeros 16 bits indican si se ha usado la acción correspondiente de la tarjeta de comandos</para>
    /// </summary>
    public uint actionsTaken;

    /// <summary>
    /// La celda sobre la que se encuentra la unidad
    /// </summary>
    public Cell cell;

    /// <summary>
    /// Jugador que controla la unidad
    /// </summary>
    public Player player;

    /// <summary>
    /// El agente que controla la unidad
    /// </summary>
    public Agent agent;

    /// <summary>
    /// Controlador del movimiento de la unidad
    /// </summary>
    public MovementController movementController;

    /// <summary>
    /// Controlador de las acciones de la unidad
    /// </summary>
    public ActionController actionController;

    /// <summary>
    /// Animador de la unidad
    /// </summary>
    public Animator animator;

    public virtual void Start()
    {
        player = GetComponentInParent<Player>();
        movementController = GetComponent<MovementController>();
        actionController = GetComponent<ActionController>();
        animator = GetComponent<Animator>();
        cell = Cell.Below(transform.position);
        if (cell)
            cell.unit = this;
    }

    /// <summary>
    /// Determina si la unidad especificada es aliada
    /// </summary>
    /// <param name="unit">La unidad que se prueba</param>
    /// <returns><c>true</c> si <c>unit</c> es del mismo equipo</returns>
    public bool IsFriendly(Unit unit)
    {
        return player && unit.player && player.team == unit.player.team;
    }

    /// <summary>
    /// Determina si la unidad especificada es enemiga
    /// </summary>
    /// <param name="unit">La unidad que se prueba</param>
    /// <returns><c>true</c> si <c>unit</c> es de otro equipo</returns>
    public bool IsHostile(Unit unit)
    {
        return player && unit.player && player.team != unit.player.team;
    }

    /// <summary>
    /// Regresa <c>true</c> si la unidad rota
    /// </summary>
    /// <returns><c>true</c> si la unidad rota</returns>
    public abstract bool Rotates();

    /// <summary>
    /// Regresa el sprite de la unidad
    /// </summary>
    /// <returns>El sprite de la unidad</returns>
    public abstract Sprite GetUnitSprite();

    /// <summary>
    /// Asigna las propiedades del botón especificado en la tarjeta de comandos
    /// </summary>
    /// <param name="commandButton">El botón al que se le asignan las propiedades</param>
    /// <param name="card">El índice de la tarjeta</param>
    /// <param name="button">El índice del botón</param>
    public abstract void SetCommandButton(CommandButton commandButton, uint card, uint button);

    /// <summary>
    /// Se manda a llamar cuando la animación de una acción se encuentra en un cuadro con un evento
    /// </summary>
    public void AnimationEvent()
    {
        actionController.action.Execute();
    }

    /// <summary>
    /// Se manda a llamar cuando la animación de muerte de la unidad está en el último cuadro
    /// </summary>
    public void Destroy()
    {
        Destroy(gameObject);
    }

    /// <summary>
    /// Asigna las propiedades de un botón vacío
    /// </summary>
    /// <param name="commandButton">El botón al que se le asignan las propiedades</param>
    protected void SetEmptyButton(CommandButton commandButton)
    {
        commandButton.image.sprite = UI.sprites.empty;
        commandButton.action = null;
        commandButton.type = CommandButton.Type.Empty;
        commandButton.transition = 0;
    }

    /// <summary>
    /// Asigna las propiedades de un botón de movimiento
    /// </summary>
    /// <param name="commandButton">El botón al que se le asignan las propiedades</param>
    protected void SetMoveButton(CommandButton commandButton)
    {
        commandButton.image.sprite = UI.sprites.move;
        commandButton.action = new Move(this);
        commandButton.type = CommandButton.Type.Command;
        commandButton.transition = 1;
    }

    /// <summary>
    /// Asigna las propiedades de un botón de terminar turno
    /// </summary>
    /// <param name="commandButton">El botón al que se le asignan las propiedades</param>
    protected void SetEndTurnButton(CommandButton commandButton)
    {
        commandButton.image.sprite = UI.sprites.endTurn;
        commandButton.action = new Sleep(this);
        commandButton.type = CommandButton.Type.Command;
        commandButton.transition = 1;
    }

    /// <summary>
    /// Asigna las propiedades de un botón de cancelar
    /// </summary>
    /// <param name="commandButton">El botón al que se le asignan las propiedades</param>
    protected void SetCancelButton(CommandButton commandButton)
    {
        commandButton.image.sprite = UI.sprites.cancel;
        commandButton.action = null;
        commandButton.type = CommandButton.Type.Cancel;
        commandButton.transition = 0;
    }

}
