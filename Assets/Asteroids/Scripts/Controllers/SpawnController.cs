using System.Collections.Generic;
using UnityEngine;

public class SpawnController
{
    private AsteroidController _asteroidController;
    private PlayerShipController _playerShipController;
    private EnemyShipController _enemyShipController;
    private float _nextSpawnTime;
    private float _minSpawnDelay = 0.5f;
    private float _maxSpawnDelay = 2f;

    public void SpawnAsteroid(List<AsteroidData> asteroids)
    {
        if (Time.time > _nextSpawnTime)
        {
            var asteroidIndex = Random.Range(0, asteroids.Count);
            _asteroidController = new AsteroidController(asteroids[asteroidIndex]);
            _asteroidController.Init();
            _asteroidController.Move();
            _nextSpawnTime += Random.Range(_minSpawnDelay, _maxSpawnDelay);
        }
    }

    public PlayerShipController SpawnPlayerShip(ShipData shipData)
    {
        _playerShipController = new PlayerShipController(shipData);
        _playerShipController.Init();
        return _playerShipController;
    }

    public EnemyShipController SpawnEnemyShip(ShipData shipData)
    {
        _enemyShipController = new EnemyShipController(shipData);
        _enemyShipController.Init();
        return _enemyShipController;
    }
}
