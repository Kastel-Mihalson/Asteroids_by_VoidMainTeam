using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameData _gameData;
    [SerializeField] private ShipData _playerShip;
    [SerializeField] private ShipData _enemyShip;
    [SerializeField] private BulletData _playerBullet;
    [SerializeField] private BulletData _enemyBullet;
    [SerializeField] private List<AsteroidData> _asteroidDataList;

    private GameModel _gameModel;
    private ShootingController _playerShootingController;
    private ShootingController _enemyShootingController;
    private EnemySpawnController _enemySpawnController;
    private ShipController _playerShipController;
    private ShipController _enemyShipController;

    private void Awake()
    {
        _gameModel = new GameModel(_gameData);
        //ResourcesManager.LoadPrefabsByNameList(_gameModel.StartLoadedPrefabNames);
        SetScreenBorders();
    }

    private void Start()
    {
        _playerShipController = new ShipController(_playerShip, _gameModel);
        _playerShipController.Init();
        _playerShootingController = new ShootingController(_playerShipController.BulletStartPoint, _playerBullet);
        _enemySpawnController = new EnemySpawnController(_asteroidDataList, _enemyShip, _gameModel);
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

    private void SetScreenBorders()
    {
        Vector3 screenSize = Camera.main.ViewportToWorldPoint(Vector3.one);
        _gameModel.RightScreenBorder = screenSize.x;
        _gameModel.LeftScreenBorder = -screenSize.x;
        _gameModel.TopScreenBorder = screenSize.z;
        _gameModel.BottomScreenBorder = -screenSize.z;
    }
}
