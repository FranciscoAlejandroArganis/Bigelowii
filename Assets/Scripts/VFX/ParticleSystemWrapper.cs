using UnityEngine;

/// <summary>
/// Envoltura de un sistema de partículas
/// </summary>
public class ParticleSystemWrapper : Wrapper
{

    public AudioSource audioSource;

    /// <summary>
    /// Unidad que genera el sistema de partículas
    /// </summary>
    public Unit unit;

    /// <summary>
    /// Sistema de partículas
    /// </summary>
    private ParticleSystem system;

    public void Awake()
    {
        unit = GetComponentInParent<Unit>();
        system = GetComponent<ParticleSystem>();
    }

    /// <summary>
    /// Empieza el sistema de partículas
    /// </summary>
    public override void Play()
    {
        system.Play();
        audioSource.Play();
    }

    public void OnParticleSystemStopped()
    {
        unit.actionController.action.Execute();
    }

    public override void Stop()
    {
        system.Stop();
        audioSource.Stop();
    }

}
