using UnityEngine;

/// <summary>
/// Componente que controla la c�mara
/// </summary>
public class CameraController : MonoBehaviour
{

    /// <summary>
    /// Enumeraci�n de los estados de la c�mara
    /// <list type="bullet">
    /// <item><c>Input</c>: la c�mara se est� moviendo de acuerdo a la entrada del jugador</item>
    /// <item><c>Fixed</c>: la c�mara no se mueve e ignora la entrada del jugador</item>
    /// <item><c>Cell</c>: la c�mara se est� moviendo hacia una celda fija e ignora la entrada del jugador</item>
    /// <item><c>Unit</c>: la c�mara est� siguiendo el movimiento de una unidad e ignora la entrada del jugador</item>
    /// </list>
    /// </summary>
    public enum State
    {
        Input,
        Fixed,
        Cell,
        Unit
    }

    /// <summary>
    /// Estado actual de la c�mara
    /// </summary>
    public static State state;

    /// <summary>
    /// La posici�n en el mapa que est� observando la c�mara actualmente
    /// </summary>
    private static Vector3 anchor;

    /// <summary>
    /// Valor actual de la rotaci�n de la c�mara
    /// <para>Entre 0 y tau</para>
    /// </summary>
    private static float rotation;

    /// <summary>
    /// Valor actual del zoom de la c�mara
    /// <para>Entre .5 y 1.5</para>
    /// </summary>
    private static float zoom;

    /// <summary>
    /// Tiempo actual del cambio
    /// <para>Empieza en 0 y termina en 1</para>
    /// </summary>
    private static float time;

    /// <summary>
    /// Velocidad actual del cambio
    /// <para>Determina qu� tan r�pido se completa el cambio</para>
    /// </summary>
    private static float speed;

    /// <summary>
    /// Posici�n inicial del punto que observa la c�mara antes del cambio
    /// </summary>
    private static Vector3 positionStart;

    /// <summary>
    /// Posici�n final del punto que observa la c�mara despu�s del cambio
    /// </summary>
    private static Vector3 positionEnd;

    /// <summary>
    /// Unidad que sigue actualmente la c�mara
    /// </summary>
    private static Unit unit;

    /// <summary>
    /// Ejemplar �nico de <c>CameraController</c>
    /// </summary>
    public static CameraController instance;

    /// <summary>
    /// Mueve la c�mara para que mire a la celda especificada
    /// </summary>
    /// <param name="cell">La celda que va a ser observada por la c�mara</param>
    public static void LookAt(Cell cell)
    {
        positionEnd = cell.transform.position;
        if (positionEnd == anchor) state = State.Fixed;
        else
        {
            positionStart = anchor;
            speed = instance.movementSpeed / Vector3.Magnitude(positionEnd - positionStart);
            time = 0;
            state = State.Cell;
        }
    }

    /// <summary>
    /// Hace que la c�mara siga a una unidad hasta que termine de moverse
    /// </summary>
    /// <param name="unit">La unidad que seguir� la c�mara</param>
    public static void Follow(Unit unit)
    {
        CameraController.unit = unit;
        state = State.Unit;
    }

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

    public void Start()
    {
        instance = this;
        anchor = Vector3.zero;
        ResetRotationAndZoom();
    }

    public void Update()
    {
        if (Input.GetButtonDown("ResetCamera"))
            ResetRotationAndZoom();
        rotation += rotationSpeed * Time.deltaTime * Input.GetAxis("Rotation");
        zoom -= zoomSpeed * Time.deltaTime * Input.GetAxis("Zoom");
        EnsureRotationAndZoomLimits();
        float cosine = Mathf.Cos(rotation);
        float sine = Mathf.Sin(rotation);
        switch (state)
        {
            case State.Input:
                anchor += movementSpeed * Time.deltaTime * (Input.GetAxis("Vertical") * new Vector3(-sine, 0, -cosine) + Input.GetAxis("Horizontal") * new Vector3(-cosine, 0, sine));
                EnsureAnchorLimits();
                break;
            case State.Fixed:
                if (Level.state == Level.State.Human) state = State.Input;
                break;
            case State.Cell:
                anchor = Vector3.Lerp(positionStart, positionEnd, time);
                time += speed * Time.deltaTime;
                if (time >= 1)
                {
                    anchor = positionEnd;
                    state = State.Fixed;
                }
                break;
            case State.Unit:
                anchor = new Vector3(unit.transform.position.x, 0, unit.transform.position.z);
                if (unit.movementController.state == MovementController.State.Stationary)
                {
                    unit = null;
                    state = State.Fixed;
                }
                break;
        }
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
