using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private ShipData _playerShip;
    [SerializeField] private ShipData _enemyShip;
    [SerializeField] private BulletData _playerBullet;
    [SerializeField] private BulletData _enemyBullet;
    [SerializeField] private List<AsteroidData> _asteroidDataList;
    [SerializeField] private AudioData _audioData;
    [SerializeField] private EffectData _effectData;

    private ShootingController _playerShootingController;
    private ShootingController _enemyShootingController;
    private SpawnController _spawnController;
    private PlayerShipController _playerShipController;
    private EnemyShipController _enemyShipController;
    private AudioController _audioController;
    private EffectController _effectController;
    private EndGameMenuController _endGameMenuController;
    private GodMode _godMode;

    private BackgroundStars _bgStars;
    [Range(1, 10)]
    public float speed = 2f;

    private void Awake()
    {
        GameModel.SetScreenBorders();
    }

    private void Start()
    {
        Time.timeScale = 1;
        _bgStars = new BackgroundStars(50);
        _audioController = new AudioController(_audioData);
        _effectController = new EffectController(_effectData);

        _spawnController = new SpawnController(_audioController, _effectController);

        _playerShipController = _spawnController.SpawnPlayerShip(_playerShip);
        _enemyShipController = _spawnController.SpawnEnemyShip(_enemyShip);

        _playerShootingController = new ShootingController(
            _playerShipController.BulletStartPoint, _playerBullet, _playerShip.ShootingLayer, _audioController);
        _enemyShootingController = new ShootingController(
            _enemyShipController.BulletStartPoint, _enemyBullet,  _enemyShip.ShootingLayer, _audioController);

        _audioController.Play(AudioClipManager.BackgroundMusic, true);

        _godMode = new GodMode();

        _playerShipController.ChitsInit(_godMode);

        _endGameMenuController = new EndGameMenuController(_audioController);
        _endGameMenuController.OnEnable();
    }

    private void Update()
    {   
        _playerShipController.Execute();
        _enemyShipController.Execute();
        _spawnController.SpawnAsteroid(_asteroidDataList);
        _godMode.Execute();
    }

    private void FixedUpdate()
    {
        _bgStars.MoveStars(speed);
        _playerShipController.FixedExecute();
        _enemyShipController.FixedExecute();
        _playerShootingController.Shoot();
        _enemyShootingController.Shoot();
    }
}
