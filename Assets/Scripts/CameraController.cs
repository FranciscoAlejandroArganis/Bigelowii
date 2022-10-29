using UnityEngine;

/// <summary>
/// Componente que controla la cámara
/// </summary>
public class CameraController : MonoBehaviour
{

    /// <summary>
    /// Velocidad con la que se mueve la cámara
    /// </summary>
    public float movementSpeed;

    /// <summary>
    /// Velocidad con la que rota la cámara
    /// </summary>
    public float rotationSpeed;

    /// <summary>
    /// Velocidad con la que se aumenta/disminuye el zoom de la cámara
    /// </summary>
    public float zoomSpeed;

    /// <summary>
    /// Valor mínimo de la coordenada x que puede tener el punto que observa de la cámara
    /// </summary>
    public float lowerX;

    /// <summary>
    /// Valor máximo de la coordenada x que puede tener el punto que observa de la cámara
    /// </summary>
    public float upperX;

    /// <summary>
    /// Valor mínimo de la coordenada z que puede tener el punto que observa de la cámara
    /// </summary>
    public float lowerZ;

    /// <summary>
    /// Valor máximo de la coordenada z que puede tener el punto que observa de la cámara
    /// </summary>
    public float upperZ;

    /// <summary>
    /// La posición en el mapa que está observando la cámara actualmente
    /// </summary>
    private Vector3 anchor;

    /// <summary>
    /// Valor actual de la rotación de la cámara
    /// <para>Entre 0 y 2pi</para>
    /// </summary>
    private float rotation;

    /// <summary>
    /// Valor actual del zoom de la cámara
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
    /// Reestablece los valores predeterminados de rotación y zoom de la cámara
    /// </summary>
    private void ResetRotationAndZoom()
    {
        rotation = Mathf.PI;
        zoom = 1;
    }

    /// <summary>
    /// Asegura que la rotación y zoom de la cámara sean valores válidos
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
    /// Asegura que la posición que observa la cámara sea válida
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
