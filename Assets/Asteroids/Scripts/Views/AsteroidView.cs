using System;
using System.Collections;
using UnityEngine;

public class AsteroidView : MonoBehaviour, IInteractiveObject, IAsteroid
{
    public event Func<int?> GetAsteroidDamageEvent;
    public event Func<int?> GetAsteroidHealthEvent;
    public event Action<int> OnDamagedEvent;

    public Rigidbody Rigidbody => gameObject.GetComponent<Rigidbody>();

    private void OnTriggerEnter(Collider other)
    {
        Interact(other);
    }

    public void Interact(Collider other)
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
            EffectController.Create(EffectManager.AsteroidHitting, gameObject.transform);
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
        EffectController.Create(EffectManager.AsteroidExplosion, gameObject.transform);
        Destroy(gameObject);
    }

    public int? GetAsteroidDamage() => GetAsteroidDamageEvent?.Invoke();
    public int? GetAsteroidHealth() => GetAsteroidHealthEvent?.Invoke();    
}
