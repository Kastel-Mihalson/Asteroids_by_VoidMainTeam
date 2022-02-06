﻿using UnityEngine;

public sealed class PlayerShipView : ShipView, IPlayer
{
    private PlayerHUDView _hudView;
    private EndGameMenuView _loseMenu;

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
}
