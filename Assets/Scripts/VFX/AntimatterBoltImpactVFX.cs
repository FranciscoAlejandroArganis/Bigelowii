using UnityEngine;
using UnityEngine.VFX;

public class AntimatterBoltImpactVFX : Wrapper
{

    public AudioSource audioSource;

    private VisualEffect[] impact;

    public void Awake()
    {
        impact = GetComponentsInChildren<VisualEffect>();
    }

    public override void Play()
    {
        foreach (VisualEffect vfx in impact)
            vfx.Play();
        audioSource.Play();
    }

    public override void Stop()
    {
        foreach (VisualEffect vfx in impact)
            vfx.Stop();
    }

}
