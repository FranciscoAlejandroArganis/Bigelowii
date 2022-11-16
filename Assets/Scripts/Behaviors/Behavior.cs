/// <summary>
/// Comportamiento que puede tener una unidad
/// </summary>
public abstract class Behavior
{

    /// <summary>
    /// Predicado que determina si un comportamiento es de un tipo especificado
    /// </summary>
    /// <typeparam name="T">El tipo del comportamiento</typeparam>
    /// <param name="behavior">El comportamiento que se prueba</param>
    /// <returns><c>true</c> si el comportamiento es de tipo <c>T</c></returns>
    public static bool Is<T>(Behavior behavior)
    {
        return behavior is T;
    }

    /// <summary>
    /// La unidad que tiene el comportamiento
    /// </summary>
    public Unit unit;

    /// <summary>
    /// Construye un nuevo comportamiento para la unidad especificada
    /// </summary>
    /// <param name="unit">La unidad que tendrá el comportamiento</param>
    public Behavior(Unit unit)
    {
        this.unit = unit;
    }

    /// <summary>
    /// Se manda a llamar cuando el comportamiento se agrega a la unidad
    /// </summary>
    public abstract void OnAdd();

    /// <summary>
    /// Se manda a llamar cuando el comportamiento se elimina de la unidad
    /// </summary>
    public abstract void OnRemove();

    /// <summary>
    /// Modifica el efecto especificado
    /// </summary>
    /// <param name="effect">El efecto que se modifica</param>
    public abstract void Modify(Effect effect);

}
