using UnityEngine;


public class BulletView : MonoBehaviour
{
    public Rigidbody Rigidbody => gameObject.GetComponent<Rigidbody>();

    public void Die(float lifeTime)
    {
        Destroy(gameObject, lifeTime);
    }
}
