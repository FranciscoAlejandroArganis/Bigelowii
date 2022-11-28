using UnityEngine;

public class Fish : Critter
{

    public float radius;

    private Vector3 center;

    private float[] parameters;

    public override void Start()
    {
        center = transform.position;
        transform.position = center + new Vector3(radius, 0, 0);
        parameters = new float[8];
        StartNewMovement();
    }

    public override Vector3 GetPosition()
    {
        float angle = time * Utilities.TAU;
        float x = Mathf.Cos(angle) + parameters[0] * Mathf.Cos(parameters[1] * angle) + parameters[2] * Mathf.Sin(parameters[3] * angle);
        float z = Mathf.Sin(angle) + parameters[4] * Mathf.Cos(parameters[5] * angle) + parameters[6] * Mathf.Sin(parameters[7] * angle);
        return center + radius * new Vector3(x, 0, z);
    }

    public override void StartNewMovement()
    {
        uint index = 0;
        while (index < parameters.Length)
        {
            parameters[index] = 1 / Random.Range(2, 16);
            parameters[index + 1] = Random.Range(2, 16);
            index += 2;
        }
        speed = movementSpeed + Random.Range(0, .5f);
    }

}
