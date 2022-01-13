using System.Collections.Generic;
using UnityEngine;


public class GameController : MonoBehaviour
{
    // Ship
    private ShipModel _shipModel;
    private ShipView _shipView;
    private ShipController _shipController;
    private GameObject _shipPrefab;
    private Vector3 _shipStartPosition;

    // Bullet
    private BulletModel _bulletModel;
    private BulletView _bulletView;
    private BulletController _bulletController;
    private GameObject _bulletPrefab;
    private Transform _bulletSpawnPosition;

    // Asteroid
    private AsteroidModel _asteroidModel;
    private AsteroidView _asteroidView;
    private AsteroidController _asteroidController;
    private GameObject _asteroidPrefab;

    // Game parameters
    private float _leftScreenBorder;
    private float _rightScreenBorder;
    private float _topScreenBorder;
    private float _bottomScreenBorder;

    private float _shootingDistance = 8f;
    private LayerMask _enemyMask;

    private Vector3 _movement;

    private void Awake()
    {
        SetScreenBorders();
        _enemyMask = LayerMask.GetMask("Enemy");
    }

    private void Start()
    {
        InitShip();
        InitBullet();
        InitAsteroid();
    }

    private void Update()
    {
        Shoot();
        SpawnAsteroid();
        _movement = GetMovementDirection();
        _shipController.LimitFlightArea(_leftScreenBorder, _rightScreenBorder, _topScreenBorder, _bottomScreenBorder);
    }

    private void FixedUpdate()
    {
        _shipController.MoveWithRigidBody(_movement);
    }

    private Vector3 GetMovementDirection()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");
        Vector3 movement = new Vector3(horizontal, 0, vertical);
        return movement;
    }

    private void SetScreenBorders()
    {
        Vector3 screenSize = Camera.main.ViewportToWorldPoint(Vector3.one);
        _rightScreenBorder = screenSize.x;
        _leftScreenBorder = -screenSize.x;
        _topScreenBorder = screenSize.z;
        _bottomScreenBorder = -screenSize.z;
    }

    private void InitShip()
    {
        _shipStartPosition = new Vector3(0, 0, -4.5f);
        _shipPrefab = Resources.Load("PlayerV2") as GameObject;
        _shipPrefab.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
        GameObject shipGameObject = Instantiate(_shipPrefab, _shipStartPosition, Quaternion.identity);

        _shipModel = new ShipModel();
        _shipView = shipGameObject.GetComponent<ShipView>();
        _shipController = new ShipController(_shipModel, _shipView);
    }

    private void InitBullet()
    {
        _bulletPrefab = Resources.Load("Bullet") as GameObject;
        _bulletModel = new BulletModel();
        _bulletSpawnPosition = FindObjectOfType<BulletSpawnMarker>().transform;
    }

    public void InitAsteroid()
    {
        _asteroidPrefab = Resources.Load("Asteroid") as GameObject;
        _asteroidModel = new AsteroidModel();
    }

    private void Shoot()
    {
        if (!_shipView)
        {
            return;
        }

        bool canShoot = Time.time > _bulletModel.NextShoot;
        bool isEnemyDetected = Physics.Raycast(_shipView.transform.position, Vector3.forward, _shootingDistance, _enemyMask);
        if (canShoot && isEnemyDetected)
        {
            _bulletModel.NextShoot = Time.time + _bulletModel.ShootDelay;

            GameObject bulletGameObject = Instantiate(_bulletPrefab, _bulletSpawnPosition.position, Quaternion.identity);
            _bulletView = bulletGameObject.GetComponent<BulletView>();

            _bulletController = new BulletController(_bulletModel, _bulletView);
            _bulletView.Die(_bulletModel.LifeTime);
            _bulletController.BulletFly();
        }
    }

    public void SpawnAsteroid()
    {
        if (Time.time > _asteroidModel.NextSpawn)
        {
            _asteroidModel.NextSpawn += Random.Range(_asteroidModel.MinDelay, _asteroidModel.MaxDelay);
            
            GameObject asteroidGameObject = Instantiate(_asteroidPrefab, 
                new Vector3(Random.Range(_leftScreenBorder, _rightScreenBorder), 0, 8f), Quaternion.identity);
            
            _asteroidView = asteroidGameObject.GetComponent<AsteroidView>();
            _asteroidController = new AsteroidController(_asteroidModel, _asteroidView);
            _asteroidController.AsteroidMove();
        }
    }

    // Removing objects outside level area
    private void OnTriggerExit(Collider other)
    {
        Destroy(other.gameObject);
    }
}
