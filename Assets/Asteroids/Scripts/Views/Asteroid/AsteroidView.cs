using UnityEngine;

public class AsteroidView : MonoBehaviour
{
    private GameObject explosionEffect;

    private void Start()
    {
        explosionEffect = Resources.Load("Explosion/Explosion_1") as GameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "GameController") return;

        if (other.tag == "Player")
        {
            GameObject playerExplosion = Instantiate(explosionEffect, other.transform.position, other.transform.rotation);
            Destroy(playerExplosion, 2f);
        }

        GameObject asteroidExplosion = Instantiate(explosionEffect, transform.position, transform.rotation);
        Destroy(asteroidExplosion, 2f);
        Destroy(gameObject);
        Destroy(other.gameObject);
    }
}
