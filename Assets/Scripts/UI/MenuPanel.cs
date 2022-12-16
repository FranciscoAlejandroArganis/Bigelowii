using System;

/// <summary>
/// Panel que muestra el bot�n para regresar al men� principal
/// </summary>
public class MenuPanel : Panel
{

    /// <summary>
    /// Bot�n que permite salir del nivel y regresar al men� principal
    /// </summary>
    public UnityEngine.UI.Button menuButton;

    public override void Show()
    {
        Audio.PlayClip(Audio.sounds.click2);
        panel.SetActive(true);
        menuButton.interactable = false;
        state = State.Visible;
    }

    public override void Hide()
    {
        Audio.PlayClip(Audio.sounds.click2);
        panel.SetActive(false);
        menuButton.interactable = true;
        state = State.Hidden;
    }

    public void Home()
    {
        Audio.PlayClip(Audio.sounds.click2);
        Scene.GoToMainMenu();
    }

    public override void UpdatePanel()
    {
        throw new NotImplementedException();
    }

}
