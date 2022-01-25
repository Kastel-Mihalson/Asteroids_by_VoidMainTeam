using System;
using System.Collections;
using UnityEngine;

public class BulletView : MonoBehaviour, IInteractiveObject, IBullet
{
    public event Func<int?> GetBulletDamageEvent;
    public event Action<GameObject> ReturnBulletToPoolEvent;
    
    public Rigidbody Rigidbody => gameObject.GetComponent<Rigidbody>();
    public PlayerHUDView _playerHUD => FindObjectOfType<PlayerHUDView>();

    private void OnTriggerEnter(Collider other)
    {
        Interact(other);
    }

    public void Interact(Collider other)
    {
        var interactiveObject = other.gameObject.GetComponent<IInteractiveObject>();
        if (interactiveObject is IBullet)
        {
            return;
        }
        if (interactiveObject is IAsteroid)
        {
            var asteroid = (AsteroidView)interactiveObject;
            var asteroidHP = asteroid.GetAsteroidHealth();

            _playerHUD.SetScore(asteroidHP);
        }
        if (interactiveObject is IShip)
        {
            var ship = (ShipView)interactiveObject;

            _playerHUD.SetScore(20);
        }

        ReturnBulletToPool(gameObject);
    }

    public void Die(float lifeTime)
    {
        StartCoroutine(nameof(ReturnBulletToPoolTimer), lifeTime);
    }

    private IEnumerator ReturnBulletToPoolTimer(float time)
    {
        yield return new WaitForSeconds(time);
        ReturnBulletToPool(gameObject);
        StopCoroutine(nameof(ReturnBulletToPoolTimer));
    }

    public void ReturnBulletToPool(GameObject bullet) => ReturnBulletToPoolEvent?.Invoke(bullet);
    public int? GetBulletDamage() => GetBulletDamageEvent?.Invoke();
}
