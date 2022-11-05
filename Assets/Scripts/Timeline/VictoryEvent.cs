/// <summary>
/// Evento de victoria
/// </summary>
public class VictoryEvent : Event
{
    
    /// <summary>
    /// Construye un nuevo evento de victoria en el tiempo especificado
    /// </summary>
    /// <param name="time">El tiempo en el que el nivel termina en victoria</param>
    public VictoryEvent(int time) : base(null, time)
    {
        eventButton.button.interactable = false;
        eventButton.image.sprite = UI.sprites.empty;
    }

}
