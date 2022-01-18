using System;
using System.Collections;
using UnityEngine;

public class BulletView : MonoBehaviour, IInteractiveObject, IBullet
{
    public event Func<int?> GetBulletDamageEvent;
    public event Action<GameObject> ReturnBulletToPoolEvent;
    
    public Rigidbody Rigidbody => gameObject.GetComponent<Rigidbody>();

    private void OnTriggerEnter(Collider other)
    {
        var interactiveObject = other.gameObject.GetComponent<IInteractiveObject>();
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
