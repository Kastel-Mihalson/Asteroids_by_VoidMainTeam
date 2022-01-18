using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private ShipData _playerShip;
    [SerializeField] private ShipData _enemyShip;
    [SerializeField] private BulletData _playerBullet;
    [SerializeField] private BulletData _enemyBullet;
    [SerializeField] private List<AsteroidData> _asteroidDataList;

    private ShootingController _playerShootingController;
    private ShootingController _enemyShootingController;
    private EnemySpawnController _enemySpawnController;
    private ShipController _playerShipController;
    private ShipController _enemyShipController;

    private void Awake()
    {
        GameModel.SetScreenBorders();
    }

    private void Start()
    {
        _playerShipController = new ShipController(_playerShip);
        _playerShipController.Init();
        _playerShootingController = new ShootingController(_playerShipController.BulletStartPoint, _playerBullet);
        _enemySpawnController = new EnemySpawnController(_asteroidDataList, _enemyShip);
        _enemyShipController = _enemySpawnController.SpawnShip();
        _enemyShootingController = new ShootingController(_enemyShipController.BulletStartPoint, _enemyBullet);
    }

    private void Update()
    {
        _playerShipController.Execute(ShipType.Player);
        _enemyShipController.Execute(ShipType.Enemy);
        _enemySpawnController.SpawnAsteroid();
    }

    private void FixedUpdate()
    {
        _playerShipController.FixedExecute();
        _enemyShipController.FixedExecute();
        _playerShootingController.Shoot();
        _enemyShootingController.Shoot();
    }
}
