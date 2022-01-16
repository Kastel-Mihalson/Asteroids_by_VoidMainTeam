using UnityEngine;

public class EnemySpawnController
{
    private AsteroidInitializer _asteroidInitializer;
    private float _nextSpawnTime;
    private float _minSpawnDelay = 0.5f;
    private float _maxSpawnDelay = 2f;

    public EnemySpawnController(float leftScreenBorder, float rightScreenBorder)
    {
        _asteroidInitializer = new AsteroidInitializer(leftScreenBorder, rightScreenBorder);
    }

    public void SpawnAsteroid(AsteroidData asteroidData)
    {
        if (Time.time > _nextSpawnTime)
        {
            _asteroidInitializer.InitAsteroid(asteroidData);
            _nextSpawnTime += Random.Range(_minSpawnDelay, _maxSpawnDelay);
        }
    }
}
