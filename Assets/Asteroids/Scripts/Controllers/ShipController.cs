using System;
using UnityEngine;
using Object = UnityEngine.Object;

public sealed class ShipController
{
    public event Action OnDiedEvent;

    private ShipModel _model;
    private ShipView _view;
    private ShipData _data;
    private GameObject _prefab;
    private Vector3 _startPosition;
    private Transform _bulletStartPoint;
    private MovementController _movementController;

    public Transform BulletStartPoint => _bulletStartPoint;

    public ShipController(ShipData data)
    {
        _data = data;
        _startPosition = data.StartPosition;
        _prefab = data.ShipPrefab;
    }

    public void Init(ShipType type)
    {
        _model = new ShipModel(_data);
        GameObject shipGameObject = Object.Instantiate(_prefab, _startPosition, Quaternion.identity);
        _view = shipGameObject.GetComponent<ShipView>();
        _bulletStartPoint = _view.BulletSpawnPoint;

        if (shipGameObject.TryGetComponent(out CapsuleCollider collider))
        {
            _movementController = new MovementController(_view.Rigidbody, _model, collider, type);
        }

        OnEnable();
    }

    public void Execute()
    {
        _movementController.Execute();
    }

    public void FixedExecute()
    {
        _movementController.FixedExecute();
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
}