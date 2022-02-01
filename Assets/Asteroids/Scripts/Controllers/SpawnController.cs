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
    private Vector3 _firstPlayerStartPosition;
    private Vector3 _secondPlayerStartPosition;
    private Vector3 _enemyShipStartPosition;
    private float _bottomOffset = 2f;
    private float _topOffset = 2f;
    private float _leftOffset = 4f;
    private float _rightOffset = 4f;

    public SpawnController(GameModeManager gameMode, AudioController audioController, EffectController effectController)
    {
        _audioController = audioController;
        _effectController = effectController;

        float bottomPosition = GameModel.ScreenBorder[Border.Bottom];
        float topPosition = GameModel.ScreenBorder[Border.Top];
        float leftPosition = GameModel.ScreenBorder[Border.Left];
        float rightPosition = GameModel.ScreenBorder[Border.Right];

        if (gameMode == GameModeManager.Singleplayer)
        {
            _firstPlayerStartPosition = new Vector3(0, 0, bottomPosition + _bottomOffset);
        }
        else if (gameMode == GameModeManager.Multiplayer)
        {
            _firstPlayerStartPosition = new Vector3(leftPosition + _leftOffset, 0, bottomPosition + _bottomOffset);
            _secondPlayerStartPosition = new Vector3(rightPosition - _rightOffset, 0, bottomPosition + _bottomOffset);
        }

        _enemyShipStartPosition = new Vector3(0, 0, topPosition - _topOffset);
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

    public PlayerShipController SpawnPlayerShip(ShipData shipData, PlayerHUDView hudView,
        PlayerManager player = PlayerManager.First)
    {
        Vector3 playerSpawnPosition;

        if (player == PlayerManager.First)
        {
            playerSpawnPosition = _firstPlayerStartPosition;
        }
        else
        {
            playerSpawnPosition = _secondPlayerStartPosition;
        }

        _playerShipController = new PlayerShipController(shipData, player, _audioController, _effectController, hudView);
        _playerShipController.Init(playerSpawnPosition);
        return _playerShipController;
    }

    public EnemyShipController SpawnEnemyShip(ShipData shipData)
    {
        _enemyShipController = new EnemyShipController(shipData, _audioController, _effectController);
        _enemyShipController.Init(_enemyShipStartPosition);
        return _enemyShipController;
    }
}
