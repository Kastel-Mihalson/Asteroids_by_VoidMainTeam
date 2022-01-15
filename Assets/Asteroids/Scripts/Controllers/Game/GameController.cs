using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameData _gameData;

    [SerializeField] private ShipData _playerShip;
    private ShipInitializer _shipInitializer;

    [SerializeField] private AsteroidData _asteroid;
    private AsteroidInitializer _asteroidInitializer;

    [SerializeField] private BulletData _bullet;
    private BulletInitializer _bulletInitializer;

    private GameModel _gameModel;

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

        _asteroidInitializer = new AsteroidInitializer(_asteroid, 
            _gameModel.LeftScreenBorder, _gameModel.RightScreenBorder);
        _bulletInitializer = new BulletInitializer(_bullet);
    }

    private void Update()
    {
        _gameModel.Movement = _shipInitializer.ShipController.GetMovementDirection();
        _shipInitializer.ShipController.LimitFlightArea(
            _gameModel.LeftScreenBorder,
            _gameModel.RightScreenBorder,
            _gameModel.TopScreenBorder,
            _gameModel.BottomScreenBorder);
    }

    private void FixedUpdate()
    {
        Shoot();
        SpawnAsteroid();
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

    private void SpawnAsteroid()
    {
        if (Time.time > _gameModel.NextSpawn)
        {
            _asteroidInitializer.InitAsteroid();
            _gameModel.NextSpawn += Random.Range(_gameModel.MinDelay, _gameModel.MaxDelay);
        }
    }

    private void Shoot()
    {
        if (!_shipInitializer.ShipView)
        {
            return;
        }

        bool canShoot = Time.time > _gameModel.NextShoot;
        bool isEnemyDetected = Physics.Raycast(_shipInitializer.ShipView.transform.position, Vector3.forward, 
            _gameModel.ShootingDistance, _gameModel.EnemyMask);

        if (canShoot && isEnemyDetected)
        {
            _bulletInitializer.InitBullet(_shipInitializer.ShipModel.BulletSpawnPosition.position);
            // Important! Get ShootDelay must be after InitBullet()
            _gameModel.NextShoot = Time.time + _bulletInitializer.BulletModel.ShootDelay;
        }
    }
}
