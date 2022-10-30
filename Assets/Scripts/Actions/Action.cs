/// <summary>
/// Acción que puede realizar una unidad
/// </summary>
public abstract class Action
{

    /// <summary>
    /// La unidad que realiza la acción
    /// </summary>
    public Unit unit;

    /// <summary>
    /// Construye una nueva acción
    /// </summary>
    /// <param name="unit">La unidad que realiza la acción</param>
    public Action(Unit unit)
    {
        this.unit = unit;
    }

    /// <summary>
    /// Se manda a llamar cuando ocurre el efecto de la acción
    /// </summary>
    public abstract void Execute();

    /// <summary>
    /// Asigna el objetivo de la acción
    /// </summary>
    /// <param name="target">La celda objetivo de la acción</param>
    public abstract void SetTarget(Cell target);

    /// <summary>
    /// Regresa la celda objetivo de la acción
    /// </summary>
    /// <returns>La celda objetivo de la acción</returns>
    public abstract Cell GetTarget();

    /// <summary>
    /// Busca los posibles objetivos de la acción
    /// </summary>
    /// <returns><c>true</c> si se encontró al menos 1 objetivo válido</returns>
    public abstract bool SearchTargets();

    /// <summary>
    /// Limpia las propiedades de las celdas después de que se buscaron objetivos
    /// </summary>
    public abstract void ClearTargets();

    /// <summary>
    /// Asigna las propiedades del botón de evento especificado
    /// </summary>
    /// <param name="eventButton">El botón al que se le asignan las propiedades</param>
    public abstract void SetEventButton(EventButton eventButton);

}
