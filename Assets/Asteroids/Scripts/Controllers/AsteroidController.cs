using System;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public sealed class AsteroidController
{
    public event Action OnDiedEvent;

    private AsteroidModel _model;
    private AsteroidView _view;
    private AsteroidData _data;
    private Rigidbody _rigidBody;
    private GameObject _prefab;

    public AsteroidController(AsteroidData data)
    {
        _data = data;
        _prefab = data.AsteroidPrefab;
    }

    public void Move()
    {
        if (_rigidBody)
        {
            _rigidBody.angularVelocity = Random.insideUnitSphere * _model.RotationSpeed;
            _rigidBody.velocity = Vector3.back * Mathf.Abs((_model.Size - (_model.MaxSize)) * 2);
        }
    }

    public void Init()
    {
        _model = new AsteroidModel(_data);
        GameObject asteroidGameObject = Object.Instantiate(_prefab,
            new Vector3(Random.Range(GameModel.ScreenBorder[Border.Left], GameModel.ScreenBorder[Border.Right]), 0, 8f), 
            Quaternion.identity);
        _model.Size = Random.Range(_model.MinSize, _model.MaxSize+1f);
        _model.Damage = (int)_model.Size;
        _model.CurrentHP = (int)_model.Size * 10;
        asteroidGameObject.transform.localScale = Vector3.one * (_model.Size*0.5f);
        _view = asteroidGameObject.GetComponent<AsteroidView>();
        _rigidBody = _view.Rigidbody;
        _view.Die(_model.LifeTime);

        OnEnable();
    }

    private void OnEnable()
    {
        _view.OnDamagedEvent += RecieveDamage;
        OnDiedEvent += _view.Die;
        OnDiedEvent += OnDisable;
    }

    private void OnDisable()
    {
        _view.OnDamagedEvent -= RecieveDamage;
        OnDiedEvent -= _view.Die;
        OnDiedEvent -= OnDisable;
    }

    private void RecieveDamage(int damage)
    {
        _model.CurrentHP -= damage;

        if (_model.CurrentHP <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        OnDiedEvent?.Invoke();
    }
}
