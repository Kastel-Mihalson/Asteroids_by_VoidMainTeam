using UnityEngine;

public class BulletView : MonoBehaviour, IInteractiveObject, IBullet
{
    public Rigidbody Rigidbody => gameObject.GetComponent<Rigidbody>();

    public void Die(float lifeTime)
    {
        Destroy(gameObject, lifeTime);
    }
}
