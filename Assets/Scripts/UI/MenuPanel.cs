using System;

/// <summary>
/// Panel que muestra el botón para regresar al menú principal
/// </summary>
public class MenuPanel : Panel
{

    /// <summary>
    /// Botón que permite salir del nivel y regresar al menú principal
    /// </summary>
    public UnityEngine.UI.Button menuButton;

    public override void Show()
    {
        panel.SetActive(true);
        menuButton.interactable = false;
        state = State.Visible;
    }

    public override void Hide()
    {
        panel.SetActive(false);
        menuButton.interactable = true;
        state = State.Hidden;
    }

    public override void UpdatePanel()
    {
        throw new NotImplementedException();
    }

}
