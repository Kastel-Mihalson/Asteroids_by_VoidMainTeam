using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotScript : MonoBehaviour
{
    public float shotSpeed = 2f;
    public float screenSizeTop = 5f;
    private void Start()
    {
        Rigidbody shotRigidbody = GetComponent<Rigidbody>();
        shotRigidbody.velocity = Vector3.forward * shotSpeed;
    }
    private void Update()
    {
        if (gameObject.transform.position.z > screenSizeTop)
        {
            Destroy(gameObject);
        }
    }
}