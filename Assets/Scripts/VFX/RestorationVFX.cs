using UnityEngine;
using UnityEngine.VFX;

public class RestorationVFX : Wrapper
{

    public AudioSource audioSource;

    public VisualEffect template;
    
    private VisualEffect instance;

    public override void Play()
    {
        instance = Instantiate(template, transform.position + new Vector3(0, .00390625f, 0), Quaternion.identity);
        audioSource.Play();
    }

    public override void Stop()
    {
        Destroy(instance.gameObject);
    }

}
