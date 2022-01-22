using System;
using UnityEngine;

public class AsteroidView : MonoBehaviour, IInteractiveObject, IAsteroid
{
    public event Func<int?> GetAsteroidDamageEvent;
    public event Action<int> OnDamagedEvent;

    private GameObject _explosionEffect;
    private float _effectTime = 2f;

    public Rigidbody Rigidbody => gameObject.GetComponent<Rigidbody>();

    private void Start()
    {
        _explosionEffect = Resources.Load("Explosion/Explosion") as GameObject;
    }

    private void OnTriggerEnter(Collider other)
    {        
        var interactiveObject = other.gameObject.GetComponent<IInteractiveObject>();
        
        if (interactiveObject is IAsteroid)
        {
            return;
        }
        if (interactiveObject is IBullet)
        {
            var asteroidExplosion = Instantiate(_explosionEffect, transform.position, transform.rotation);
            asteroidExplosion.transform.localScale = Vector3.one * 0.2f;
            Destroy(asteroidExplosion, _effectTime);

            var bulletView = (BulletView)interactiveObject;
            int? damage = bulletView.GetBulletDamage();
            if (damage != null)
            {
                OnDamagedEvent?.Invoke((int)damage);
            }
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
        Destroy(gameObject);
        GameObject asteroidExplosion = Instantiate(_explosionEffect, transform.position, transform.rotation);
        Destroy(asteroidExplosion, _effectTime);
    }

    public int? GetAsteroidDamage() => GetAsteroidDamageEvent?.Invoke();
}
