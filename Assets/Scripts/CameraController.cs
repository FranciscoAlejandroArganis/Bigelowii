using UnityEngine;

/// <summary>
/// Componente que controla la c�mara
/// </summary>
public class CameraController : MonoBehaviour
{

    /// <summary>
    /// Velocidad con la que se mueve la c�mara
    /// </summary>
    public float movementSpeed;

    /// <summary>
    /// Velocidad con la que rota la c�mara
    /// </summary>
    public float rotationSpeed;

    /// <summary>
    /// Velocidad con la que se aumenta/disminuye el zoom de la c�mara
    /// </summary>
    public float zoomSpeed;

    /// <summary>
    /// Valor m�nimo de la coordenada x que puede tener el punto que observa de la c�mara
    /// </summary>
    public float lowerX;

    /// <summary>
    /// Valor m�ximo de la coordenada x que puede tener el punto que observa de la c�mara
    /// </summary>
    public float upperX;

    /// <summary>
    /// Valor m�nimo de la coordenada z que puede tener el punto que observa de la c�mara
    /// </summary>
    public float lowerZ;

    /// <summary>
    /// Valor m�ximo de la coordenada z que puede tener el punto que observa de la c�mara
    /// </summary>
    public float upperZ;

    /// <summary>
    /// La posici�n en el mapa que est� observando la c�mara actualmente
    /// </summary>
    private Vector3 anchor;

    /// <summary>
    /// Valor actual de la rotaci�n de la c�mara
    /// <para>Entre 0 y 2pi</para>
    /// </summary>
    private float rotation;

    /// <summary>
    /// Valor actual del zoom de la c�mara
    /// <para>Entre .5 y 1.5</para>
    /// </summary>
    private float zoom;

    public void Start()
    {
        anchor = Vector3.zero;
        ResetRotationAndZoom();
    }

    public void Update()
    {
        if (Input.GetButtonDown("ResetCamera"))
            ResetRotationAndZoom();
        rotation += Time.deltaTime * rotationSpeed * Input.GetAxis("Rotation");
        zoom -= Time.deltaTime * zoomSpeed * Input.GetAxis("Zoom");
        EnsureRotationAndZoomLimits();
        float cosine = Mathf.Cos(rotation);
        float sine = Mathf.Sin(rotation);
        anchor += Time.deltaTime * movementSpeed * (Input.GetAxis("Vertical") * new Vector3(-sine, 0, -cosine) + Input.GetAxis("Horizontal") * new Vector3(-cosine, 0, sine));
        EnsureAnchorLimits();
        Vector3 position = new Vector3(4 * sine, 8, 4 * cosine);
        transform.SetPositionAndRotation(anchor + zoom * position, Quaternion.LookRotation(-position));
    }

    /// <summary>
    /// Reestablece los valores predeterminados de rotaci�n y zoom de la c�mara
    /// </summary>
    private void ResetRotationAndZoom()
    {
        rotation = Mathf.PI;
        zoom = 1;
    }

    /// <summary>
    /// Asegura que la rotaci�n y zoom de la c�mara sean valores v�lidos
    /// </summary>
    private void EnsureRotationAndZoomLimits()
    {
        if (rotation < 0)
            rotation = 0;
        else if (rotation > Utilities.TAU)
            rotation = Utilities.TAU;
        if (zoom < .5f)
            zoom = .5f;
        else if (zoom > 1.5f)
            zoom = 1.5f;
    }

    /// <summary>
    /// Asegura que la posici�n que observa la c�mara sea v�lida
    /// </summary>
    private void EnsureAnchorLimits()
    {
        if (anchor.x < lowerX)
            anchor.x = lowerX;
        else if (anchor.x > upperX)
            anchor.x = upperX;
        if (anchor.z < lowerZ)
            anchor.z = lowerZ;
        else if (anchor.z > upperZ)
            anchor.z = upperZ;
    }

}
