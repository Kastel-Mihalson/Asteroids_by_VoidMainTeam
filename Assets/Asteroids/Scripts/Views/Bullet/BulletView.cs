using UnityEngine;


public class BulletView : MonoBehaviour
{
    public void Die(float lifeTime)
    {
        Destroy(gameObject, lifeTime);
    }
}
