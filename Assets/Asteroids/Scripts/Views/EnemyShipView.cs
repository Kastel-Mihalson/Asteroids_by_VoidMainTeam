using UnityEngine;

public sealed class EnemyShipView : ShipView
{
    private EnemyHUDView _hudView;

    public override void Interact(Collider other)
    {
        if (other.TryGetComponent(out IInteractiveObject interactiveObject))
        {
            if (interactiveObject is IBullet)
            {
                var bulletView = (BulletView)interactiveObject;
                int? damage = bulletView.GetBulletDamage();
                if (damage != null)
                {
                    GetDamage((int)damage);
                }

                AudioController.Play(AudioClipManager.ShipHitting);
                EffectController.Create(EffectManager.ShipHitting, gameObject.transform);
            }
        }
    }

    public override void Die()
    {
        AudioController.Play(AudioClipManager.ShipExplosion);
        EffectController.Create(EffectManager.ShipExplosion, gameObject.transform);
        Destroy(gameObject);
    }

    private void Awake()
    {
        _hudView = FindObjectOfType<EnemyHUDView>();
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
}
