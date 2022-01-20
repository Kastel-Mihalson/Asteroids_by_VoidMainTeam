using System;
using UnityEngine;

public class AsteroidView : MonoBehaviour, IInteractiveObject, IAsteroid
{
    public event Action<int> OnDamagedEvent;

    public Rigidbody Rigidbody => gameObject.GetComponent<Rigidbody>();

    private void OnTriggerEnter(Collider other)
    {        
        var interactiveObject = other.gameObject.GetComponent<IInteractiveObject>();
        
        if (interactiveObject is IAsteroid)
        {
            return;
        }
        if (interactiveObject is IBullet)
        {
            var bulletView = (BulletView)interactiveObject;
            int? damage = bulletView.GetBulletDamage();
            if (damage != null)
            {
                OnDamagedEvent?.Invoke((int)damage);
            }
            AudioController.Play(AudioClipManager.AsteroidHitting);
            EffectController.Init(EffectManager.AsteroidHitting, gameObject.transform);
        }
        else
        {
            Die();
        }

    }

    public void Die(float lifeTime)
    {
        Destroy(gameObject, lifeTime);
    }

    public void Die()
    {
        AudioController.Play(AudioClipManager.AsteroidExplosion);
        EffectController.Init(EffectManager.AsteroidExplosion, gameObject.transform);
        Destroy(gameObject);
    }
}
