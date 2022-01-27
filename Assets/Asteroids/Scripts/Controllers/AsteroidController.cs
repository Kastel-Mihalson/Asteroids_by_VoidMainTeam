using System;
using UnityEngine;
using Random = UnityEngine.Random;

public sealed class AsteroidController
{
    public event Action OnDiedEvent;

    private AsteroidModel _model;
    private AsteroidView _view;
    private AsteroidData _data;
    private Rigidbody _rigidBody;
    private GameObject _prefab;
    private float _sizeMultiplier;
    private float _borderSpawnOffset;
    private int _speedMultiplier;
    private int _healthMultiplier;
    private GameObjectPool _asteroidPool;
    private float _yAxisAsteroidSpawn;

    public AsteroidController(AsteroidData data)
    {
        _data = data;
        _prefab = data.AsteroidPrefab;
        _borderSpawnOffset = 0.5f;
        _sizeMultiplier = 0.5f;
        _speedMultiplier = 3;
        _healthMultiplier = 10;
        _asteroidPool = new GameObjectPool(_prefab);
        _yAxisAsteroidSpawn = 8f;
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

        var xAxisAsteroidSpawn = Random.Range(GameModel.ScreenBorder[Border.Left] + _borderSpawnOffset, GameModel.ScreenBorder[Border.Right] - _borderSpawnOffset);
        asteroidGameObject.transform.position = new Vector3(xAxisAsteroidSpawn, 0, _yAxisAsteroidSpawn);          
       
        var asteroidParamValue = Random.Range(_model.MinSize, _model.MaxSize + 1);
        _model.Size = asteroidParamValue;
        _model.Damage = asteroidParamValue;
        _model.CurrentHP = asteroidParamValue * _healthMultiplier;
        _model.MoveSpeed = Mathf.Abs(asteroidParamValue - (_model.MaxSize + 1)) * _speedMultiplier;
        asteroidGameObject.transform.localScale = Vector3.one * asteroidParamValue * _sizeMultiplier;

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
        _view.OnDamagedEvent += RecieveDamage;
        OnDiedEvent += _view.Die;
        OnDiedEvent += OnDisable;
        _view.ReturnObjectToPoolEvent += AddToQueue;
    }

    public void OnDisable()
    {
        _view.GetAsteroidHealthEvent -= GetHealth;
        _view.GetAsteroidDamageEvent -= GetDamage;
        _view.OnDamagedEvent -= RecieveDamage;
        OnDiedEvent -= _view.Die;
        OnDiedEvent -= OnDisable;
        _view.ReturnObjectToPoolEvent -= AddToQueue;
    }

    private void RecieveDamage(int damage)
    {
        _model.CurrentHP -= damage;

        if (_model.CurrentHP <= 0)
        {
            Die();
        }
    }

    private int? GetHealth()
    {
        return _model.CurrentHP;
    }

    private int? GetDamage()
    {
        return _model.Damage;
    }
    private void Die()
    {
        OnDiedEvent?.Invoke();
    }
}
