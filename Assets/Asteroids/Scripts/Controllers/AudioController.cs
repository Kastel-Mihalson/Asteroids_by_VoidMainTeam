using UnityEngine;
using System.Collections.Generic;

public sealed class AudioController
{
    private const string SOUND = "Sound";
    //private const string AUDIO_DATA = "AudioData";
    private AudioData _audioData;
    private GameObject _root;
    private List<GameObject> _soundSource;

    public AudioController(AudioData data)
    {
        _audioData = data;
        //_audioData = Resources.Load<AudioData>(AUDIO_DATA);
        _root = new GameObject($"[{SOUND}]");
        _soundSource = new List<GameObject>();
    }

    public void Play(AudioClipManager clipType, bool isLoop = false)
    {
        AudioClip clip = _audioData.Sounds.GetAudioClip(clipType);
        if (clip == null)
        {
            Debug.Log($"{clipType} is missing in {nameof(AudioData)}.");
            return;
        }

        Play(clip, isLoop);
    }

    private void Play(AudioClip clip, bool isLoop)
    {
        // TODO pool
        GameObject soundSource = new GameObject(SOUND);
        soundSource.transform.SetParent(_root.transform);
        AudioSource audioSource = soundSource.AddComponent<AudioSource>();
        _soundSource.Add(soundSource);

        audioSource.clip = clip;
        audioSource.loop = isLoop;
        audioSource.Play();

        if (!isLoop)
        {
            _soundSource.Remove(soundSource);
            Object.Destroy(soundSource, clip.length);
        }
    }

    public void Clear()
    {        
        foreach (var source in _soundSource)
        {
            Object.Destroy(source);
        }
    }
}