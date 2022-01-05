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
    private float _leftScreenBorder = -9.5f;
    private float _rightScreenBorder = 9.5f;
    private float _topScreenBorder = 1f;
    private float _bottomScreenBorder = -5f;

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
        _shipPrefab = Resources.Load("Player") as GameObject;
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
        var bulletModel = _bulletController._model;
        bool canShoot = Time.time > bulletModel.nextShoot;

        if (Input.GetKey(KeyCode.Space) && canShoot)
        {
            _bulletController._model.nextShoot = Time.time + bulletModel.shootDelay;
            _bulletGameObject = Instantiate(_bulletPrefab, _bulletSpawnPosition.position, Quaternion.identity);
            _bulletRigidBody = _bulletGameObject.GetComponent<Rigidbody>();
            _bulletController.BulletFly(_bulletRigidBody);
        }
    }

    public void AsteroidUpdate()
    {
        var asteroidModel = _asteroidController._model;

        if (Time.time > asteroidModel.nextSpawn)
        {
            _asteroidGameObject = Instantiate(_asteroidPrefab, new Vector3(Random.Range(-9.5f, 9.5f), 0, 8f), Quaternion.identity);
            _asteroidRigidBody = _asteroidGameObject.GetComponent<Rigidbody>();

            if (_asteroidRigidBody)
            {
                _asteroidController.AsteroidMove(_asteroidRigidBody);
            }

            asteroidModel.nextSpawn += Random.Range(asteroidModel.minDelay, asteroidModel.maxDelay);
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
