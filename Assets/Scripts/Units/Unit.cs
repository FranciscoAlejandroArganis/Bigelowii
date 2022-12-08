using System.Collections.Generic;
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
    /// El costo de la unidad, en conos
    /// </summary>
    public int cost;

    /// <summary>
    /// Codifica las acciones que ha tomado la unidad en su turno actual
    /// <para>Los primeros 16 bits indican si se ha usado la acción correspondiente de la tarjeta de comandos</para>
    /// </summary>
    public int actionsTaken;

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
    /// Lista con los comportamientos de la unidad
    /// </summary>
    public List<Behavior> behaviors;

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

    public AudioSource audioSource;

    public virtual void Start()
    {
        behaviors = new List<Behavior>();
        player = GetComponentInParent<Player>();
        movementController = GetComponent<MovementController>();
        actionController = GetComponent<ActionController>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
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
    public abstract void SetCommandButton(CommandButton commandButton, uint card, int button);

    /// <summary>
    /// Se manda a llamar cuando la animación de una acción se encuentra en un cuadro con un evento
    /// </summary>
    public void AnimationEvent()
    {
        if (actionController.action != null)
            actionController.action.Execute();
    }

    /// <summary>
    /// Asigna las propiedades de un botón vacío
    /// </summary>
    /// <param name="commandButton">El botón al que se le asignan las propiedades</param>
    public void SetEmptyButton(CommandButton commandButton)
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
        commandButton.transition = 2;
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

    /// <summary>
    /// Asigna las propiedades de un botón de confirmar
    /// </summary>
    /// <param name="commandButton">El botón al que se le asignan las propiedades</param>
    protected void SetConfirmButton(CommandButton commandButton)
    {
        commandButton.image.sprite = UI.sprites.confirm;
        commandButton.action = null;
        commandButton.type = CommandButton.Type.Confirm;
        commandButton.transition = 0;
    }

}
