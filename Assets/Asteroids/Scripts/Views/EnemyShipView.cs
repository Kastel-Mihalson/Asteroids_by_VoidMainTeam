using UnityEngine;

public sealed class EnemyShipView : ShipView
{
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
}
