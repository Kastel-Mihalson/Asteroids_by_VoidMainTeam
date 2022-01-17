using System;
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
        if (transform.position.z > 7f || transform.position.z < -7f)
        {
            ReturnBulletToPool(gameObject);
        }
        // Destroy(gameObject, lifeTime);
    }

    public void ReturnBulletToPool(GameObject bullet) => ReturnBulletToPoolEvent?.Invoke(bullet);

    public int? GetBulletDamage() => GetBulletDamageEvent?.Invoke();

}