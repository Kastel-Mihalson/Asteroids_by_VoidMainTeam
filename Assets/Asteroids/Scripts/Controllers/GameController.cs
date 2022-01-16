using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameData _gameData;
    [SerializeField] private ShipData _playerShip;
    [SerializeField] private BulletData _bullet;
    [SerializeField] private List<AsteroidData> _asteroidDataList;

    private GameModel _gameModel;
    private ShootingController _shootingController;
    private EnemySpawnController _enemySpawnController;
    private ShipController _shipController;

    private Transform _bulletStartPoint;

    private void Awake()
    {
        _gameModel = new GameModel(_gameData);
        //ResourcesManager.LoadPrefabsByNameList(_gameModel.StartLoadedPrefabNames);
        SetScreenBorders();
    }

    private void Start()
    {
        _shipController = new ShipController(_playerShip, _gameModel);
        _shipController.Init();

        // TODO remove from there
        var spawnObject = FindObjectOfType<BulletSpawnMarker>().transform;
        if (spawnObject != null)
        {
            _bulletStartPoint = spawnObject;
        }
        //

        _shootingController = new ShootingController(_bulletStartPoint, _bullet);
        _enemySpawnController = new EnemySpawnController(_asteroidDataList, _gameModel);
    }

    private void Update()
    {
        _shipController.Execute();
        _enemySpawnController.SpawnAsteroid();
    }

    private void FixedUpdate()
    {
        _shipController.FixedExecute();
        _shootingController.Shoot();
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
