using UnityEngine;


public class AsteroidView : MonoBehaviour
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
        if (other.tag == "GameController") return;

        if (other.tag == "Player")
        {
            GameObject playerExplosion = Instantiate(_explosionEffect, other.transform.position, other.transform.rotation);
            Destroy(playerExplosion, _effectTime);
        }

        GameObject asteroidExplosion = Instantiate(_explosionEffect, transform.position, transform.rotation);
        Destroy(asteroidExplosion, _effectTime);
        Destroy(gameObject);
        Destroy(other.gameObject);
    }
}
