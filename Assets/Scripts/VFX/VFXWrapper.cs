using UnityEngine;
using UnityEngine.VFX;

public class VFXWrapper : Wrapper
{

    public AudioSource audioSource;

    public VisualEffect template;

    public VisualEffect instance;

    public override void Play()
    {
        instance = Instantiate(template, transform.position, Quaternion.identity);
        audioSource.Play();
    }

    public override void Stop()
    {
        Destroy(instance.gameObject);
    }

}
