using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "GameData", menuName = "ScriptableObjects/GameData", order = 1)]
public sealed class GameData : ScriptableObject
{
    [SerializeField] private GameModeManager _gameMode;
    [SerializeField] private LevelData _levelData;
    [SerializeField] private AudioMixerGroup _audioMixerGroup;
    [SerializeField] private AudioData _audioData;
    [SerializeField] private EffectData _effectData;

    public GameModeManager GameMode
    {
        get => _gameMode;
        set => _gameMode = value;
    }
    public LevelData LevelData => _levelData;
    public AudioMixerGroup AudioMixerGroup => _audioMixerGroup;
    public AudioData AudioData => _audioData;
    public EffectData EffectData => _effectData;
}
