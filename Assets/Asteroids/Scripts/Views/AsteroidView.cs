using UnityEngine;

public class AsteroidView : MonoBehaviour, IInteractiveObject, IAsteroid
{
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
            GameObject asteroidExplosion = Instantiate(_explosionEffect, transform.position, transform.rotation);
            Destroy(asteroidExplosion, _effectTime);
        }

        Destroy(gameObject);
    }

    public void Die(float lifeTime)
    {
        Destroy(gameObject, lifeTime);
    }
}
