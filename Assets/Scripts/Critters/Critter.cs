using UnityEngine;

public abstract class Critter : MonoBehaviour
{

    public float movementSpeed;

    protected Vector3 positionStart;

    protected Vector3 positionEnd;

    /// <summary>
    /// Tiempo actual del cambio
    /// <para>Empieza en 0 y termina en 1</para>
    /// </summary>
    protected float time;

    /// <summary>
    /// Velocidad actual del cambio
    /// <para>Determina qué tan rápido se completa el cambio</para>
    /// </summary>
    protected float speed;

    public virtual void Start()
    {
        StartNewMovement();
    }

    public void Update()
    {
        transform.position = GetPosition();
        transform.rotation = CameraController.instance.transform.rotation;
        time += speed * Time.deltaTime;
        if (time >= 1)
        {
            transform.position = positionEnd;
            time = 0;
            StartNewMovement();
        }
    }

    public abstract Vector3 GetPosition();

    /// <summary>
    /// Establece los parámetros para empezar un nuevo movimiento
    /// <para>Asigna los valores de <c>positionStart</c>, <c>positionEnd</c> y <c>speed</c></para>
    /// </summary>
    public abstract void StartNewMovement();

}
