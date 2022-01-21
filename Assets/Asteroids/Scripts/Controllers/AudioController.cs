using UnityEngine;

public sealed class AudioController
{
    private const string SOUND = "Sound";
    private static AudioData _audioData;
    private static GameObject _root;

    public AudioController(AudioData audioData)
    {
        _audioData = audioData;
        _root = new GameObject($"[{SOUND}]");
    }

    public static void Play(AudioClipManager audioClip, bool isLoop = false)
    {
        AudioClip clip = _audioData.Sounds.GetAudioClip(audioClip);
        if (clip == null)
        {
            Debug.Log($"{audioClip} is missing in {nameof(AudioData)}.");
            return;
        }

        // TODO pool
        GameObject soundSource = new GameObject(SOUND);
        soundSource.transform.SetParent(_root.transform);
        AudioSource audioSource = soundSource.AddComponent<AudioSource>();

        audioSource.clip = clip;
        audioSource.loop = isLoop;
        audioSource.Play();

        if (!isLoop)
        {
            Object.Destroy(soundSource, clip.length);
        }
    }
}