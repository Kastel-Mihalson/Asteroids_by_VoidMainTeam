using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{

    void Start()
    {
        transform.GetComponent<Rigidbody>().AddForce(Vector3.down * 20f, ForceMode.Impulse);
    }

    void Update()
    {
        if (this.gameObject.transform.position.y < -5)
        {
            this.gameObject.transform.Translate(0, 15, 0, Space.World);
        }
    }

    public void SpawnAsteroid(GameObject asteroidPrefab)
    {
        GameObject enemy = GameObject.Instantiate(asteroidPrefab, AddPositionAsteroid(), Quaternion.identity, gameObject.transform);
    }

    public Vector3 AddPositionAsteroid()
    {
        Vector3 v3 = new Vector3();
        v3.x = Random.Range(-10.0f, 10.0f);
        v3.y = Random.Range(-10.0f, 10.0f);
        v3.z = Random.Range(-10.0f, 10.0f);
        return v3;
    }
}
