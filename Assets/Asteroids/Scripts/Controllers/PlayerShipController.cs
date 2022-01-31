﻿using System;
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
    private GodMode _godMode;
    private AudioController _audioController;
    private EffectController _effectController;

    public PlayerShipController(ShipData data, AudioController auidoController, EffectController effectController) : base(data)
    {
        _model = new PlayerShipModel(data);
        _audioController = auidoController;
        _effectController = effectController;
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

    public void ChitsInit(GodMode godMode)
    {
        _godMode = godMode;
    }

    public override void OnEnable()
    {
        _view.OnDamagedEvent += RecieveDamage;
        _view.OnDamagedEvent += CreateHittingEffects;
        OnHpChangedEvent += _view.SetHealth;
        OnArmorChangedEvent += _view.SetArmor;
        OnDiedEvent += _view.Die;
        OnDiedEvent += CreateExplosionEffects;
        OnDiedEvent += OnDisable;
    }

    public override void OnDisable()
    {
        _view.OnDamagedEvent -= RecieveDamage;
        _view.OnDamagedEvent -= CreateHittingEffects;
        OnHpChangedEvent -= _view.SetHealth;
        OnArmorChangedEvent -= _view.SetArmor;
        OnDiedEvent -= _view.Die;
        OnDiedEvent -= CreateExplosionEffects;
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
        if (_godMode.IsGodMode() == false)
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
    }

    private void Die()
    {
        OnDiedEvent?.Invoke();
    }
    
    private void CreateHittingEffects(int _)
    {
        _audioController.Play(AudioClipManager.ShipHitting);
        _effectController.Create(EffectManager.ShipHitting, _view.transform);
    }

    private void CreateExplosionEffects()
    {
        _audioController.Play(AudioClipManager.ShipExplosion);
        _effectController.Create(EffectManager.ShipExplosion, _view.transform);
    }
}