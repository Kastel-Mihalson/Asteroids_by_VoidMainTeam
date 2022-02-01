using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private ShipData _playerShip;
    [SerializeField] private ShipData _enemyShip;
    [SerializeField] private BulletData _playerBullet;
    [SerializeField] private BulletData _enemyBullet;
    [SerializeField] private GameData _gameData;

    private ShootingController _playerShootingController;
    private ShootingController _enemyShootingController;
    private SpawnController _spawnController;
    private PlayerShipController _playerShipController;
    private EnemyShipController _enemyShipController;
    private AudioController _audioController;
    private EffectController _effectController;
    private EndGameMenuController _endGameMenuController;

    private GameObjectPool _asteroidPool;
    private BackgroundStars _bgStars;
    private float _currentTime;
    private float speed = 2f;

    private void Awake()
    {
        GameModel.SetScreenBorders();
    }

    private void Start()
    {
        Time.timeScale = 1;

        _asteroidPool = new GameObjectPool("Asteroids");
        _bgStars = new BackgroundStars(50);
        _audioController = new AudioController(_gameData.AudioData, _gameData.AudioMixerGroup);
        _effectController = new EffectController(_gameData.EffectData);

        _spawnController = new SpawnController(_audioController, _effectController);

        _playerShipController = _spawnController.SpawnPlayerShip(_playerShip);
        _enemyShipController = _spawnController.SpawnEnemyShip(_enemyShip);

        if (_gameData.GameMode == GameModeManager.Singleplayer)
        {
            // TODO one player
        }
        else if (_gameData.GameMode == GameModeManager.Multiplayer)
        {
            // TODO two players
        }

        _playerShootingController = new ShootingController(
            _playerShipController.BulletStartPoint, _playerBullet, _playerShip.ShootingLayer, _audioController);
        _enemyShootingController = new ShootingController(
            _enemyShipController.BulletStartPoint, _enemyBullet,  _enemyShip.ShootingLayer, _audioController);

        _audioController.Play(AudioClipManager.BackgroundMusic, true);

        _endGameMenuController = new EndGameMenuController(_audioController);
        _endGameMenuController.OnEnable();
    }

    private void Update()
    {
        var asteroid = SelectAsteroid(_gameData.LevelData.AsteroidDataList);
        _currentTime += Time.deltaTime;

        _playerShipController.Execute();
        _enemyShipController.Execute();

        _spawnController.SpawnAsteroid(asteroid, _asteroidPool, _currentTime);
    }

    private void FixedUpdate()
    {
        _bgStars.MoveStars(speed);
        _playerShipController.FixedExecute();
        _enemyShipController.FixedExecute();
        _playerShootingController.Shoot();
        _enemyShootingController.Shoot();
    }

    // TODO implement in other class
    private AsteroidData SelectAsteroid(List<AsteroidData> asteroids)
    {
        var asteroidIndex = Random.Range(0, asteroids.Count);
        _asteroidPool.SetGameObject = asteroids[asteroidIndex].AsteroidPrefab;

        return asteroids[asteroidIndex];
    }
}
