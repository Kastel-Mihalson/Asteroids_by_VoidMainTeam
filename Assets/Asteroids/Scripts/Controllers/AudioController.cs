using UnityEngine;

public sealed class AudioController
{
    private static AudioData _audioData;

    public AudioController(AudioData audioData)
    {
        _audioData = audioData;
    }

    public static void PlayShot()
    {
        Play(_audioData.EnemyShot);
    }

    public static void PlayAsteroidExplosion()
    {
        Play(_audioData.AsteroidExplosion);
    }

    public static void PlayAsteroidHitting()
    {
        Play(_audioData.AsteroidHitting);
    }

    public static void PlayShipExplosion()
    {
        Play(_audioData.ShipExplosion);
    }

    public static void PlayShipHitting()
    {
        Play(_audioData.ShipHitting);
    }

    private static void Play(AudioClip clip)
    {
        GameObject soundSource = new GameObject();
        AudioSource audioSource = soundSource.AddComponent<AudioSource>();
        audioSource.PlayOneShot(clip);
        Object.Destroy(soundSource, clip.length);
    }
}