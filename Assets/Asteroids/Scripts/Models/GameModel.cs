using System.Collections.Generic;
using UnityEngine;

public sealed class GameModel
{
    private float _leftScreenBorder;
    private float _rightScreenBorder;
    private float _topScreenBorder;
    private float _bottomScreenBorder;
    private float _shootingDistance;
    private float _nextSpawn;
    private float _minDelay;
    private float _maxDelay;
    private float _nextShoot;

    private List<string> _startLoadedPrefabNames;

    private LayerMask _enemyMask;
    private Vector3 _movement;

    public float LeftScreenBorder
    {
        get => _leftScreenBorder;
        set => _leftScreenBorder = value;
    }
    public float RightScreenBorder
    {
        get => _rightScreenBorder;
        set => _rightScreenBorder = value;
    }
    public float TopScreenBorder
    {
        get => _topScreenBorder;
        set => _topScreenBorder = value;
    }
    public float BottomScreenBorder
    {
        get => _bottomScreenBorder;
        set => _bottomScreenBorder = value;
    }
    public float ShootingDistance => _shootingDistance;
    public float NextSpawn
    {
        get => _nextSpawn;
        set => _nextSpawn = value;
    }
    public float MinDelay => _minDelay;
    public float MaxDelay => _maxDelay;
    public float NextShoot
    {
        get => _nextShoot;
        set => _nextShoot = value;
    }

    public List<string> StartLoadedPrefabNames => _startLoadedPrefabNames;

    public LayerMask EnemyMask => _enemyMask;
    public Vector3 Movement
    {
        get => _movement;
        set => _movement = value;
    }

    public GameModel(GameData gameData)
    {
        _nextShoot = gameData.NextShoot;
        _nextSpawn = gameData.NextSpawn;
        _shootingDistance = gameData.ShootingDistance;
        _minDelay = gameData.MinDelay;
        _maxDelay = gameData.MaxDelay;
        _startLoadedPrefabNames = gameData.StartLoadedPrefabNames;
        _enemyMask = gameData.EnemyMask;
    }
}
