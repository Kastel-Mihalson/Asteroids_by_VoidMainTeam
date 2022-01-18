using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnController
{
    private AsteroidController _asteroidController;
    private ShipController _shipController;
    private List<AsteroidData> _asteroids;
    private ShipData _shipData;
    private GameModel _gameModel;
    private float _nextSpawnTime;
    private float _minSpawnDelay = 0.5f;
    private float _maxSpawnDelay = 2f;

    public EnemySpawnController(List<AsteroidData> asteroids, ShipData shipData, GameModel gameModel)
    {
        _asteroids = asteroids;
        _shipData = shipData;
        _gameModel = gameModel;
    }

    public void SpawnAsteroid()
    {
        if (Time.time > _nextSpawnTime)
        {
            var asteroidIndex = Random.Range(0, _asteroids.Count);
            _asteroidController = new AsteroidController(_asteroids[asteroidIndex], _gameModel);
            _asteroidController.Init();
            _asteroidController.Move();
            _nextSpawnTime += Random.Range(_minSpawnDelay, _maxSpawnDelay);
        }
    }

    public ShipController SpawnShip()
    {
        _shipController = new ShipController(_shipData, _gameModel);
        _shipController.Init();
        return _shipController;
    }
}
