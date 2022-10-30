/// <summary>
/// Acci�n que puede realizar una unidad
/// </summary>
public abstract class Action
{

    /// <summary>
    /// La unidad que realiza la acci�n
    /// </summary>
    public Unit unit;

    /// <summary>
    /// Construye una nueva acci�n
    /// </summary>
    /// <param name="unit">La unidad que realiza la acci�n</param>
    public Action(Unit unit)
    {
        this.unit = unit;
    }

    /// <summary>
    /// Se manda a llamar cuando ocurre el efecto de la acci�n
    /// </summary>
    public abstract void Execute();

    /// <summary>
    /// Asigna el objetivo de la acci�n
    /// </summary>
    /// <param name="target">La celda objetivo de la acci�n</param>
    public abstract void SetTarget(Cell target);

    /// <summary>
    /// Regresa la celda objetivo de la acci�n
    /// </summary>
    /// <returns>La celda objetivo de la acci�n</returns>
    public abstract Cell GetTarget();

    /// <summary>
    /// Busca los posibles objetivos de la acci�n
    /// </summary>
    /// <returns><c>true</c> si se encontr� al menos 1 objetivo v�lido</returns>
    public abstract bool SearchTargets();

    /// <summary>
    /// Limpia las propiedades de las celdas despu�s de que se buscaron objetivos
    /// </summary>
    public abstract void ClearTargets();

    /// <summary>
    /// Asigna las propiedades del bot�n de evento especificado
    /// </summary>
    /// <param name="eventButton">El bot�n al que se le asignan las propiedades</param>
    public abstract void SetEventButton(EventButton eventButton);

}
