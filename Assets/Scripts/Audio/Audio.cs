using UnityEngine;

/// <summary>
/// Componente encargado de manejar el audio
/// </summary>
public class Audio
{
    
    /// <summary>
    /// Conjunto de sonidos
    /// </summary>
    public static SoundsSet sounds;

    /// <summary>
    /// Reproduce el audio especificado, en la posición de la cámara
    /// </summary>
    /// <param name="clip">El audio que se reproduce</param>
    public static void PlayClip(AudioClip clip)
    {
        AudioSource.PlayClipAtPoint(clip, CameraController.instance.transform.position);
    }

}
