using UnityEngine;

/// <summary>
/// Envoltura de un sistema de part�culas
/// </summary>
public class Particle : MonoBehaviour
{

    /// <summary>
    /// Unidad que genera el sistema de part�culas
    /// </summary>
    private Unit unit;

    /// <summary>
    /// Sistema de part�culas
    /// </summary>
    private ParticleSystem system;

    public void Start()
    {
        unit = GetComponentInParent<Unit>();
        system = GetComponent<ParticleSystem>();
    }

    /// <summary>
    /// Empieza el sistema de part�culas
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
