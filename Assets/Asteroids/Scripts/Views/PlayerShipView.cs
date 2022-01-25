using UnityEngine;
using UnityEngine.UI;

public sealed class PlayerShipView : ShipView
{
    //public event Action<int> OnScoreChangedEvent;

    private PlayerHUDView _playerHUDView;

    public override void Interact(Collider other)
    {
        if (other.TryGetComponent(out IInteractiveObject interactiveObject))
        {
            int? damage = null;

            if (interactiveObject is IBullet)
            {
                var bulletView = (BulletView)interactiveObject;
                damage = bulletView.GetBulletDamage();
                
                AudioController.Play(AudioClipManager.ShipHitting);
                EffectController.Create(EffectManager.ShipHitting, gameObject.transform);
            }
            else if (interactiveObject is IAsteroid)
            {
                var asteroidView = (AsteroidView)interactiveObject;
                damage = asteroidView.GetAsteroidDamage();                
            }

            if (damage != null)
            {
                GetDamage((int)damage);
            }
        }
    }

    public override void Die()
    {
        AudioController.Play(AudioClipManager.ShipExplosion);
        EffectController.Create(EffectManager.ShipExplosion, gameObject.transform);
        Destroy(gameObject);
    }

    // TODO remove awake
    private void Awake()
    {
        _playerHUDView = FindObjectOfType<PlayerHUDView>();
    }

    public void SetMaxHealth(int health)
    {
        _playerHUDView.SetMaxHealth(health);
    }

    public void SetHealth(int health)
    {
        _playerHUDView.SetHealth(health);
    }

    public void SetMaxArmor(int armor)
    {
        _playerHUDView.SetMaxArmor(armor);
    }

    public void SetArmor(int armor)
    {
        _playerHUDView.SetArmor(armor);
    }

    public void SetScore(int? value)
    {
        _playerHUDView.SetScore(value);
    }
}
