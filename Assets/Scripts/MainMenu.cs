using UnityEngine;

/// <summary>
/// Componente encargado de la pantalla del men� principal
/// </summary>
public class MainMenu : MonoBehaviour
{

    public static bool init;

    /// <summary>
    /// Velocidad con la que gira la c�mara en el men� principal
    /// </summary>
    public float speed;

    /// <summary>
    /// Referencia al scriptable object que contiene el conjuto de sprites
    /// </summary>
    public SpritesSet sprites;

    /// <summary>
    /// Referencia al scriptable object que contiene el conjunto de sonidos
    /// </summary>
    public SoundsSet sounds;

    public GameObject welcomeScreen;

    /// <summary>
    /// Arreglo con las unidades 2D que se muestran en el men� principal
    /// </summary>
    public GameObject[] dummyUnits2D;

    /// <summary>
    /// Arreglo con las unidades 3D que se muestran en el men� principal
    /// </summary>
    public GameObject[] dummyUnits3D;

    /// <summary>
    /// C�mara de la escena del men� principal
    /// </summary>
    private Camera mainMenuCamera;

    public void Awake()
    {
        mainMenuCamera = Camera.main;
        UI.sprites = sprites;
        Audio.sounds = sounds;
    }

    public void Start()
    {
        if (init)
            welcomeScreen.SetActive(false);
    }

    public void Update()
    {
        float angle = speed * Time.time;
        mainMenuCamera.transform.rotation = Quaternion.LookRotation(new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)));
        foreach (GameObject dummy in dummyUnits2D)
            dummy.transform.rotation = mainMenuCamera.transform.rotation;
        angle *= 4;
        foreach (GameObject dummy in dummyUnits3D)
            dummy.transform.rotation = Quaternion.LookRotation(new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)));
    }

    /// <summary>
    /// Se manda a llamar cuando se presiona el bot�n de salir del juego
    /// </summary>
    public void OnExit()
    {
        Audio.PlayClip(Audio.sounds.click2);
        Application.Quit();
    }

    public void OnPlay()
    {
        Audio.PlayClip(Audio.sounds.click2);
        init = true;
        welcomeScreen.SetActive(false);
    }

}
