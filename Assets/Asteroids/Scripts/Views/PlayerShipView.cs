﻿using System;
using System.Collections;
using UnityEngine;

public sealed class PlayerShipView : ShipView, IPlayer
{
    public event Action OnBurnEvent;
    public event Action<float> OnDecceleratedEvent;
    public event Action<float> OnAcceleratedEvent;
    public event Action OnNormilizedSpeedEvent;

    private PlayerHUDView _hudView;
    private EndGameMenuView _loseMenu;
    private float _fireHitTimeDelay = 1f;
    private int _fireDamage = 1;
    private float _decelerationTime = 3f;


    public override void Interact(Collider other)
    {
        if (other.TryGetComponent(out IInteractiveObject interactiveObject))
        {
            int? damage = null;

            if (interactiveObject is IBullet)
            {
                var bulletView = (BulletView)interactiveObject;
                damage = bulletView.GetBulletDamage();
            }
            else if (interactiveObject is IAsteroid)
            {
                var asteroidView = (AsteroidView)interactiveObject;
                damage = asteroidView.GetAsteroidDamage();

                if (asteroidView is IFire)
                {
                    StartCoroutine(Burn((int)damage));
                }
                else if (asteroidView is IIce)
                {
                    StartCoroutine(Decelerate((int)damage));
                }
            }
            else if (interactiveObject is IEnemy)
            {
                Die();
            }

            if (damage != null)
            {
                GetDamage((int)damage);
            }
        }
    }

    public override void Die()
    {
        _hudView.IsDead = true;

        if (_loseMenu)
        {
            _loseMenu.ShowResult(false);
        }

        Destroy(gameObject);
    }

    private void Awake()
    {
        _loseMenu = FindObjectOfType<EndGameMenuView>();
    }

    public void SetHUDView(PlayerHUDView view)
    {
        _hudView = view;
    }

    public void SetMaxHealth(int health)
    {
        _hudView.SetMaxHealth(health);
    }

    public void SetHealth(int health)
    {
        _hudView.SetHealth(health);
    }

    public void SetMaxArmor(int armor)
    {
        _hudView.SetMaxArmor(armor);
    }

    public void SetArmor(int armor)
    {
        _hudView.SetArmor(armor);
    }

    public void SetScore(int? value)
    {
        _hudView.SetScore(value);
    }

    private IEnumerator Burn(int fireHitCount)
    {
        OnBurnEvent?.Invoke();

        for (var i = 0; i < fireHitCount; i++)
        {
            GetDamage(_fireDamage);
            yield return new WaitForSeconds(_fireHitTimeDelay);
        }
        
        StopCoroutine(Burn(fireHitCount));
    }

    private IEnumerator Decelerate(int speedParam)
    {
        OnDecceleratedEvent?.Invoke(speedParam * 1.5f);
        yield return new WaitForSeconds(_decelerationTime);
        OnAcceleratedEvent?.Invoke(speedParam * 1.5f);
        //OnNormilizedSpeedEvent?.Invoke();
        StopCoroutine(Decelerate(speedParam));
    }
}
