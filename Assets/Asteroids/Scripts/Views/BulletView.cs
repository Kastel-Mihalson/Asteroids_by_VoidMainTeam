using System;
using System.Collections;
using UnityEngine;

public class BulletView : MonoBehaviour, IInteractiveObject, IBullet
{
    public Rigidbody Rigidbody => gameObject.GetComponent<Rigidbody>();
    private GameObjectPool _bulletPool;
    
    public event Action<GameObject> ReturnBulletToPoolEvent;


    private void OnTriggerEnter(Collider other)
    {
        var interactiveObject = other.gameObject.GetComponent<IInteractiveObject>();

        if (interactiveObject is IAsteroid)
        {
            ReturnBulletToPool(gameObject);
        }
    }

    public void Die(float lifeTime)
    {
        //yield return new WaitForSeconds(lifeTime);
        //ReturnBulletToPool(gameObject);
        Destroy(gameObject, lifeTime);
    }

    public void ReturnBulletToPool(GameObject bullet) => ReturnBulletToPoolEvent?.Invoke(bullet);
    
}
