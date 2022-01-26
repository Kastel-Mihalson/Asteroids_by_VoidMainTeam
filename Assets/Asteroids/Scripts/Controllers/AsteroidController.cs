using UnityEngine;
using Random = UnityEngine.Random;

public sealed class AsteroidController
{
    private AsteroidModel _model;
    private AsteroidView _view;
    private AsteroidData _data;
    private Rigidbody _rigidBody;
    private GameObject _prefab;
    private GameObjectPool _asteroidPool;
    private float _borderSpawnOffset;
    private float _ySpawnPosition;

    public AsteroidController(AsteroidData data)
    {
        _data = data;
        _prefab = data.AsteroidPrefab;
        _asteroidPool = new GameObjectPool(_prefab);
        _borderSpawnOffset = 0.5f;
        _ySpawnPosition = 8f;
    }

    public void Move()
    {
        if (_rigidBody)
        {
            _rigidBody.angularVelocity = Random.insideUnitSphere * _model.RotationSpeed;
            _rigidBody.velocity = Vector3.back * _model.MoveSpeed;
        }
    }

    public void Init()
    {
        _model = new AsteroidModel(_data);

        GameObject asteroidGameObject = _asteroidPool.GetGameObject();

        var xSpawnPosition = Random.Range(GameModel.ScreenBorder[Border.Left] + _borderSpawnOffset, GameModel.ScreenBorder[Border.Right] - _borderSpawnOffset);
        asteroidGameObject.transform.position = new Vector3(xSpawnPosition, 0, _ySpawnPosition);    
        asteroidGameObject.transform.localScale = Vector3.one * _model.Size;

        _view = asteroidGameObject.GetComponent<AsteroidView>();
        _rigidBody = _view.Rigidbody;
        _view.Die(_model.LifeTime);
    }

    public void AddToQueue(GameObject asteroid)
    {
        _asteroidPool.AddToQueue(asteroid);
    }


    public void OnEnable()
    {
        _view.GetAsteroidHealthEvent += GetHealth;
        _view.GetAsteroidDamageEvent += GetDamage;
        _view.OnDamagedEvent += _model.RecieveDamage;
        _model.OnDiedEvent += _view.Die;
        _model.OnDiedEvent += OnDisable;
        _view.ReturnObjectToPoolEvent += AddToQueue;
    }

    public void OnDisable()
    {
        _view.GetAsteroidHealthEvent -= GetHealth;
        _view.GetAsteroidDamageEvent -= GetDamage;
        _view.OnDamagedEvent -= _model.RecieveDamage;
        _model.OnDiedEvent -= _view.Die;
        _model.OnDiedEvent -= OnDisable;
        _view.ReturnObjectToPoolEvent -= AddToQueue;
    }

    private int? GetHealth() => _model.CurrentHP;

    private int? GetDamage()=> _model.Damage;
}
