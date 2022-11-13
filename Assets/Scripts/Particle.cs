using UnityEngine;

/// <summary>
/// Envoltura de un sistema de partículas
/// </summary>
public class Particle : MonoBehaviour
{

    /// <summary>
    /// Unidad que genera el sistema de partículas
    /// </summary>
    private Unit unit;

    /// <summary>
    /// Sistema de partículas
    /// </summary>
    private ParticleSystem system;

    public void Start()
    {
        unit = GetComponentInParent<Unit>();
        system = GetComponent<ParticleSystem>();
    }

    /// <summary>
    /// Empieza el sistema de partículas
    /// </summary>
    public void Play()
    {
        system.Play();
    }

    public void OnParticleSystemStopped()
    {
        unit.actionController.action.Execute();
    }

}
