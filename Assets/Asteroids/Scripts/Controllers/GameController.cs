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

    private ShootingController _playerShootingController;
    private ShootingController _enemyShootingController;
    private SpawnController _spawnController;
    private ShipController _playerShipController;
    private ShipController _enemyShipController;
    private AudioController _audioController;

    private BackgroundStars _bgStars;
    [Range(1, 10)]
    public float speed = 2f;

    private void Awake()
    {
        GameModel.SetScreenBorders();
    }

    private void Start()
    {
        _bgStars = new BackgroundStars(50);
        _spawnController = new SpawnController();
        _playerShipController = _spawnController.SpawnShip(_playerShip);
        _enemyShipController = _spawnController.SpawnShip(_enemyShip);
        _playerShootingController =
            new ShootingController(_playerShipController.BulletStartPoint, _playerBullet, _playerShip.ShootingLayer);
        _enemyShootingController =
            new ShootingController(_enemyShipController.BulletStartPoint, _enemyBullet, _enemyShip.ShootingLayer);
        _audioController = new AudioController(_audioData);
    }

    private void Update()
    {   
        _playerShipController.Execute();
        _enemyShipController.Execute();
        _spawnController.SpawnAsteroid(_asteroidDataList);
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
