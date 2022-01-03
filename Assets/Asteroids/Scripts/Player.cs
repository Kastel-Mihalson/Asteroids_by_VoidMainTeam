using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float screenSizeTop = 5f;
    public GameObject shot;
    public Transform shotSpawn;
    public float shotDelay = 2f;

    private float _nextShot = 0f;

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space) && Time.time > _nextShot)
        {
            _nextShot = Time.time + shotDelay;
            Instantiate(shot, shotSpawn.position, Quaternion.Euler(0, 0, 0));
        }
        
    }
}
