using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnController
{
    private AsteroidController _asteroidController;
    private List<AsteroidData> _asteroids;
    private GameModel _gameModel;
    private float _nextSpawnTime;
    private float _minSpawnDelay = 0.5f;
    private float _maxSpawnDelay = 2f;

    public EnemySpawnController(List<AsteroidData> asteroids, GameModel gameModel)
    {
        _asteroids = asteroids;
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
}
