using UnityEngine;

public class ParticleBeamVFX : Wrapper
{

    public AudioSource audioSource;

    public LineRenderer line;

    public override void Play()
    {
        line.SetPosition(1, Vector3.forward);
        audioSource.Play();
    }

    public override void Stop()
    {
        line.SetPosition(1, Vector3.zero);
        audioSource.Stop();
    }

}
