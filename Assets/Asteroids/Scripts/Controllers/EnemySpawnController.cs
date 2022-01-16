using UnityEngine;

public class EnemySpawnController
{
    private AsteroidController _asteroidController;
    private float _nextSpawnTime;
    private float _minSpawnDelay = 0.5f;
    private float _maxSpawnDelay = 2f;
    private float _leftScreenBorder;
    private float _rightScreenBorder;

    public EnemySpawnController(float leftScreenBorder, float rightScreenBorder)
    {
        _leftScreenBorder = leftScreenBorder;
        _rightScreenBorder = rightScreenBorder;
    }

    public void SpawnAsteroid(AsteroidData asteroidData)
    {
        if (Time.time > _nextSpawnTime)
        {
            _asteroidController = new AsteroidController(asteroidData, _leftScreenBorder, _rightScreenBorder);
            _asteroidController.Init();
            _asteroidController.Move();
            _nextSpawnTime += Random.Range(_minSpawnDelay, _maxSpawnDelay);
        }
    }
}
