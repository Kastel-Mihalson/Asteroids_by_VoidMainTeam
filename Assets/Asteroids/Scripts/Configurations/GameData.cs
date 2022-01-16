using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "ScriptableObjects/GameData", order = 1)]
public class GameData : ScriptableObject
{
    [SerializeField] private float _nextSpawnTime = 0f;
    [SerializeField] private float _minSpawnDelay = 0.5f;
    [SerializeField] private float _maxSpawnDelay = 2f;

    [SerializeField]
    private List<string> _startLoadedPrefabNames = new List<string>
    {
        "PlayerV2",
        "Bullet",
        "Asteroid"
    };

    public float NextSpawnTime => _nextSpawnTime;
    public float MinSpawnDelay => _minSpawnDelay;
    public float MaxSpawnDelay => _maxSpawnDelay;
    public List<string> StartLoadedPrefabNames => _startLoadedPrefabNames;
}
