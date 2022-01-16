using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameData _gameData;
    [SerializeField] private ShipData _playerShip;
    [SerializeField] private BulletData _bullet;
    [SerializeField] private List<AsteroidData> _asteroidDataList;

    private ShipInitializer _shipInitializer;
    private GameModel _gameModel;
    private ShootingController _shootingController;
    private EnemySpawnController _enemySpawnController;

    private void Awake()
    {
        _gameModel = new GameModel(_gameData);
        //ResourcesManager.LoadPrefabsByNameList(_gameModel.StartLoadedPrefabNames);
        SetScreenBorders();
    }

    private void Start()
    {
        _shipInitializer = new ShipInitializer(_playerShip);
        _shipInitializer.InitShip();
        _shootingController = new ShootingController(_shipInitializer.ShipModel.BulletSpawnPoint, _bullet);
        _enemySpawnController = new EnemySpawnController(_gameModel.LeftScreenBorder, _gameModel.RightScreenBorder);
    }

    private void Update()
    {
        _gameModel.Movement = _shipInitializer.ShipController.GetMovementDirection();
        _shipInitializer.ShipController.LimitFlightArea(
            _gameModel.LeftScreenBorder,
            _gameModel.RightScreenBorder,
            _gameModel.TopScreenBorder,
            _gameModel.BottomScreenBorder);

        var asteroidIndex = Random.Range(0, _asteroidDataList.Count); // every frame :(
        _enemySpawnController.SpawnAsteroid(_asteroidDataList[asteroidIndex]);
    }

    private void FixedUpdate()
    {
        _shootingController.Shoot();
        _shipInitializer.ShipController.MoveWithRigidBody(_gameModel.Movement);
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
