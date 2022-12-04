using UnityEngine;

public class Bird : Critter
{

    public Vector3 lower;

    public Vector3 upper;

    public override Vector3 GetPosition()
    {
        return Vector3.Lerp(positionStart, positionEnd, time);
    }

    public override void StartNewMovement()
    {
        positionStart = transform.position;
        positionEnd = new Vector3(Random.Range(lower.x, upper.x), Random.Range(lower.y, upper.y), Random.Range(lower.z, upper.z));
        speed = positionStart == positionEnd ? float.PositiveInfinity : movementSpeed / Vector3.Magnitude(positionEnd - positionStart);
    }

}
