using System.Collections.Generic;
using UnityEngine;

public class SpawnController
{
    private AsteroidController _asteroidController;
    private ShipController _shipController;
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
            _asteroidController.OnDisable();
            _asteroidController.OnEnable();
            _asteroidController.Move();
            _nextSpawnTime += Random.Range(_minSpawnDelay, _maxSpawnDelay);
        }
    }

    public ShipController SpawnShip(ShipData shipData, PlayerHUDView viewHUD)
    {
        _shipController = new ShipController(shipData, viewHUD);
        _shipController.Init(shipData.Type);
        return _shipController;
    }
}
