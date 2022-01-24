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

    private HealthBar _health;
    private ArmorBar _armor;
    private ShipType _shipType;

    public ShipController(ShipData data)
    {
        _data = data;
        _startPosition = data.StartPosition;
        _prefab = data.ShipPrefab;
        _health = Object.FindObjectOfType<HealthBar>();
        _armor = Object.FindObjectOfType<ArmorBar>();
    }

    public void Init(ShipType type)
    {
        _shipType = type;
        _model = new ShipModel(_data);
        GameObject shipGameObject = Object.Instantiate(_prefab, _startPosition, Quaternion.identity);
        _view = shipGameObject.GetComponent<ShipView>();
        _bulletStartPoint = _view.BulletSpawnPoint;

        if (_shipType == ShipType.Player)
        {
            _health.SetMaxHealth(_model.MaxHP);
            _armor.SetMaxArmor(_model.MaxArmor);
        }

        if (shipGameObject.TryGetComponent(out CapsuleCollider collider))
        {
            if (type == ShipType.Enemy)
            {
                _movementController = new EnemyShipMovement();
            }
            else
            {
                _movementController = new PlayerShipMovement();
            }

            _movementController.Init(_view.Rigidbody, _model, collider);
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

            if (_shipType == ShipType.Player)
            {
                _armor.SetArmor(_model.CurrentArmor);
            }
        }
        else
        {
            _model.CurrentHP -= damage;

            if (_shipType == ShipType.Player)
            {
                _health.SetHealth(_model.CurrentHP);
            }
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