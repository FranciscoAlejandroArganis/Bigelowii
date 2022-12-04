using UnityEngine;

public class PrismaticDischargeVFX : Wrapper
{

    public AudioSource audioSource;

    public LineRenderer line;

    public Vector3[] points;

    public override void Play()
    {
        foreach (Vector3 point in points)
        {
            LineRenderer line = Instantiate(this.line, transform.position, Quaternion.identity, transform);
            line.SetPosition(0, transform.position);
            line.SetPosition(1, point);
        }
        audioSource.Play();
    }

    public override void Stop()
    {
        foreach (Transform child in transform)
            Destroy(child.gameObject);
        audioSource.Stop();
    }

}
