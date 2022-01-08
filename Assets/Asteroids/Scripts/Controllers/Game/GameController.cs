using UnityEngine;


public class GameController : MonoBehaviour
{
    // Ship
    private ShipModel _shipModel;
    private ShipView _shipView;
    private ShipController _shipController;
    private GameObject _shipGameObject;
    private GameObject _shipPrefab;
    private Rigidbody _shipRigidBody;
    private Vector3 _shipStartPosition;

    // Bullet
    private BulletModel _bulletModel;
    private BulletView _bulletView;
    private BulletController _bulletController;
    private GameObject _bulletGameObject;
    private GameObject _bulletPrefab;
    private Rigidbody _bulletRigidBody;
    private Transform _bulletSpawnPosition;

    // Asteroid
    private AsteroidModel _asteroidModel;
    private AsteroidView _asteroidView;
    private AsteroidController _asteroidController;
    private GameObject _asteroidGameObject;
    private GameObject _asteroidPrefab;
    private Rigidbody _asteroidRigidBody;
    private Transform _asteroidSpawnPosition;

    // Game parameters
    private float _leftScreenBorder;
    private float _rightScreenBorder;
    private float _topScreenBorder;
    private float _bottomScreenBorder;

    private void Awake()
    {
        Vector3 screenSize = Camera.main.ViewportToWorldPoint(Vector3.one);
        _rightScreenBorder = screenSize.x;
        _leftScreenBorder = -screenSize.x;
        _topScreenBorder = screenSize.z;
        _bottomScreenBorder = -screenSize.z;
    }

    void Start()
    {
        InitShip();
        InitBullet();
        InitAsteroid();
    }

    void Update()
    {
        ShipUpdate();
        AsteroidUpdate();
        ScreenBorderPosition();
    }

    private void InitShip()
    {
        _shipStartPosition = new Vector3(0, 0, -4.5f);
        _shipPrefab = Resources.Load("PlayerV2") as GameObject;
        _shipPrefab.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
        _shipGameObject = Instantiate(_shipPrefab, _shipStartPosition, Quaternion.identity);
        _shipView = _shipGameObject.GetComponent<ShipView>();
        _shipRigidBody = _shipGameObject.GetComponent<Rigidbody>();
        _shipModel = new ShipModel();
        _shipController = new ShipController(_shipModel, _shipView);
    }

    private void InitBullet()
    {
        _bulletPrefab = Resources.Load("Bullet") as GameObject;
        _bulletModel = new BulletModel();
        _bulletController = new BulletController(_bulletModel, _bulletView);
        _bulletSpawnPosition = FindObjectOfType<BulletSpawnMarker>().transform;
    }

    public void InitAsteroid()
    {
        _asteroidPrefab = Resources.Load("Asteroid") as GameObject;
        _asteroidModel = new AsteroidModel();
        _asteroidController = new AsteroidController(_asteroidModel, _asteroidView);
    }

    private void ShipUpdate()
    {
        if (_shipRigidBody)
        {
            _shipController.MoveWithRigidBody(_shipRigidBody);
            Shoot();
        }
    }

    private void Shoot()
    {
        bool canShoot = Time.time > _bulletModel.NextShoot;

        if (Input.GetKey(KeyCode.Space) && canShoot)
        {
            _bulletModel.NextShoot = Time.time + _bulletModel.ShootDelay;
            _bulletGameObject = Instantiate(_bulletPrefab, _bulletSpawnPosition.position, Quaternion.identity);
            _bulletRigidBody = _bulletGameObject.GetComponent<Rigidbody>();
            _bulletView = _bulletGameObject.GetComponent<BulletView>();
            _bulletController.BulletFly(_bulletRigidBody);
            _bulletView.Die(_bulletModel.LifeTime);
        }
    }

    public void AsteroidUpdate()
    {
        if (Time.time > _asteroidModel.NextSpawn)
        {
            _asteroidGameObject = Instantiate(_asteroidPrefab, new Vector3(Random.Range(_leftScreenBorder, _rightScreenBorder), 0, 8f), Quaternion.identity);
            _asteroidRigidBody = _asteroidGameObject.GetComponent<Rigidbody>();

            if (_asteroidRigidBody)
            {
                _asteroidController.AsteroidMove(_asteroidRigidBody);
            }

            _asteroidModel.NextSpawn += Random.Range(_asteroidModel.MinDelay, _asteroidModel.MaxDelay);
        }
    }

    // Ограничение области полета корабля
    private void ScreenBorderPosition()
    {
        if (_shipGameObject)
        {
            float x = Mathf.Clamp(_shipGameObject.transform.position.x, _leftScreenBorder, _rightScreenBorder);
            float z = Mathf.Clamp(_shipGameObject.transform.position.z, _bottomScreenBorder, _topScreenBorder);

            _shipGameObject.transform.position = new Vector3(x, 0, z);
        }
    }

    // Удаление объектов выходящих за пределы игровой зоны
    private void OnTriggerExit(Collider other)
    {
        Destroy(other.gameObject);
    }
}
