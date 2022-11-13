using UnityEngine;

/// <summary>
/// Componente encargado de la pantalla del men� principal
/// </summary>
public class MainMenu : MonoBehaviour
{

    public Scene scene;

    void Start()
    {
        UI.sprites = GetComponent<SpriteSet>();
        scene.GoToLevel(3);
    }

}
