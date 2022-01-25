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

    private PlayerHUDView _playerHUD;
    private HealthBar _enemyHealth;
    private ArmorBar _enemyArmor;
    private ShipType _shipType;

    public ShipController(ShipData data, PlayerHUDView viewHUD)
    {
        _data = data;
        _startPosition = data.StartPosition;
        _prefab = data.ShipPrefab;
        _playerHUD = viewHUD;
    }

    public void Init(ShipType type)
    {
        _shipType = type;
        _model = new ShipModel(_data);
        GameObject shipGameObject = Object.Instantiate(_prefab, _startPosition, Quaternion.identity);
        _view = shipGameObject.GetComponent<ShipView>();
        _enemyHealth = Object.FindObjectOfType<EnemyHealthBar>();
        _enemyArmor = Object.FindObjectOfType<EnemyArmorBar>();

        _bulletStartPoint = _view.BulletSpawnPoint;

        if (_shipType == ShipType.Player)
        {
            _playerHUD.SetMaxHealth(_model.MaxHP);
            _playerHUD.SetMaxArmor(_model.MaxArmor);
        }
        if (_shipType == ShipType.Enemy)
        {
            _enemyHealth.SetMaxHealth(_model.MaxHP);
        }

        if (shipGameObject.TryGetComponent(out CapsuleCollider collider))
        {
            _movementController = type switch
            {
                ShipType.Player => new PlayerShipMovement(),
                ShipType.Enemy => new EnemyShipMovement(),
                _ => null
            };

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

    private void SetGameScore(int value)
    {
        _playerHUD.SetScore(value);
    }

    private void RecieveDamage(int damage)
    {
        if (_model.CurrentArmor > 0)
        {
            _model.CurrentArmor -= damage;

            if (_shipType == ShipType.Player)
            {
                _playerHUD.SetArmor(_model.CurrentArmor);
            }
        }
        else
        {
            _model.CurrentHP -= damage;

            if (_shipType == ShipType.Player)
            {
                _playerHUD.SetHealth(_model.CurrentHP);
            }
            if (_shipType == ShipType.Enemy)
            {
                _enemyHealth.SetHealth(_model.CurrentHP);
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