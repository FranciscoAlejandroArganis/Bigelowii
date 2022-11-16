/// <summary>
/// Acción en la que expira un comportamiento <c>Blessed</c>
/// </summary>
public class DiamondUnblessing : BehaviorExpire
{

    /// <summary>
    /// Construye una nueva acción <c>DiamondUnblessing</c>
    /// </summary>
    /// <param name="blessed">El comportamiento que expira</param>
    public DiamondUnblessing(Blessed blessed) : base(blessed) { }

    public override void SetEventButton(EventButton eventButton)
    {
        eventButton.image.sprite = UI.sprites.empty;
    }

}
