using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "ScriptableObjects/GameData", order = 1)]
public class GameData : ScriptableObject
{
    [SerializeField] private float _shootingDistance = 7f;
    [SerializeField] private float _nextSpawn = 0f;
    [SerializeField] private float _minDelay = 0.5f;
    [SerializeField] private float _maxDelay = 2f;
    [SerializeField] private float _nextShoot = 0f;

    [SerializeField]
    private List<string> _startLoadedPrefabNames = new List<string>
    {
        "PlayerV2",
        "Bullet",
        "Asteroid"
    };

    [SerializeField] private LayerMask _enemyMask;
    public float ShootingDistance => _shootingDistance;
    public float NextSpawn => _nextSpawn;
    public float MinDelay => _minDelay;
    public float MaxDelay => _maxDelay;
    public float NextShoot => _nextShoot;

    public List<string> StartLoadedPrefabNames => _startLoadedPrefabNames;

    public LayerMask EnemyMask => _enemyMask;
}
