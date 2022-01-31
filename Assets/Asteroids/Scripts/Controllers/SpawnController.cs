using System.Collections.Generic;
using UnityEngine;

public sealed class SpawnController
{
    private AsteroidController _asteroidController;
    private PlayerShipController _playerShipController;
    private EnemyShipController _enemyShipController;
    private float _nextSpawnTime;
    private float _currentTime;
    private float _minSpawnDelay = 0.5f;
    private float _maxSpawnDelay = 2f;
    private AudioController _audioController;
    private EffectController _effectController;

    public SpawnController(AudioController audioController, EffectController effectController)
    {
        _audioController = audioController;
        _effectController = effectController;
    }


    public void SpawnAsteroid(AsteroidData asteroids, GameObjectPool asteroidPool, float currentTime)
    {
        if (currentTime > _nextSpawnTime)
        {
            //var asteroidIndex = Random.Range(0, asteroids.Count);

            //_asteroidController = new AsteroidController(
            //    asteroids[asteroidIndex], _audioController, 
            //    _effectController, asteroidPool);

            _asteroidController = new AsteroidController(
                asteroids, _audioController,
                _effectController, asteroidPool);

            _asteroidController.Init();
            _asteroidController.OnDisable();
            _asteroidController.OnEnable();
            _asteroidController.Move();
            _nextSpawnTime += Random.Range(_minSpawnDelay, _maxSpawnDelay);
        }
    }

    public PlayerShipController SpawnPlayerShip(ShipData shipData)
    {
        _playerShipController = new PlayerShipController(shipData, _audioController, _effectController);
        _playerShipController.Init();
        return _playerShipController;
    }

    public EnemyShipController SpawnEnemyShip(ShipData shipData)
    {
        _enemyShipController = new EnemyShipController(shipData, _audioController, _effectController);
        _enemyShipController.Init();
        return _enemyShipController;
    }
}
