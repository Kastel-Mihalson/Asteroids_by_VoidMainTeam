using UnityEngine;

public class BulletView : MonoBehaviour, IInteractiveObject, IBullet
{
    public Rigidbody Rigidbody => gameObject.GetComponent<Rigidbody>();

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }

    public void Die(float lifeTime)
    {
        if (gameObject)
        {
            Destroy(gameObject, lifeTime);
        }
    }
}
