using System;
using System.Collections;
using UnityEngine;

public class AsteroidView : MonoBehaviour, IInteractiveObject, IAsteroid
{
    public event Action<int> OnDamagedEvent;
    public event Action<GameObject> ReturnAsteroidToPoolEvent;

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
            ReturnAsteroidToPool(gameObject);
        }
        else
        {
            Die();
        }

    }

    public void Die(float lifeTime)
    {
        StartCoroutine(nameof(ReturnAsteroidToPoolTimer), lifeTime);

        Destroy(gameObject, lifeTime);
    }

    private IEnumerator ReturnAsteroidToPoolTimer(float time)
    {
        yield return new WaitForSeconds(time);
        ReturnAsteroidToPool(gameObject);
        StopCoroutine(nameof(ReturnAsteroidToPoolTimer));
    }

    public void Die()
    {
        Destroy(gameObject);

        StartCoroutine(nameof(ReturnAsteroidToPoolTimer));
        GameObject asteroidExplosion = Instantiate(_explosionEffect, transform.position, transform.rotation);
        Destroy(asteroidExplosion, _effectTime);
    }

    public void ReturnAsteroidToPool(GameObject asteroid) => ReturnAsteroidToPoolEvent?.Invoke(asteroid);
}
