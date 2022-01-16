using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnController
{
    private AsteroidController _asteroidController;
    private List<AsteroidData> _asteroids;
    private float _nextSpawnTime;
    private float _minSpawnDelay = 0.5f;
    private float _maxSpawnDelay = 2f;
    private float _leftScreenBorder;
    private float _rightScreenBorder;

    public EnemySpawnController(List<AsteroidData> asteroids, GameModel gameModel)
    {
        _leftScreenBorder = gameModel.LeftScreenBorder;
        _rightScreenBorder = gameModel.RightScreenBorder;
        _asteroids = asteroids;
    }

    public void SpawnAsteroid()
    {
        if (Time.time > _nextSpawnTime)
        {
            var asteroidIndex = Random.Range(0, _asteroids.Count);
            _asteroidController = new AsteroidController(_asteroids[asteroidIndex], _leftScreenBorder, _rightScreenBorder);
            _asteroidController.Init();
            _asteroidController.Move();
            _nextSpawnTime += Random.Range(_minSpawnDelay, _maxSpawnDelay);
        }
    }
}
