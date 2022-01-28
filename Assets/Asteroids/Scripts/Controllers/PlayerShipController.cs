using System;
using UnityEngine;
using Object = UnityEngine.Object;

public sealed class PlayerShipController : ShipController
{
    public event Action OnDiedEvent;
    public event Action<int> OnHpChangedEvent;
    public event Action<int> OnArmorChangedEvent;

    private PlayerShipModel _model;
    private PlayerShipView _view;
    private PlayerShipMovement _movementController;

    public PlayerShipController(ShipData data) : base(data)
    {
        _model = new PlayerShipModel(data);
    }

    public override void Init()
    {
        GameObject shipGameObject = Object.Instantiate(Data.ShipPrefab, Data.StartPosition, Quaternion.identity);
        _view = shipGameObject.GetComponent<PlayerShipView>();

        BulletStartPoint = _view.BulletSpawnPoint;

        _view.SetMaxHealth(_model.MaxHP);
        _view.SetMaxArmor(_model.MaxArmor);

        if (shipGameObject.TryGetComponent(out CapsuleCollider collider))
        {
            _movementController = new PlayerShipMovement();
            _movementController.Init(_view.Rigidbody, _model, collider);
        }

        OnEnable();
    }

    public override void OnEnable()
    {
        _view.OnDamagedEvent += RecieveDamage;
        OnHpChangedEvent += _view.SetHealth;
        OnArmorChangedEvent += _view.SetArmor;
        OnDiedEvent += _view.Die;
        OnDiedEvent += OnDisable;
    }

    public override void OnDisable()
    {
        _view.OnDamagedEvent -= RecieveDamage;
        OnHpChangedEvent -= _view.SetHealth;
        OnArmorChangedEvent -= _view.SetArmor;
        OnDiedEvent -= _view.Die;
        OnDiedEvent -= OnDisable;
    }

    public override void Execute()
    {
        _movementController.Execute();
    }

    public override void FixedExecute()
    {
        _movementController.FixedExecute();
    }

    private void RecieveDamage(int damage)
    {
        if (_model.CurrentArmor > 0)
        {
            _model.CurrentArmor -= damage;
            OnArmorChangedEvent?.Invoke(_model.CurrentArmor);
        }
        else
        {
            _model.CurrentHP -= damage;
            OnHpChangedEvent?.Invoke(_model.CurrentHP);
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