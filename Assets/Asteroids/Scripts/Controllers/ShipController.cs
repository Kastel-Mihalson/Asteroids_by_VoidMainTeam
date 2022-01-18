using System;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public sealed class ShipController
{
    public event Action OnDiedEvent;

    private ShipModel _model;
    private ShipView _view;
    private ShipData _data;
    private Rigidbody _rigidBody;
    private Transform _transform;
    private GameObject _prefab;
    private Vector3 _startPosition;
    private Vector3 _movement;
    private float _nextChangeDirectionTime;
    private Transform _bulletStartPoint;
    private float _offsetX;
    private float _offsetZ;
    private float _leftMovementLimit;
    private float _rightMovementLimit;
    private float _topMovementLimit;
    private float _bottomMovementLimit;

    public Transform BulletStartPoint => _bulletStartPoint;

    public ShipController(ShipData data)
    {
        _data = data;
        _startPosition = data.StartPosition;
        _prefab = data.ShipPrefab;      
    }

    public void Init()
    {
        _model = new ShipModel(_data);
        GameObject shipGameObject = Object.Instantiate(_prefab, _startPosition, Quaternion.identity);
        _view = shipGameObject.GetComponent<ShipView>();
        _rigidBody = _view.Rigidbody;
        _transform = _rigidBody.transform;
        _bulletStartPoint = _view.BulletSpawnPoint;

        var collider = _transform.GetComponent<CapsuleCollider>();
        _offsetX = collider.radius;
        _offsetZ = collider.height / 2;
        _leftMovementLimit = GameModel.ScreenBorder[Border.Left] + _offsetX;
        _rightMovementLimit = GameModel.ScreenBorder[Border.Right] - _offsetX;
        _topMovementLimit = GameModel.ScreenBorder[Border.Top] - _offsetZ;
        _bottomMovementLimit = GameModel.ScreenBorder[Border.Bottom] + _offsetZ;

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
        if (_model.CurrentArmor > 0)
        {
            _model.CurrentArmor -= damage;
        }
        else
        {
            _model.CurrentHP -= damage;
        }

        if (_model.CurrentHP <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        OnDiedEvent?.Invoke();
    }

    public void Execute(ShipType type)
    {
        if (type == ShipType.Player)
        {
            _movement = GetMovementDirection();
        }
        else if (type == ShipType.Enemy && Time.time > _nextChangeDirectionTime)
        {
            _movement = GetRandomHorizontalMovementDirection();
            _nextChangeDirectionTime += Random.Range(1f, 5f);
        }        

        LimitFlightArea();
    }

    public void FixedExecute()
    {
        Move(_movement);
    }

    private Vector3 GetMovementDirection()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        return new Vector3(horizontal, 0, vertical);
    }

    private Vector3 GetRandomHorizontalMovementDirection()
    {
        return new Vector3(Random.Range(-1f, 1f), 0f, 0f);
    }

    private void Move(Vector3 movement)
    {
        if (_rigidBody)
        {        
            _rigidBody.velocity = movement * _model.MoveSpeed;
            _rigidBody.rotation = Quaternion.Euler(0, 0, -_rigidBody.velocity.x * _model.TurnSpeed);
        }
    }

    public void LimitFlightArea()
    {
        if (_transform)
        {
            float x = Mathf.Clamp(_transform.position.x, _leftMovementLimit, _rightMovementLimit);
            float z = Mathf.Clamp(_transform.position.z, _bottomMovementLimit, _topMovementLimit);
            _transform.position = new Vector3(x, 0, z);
        }
    }
}