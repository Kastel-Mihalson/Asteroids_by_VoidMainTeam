using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController
{
    private AsteroidModel _model;
    private AsteroidView _view;

    public AsteroidController(AsteroidModel model, AsteroidView view)
    {
        _model = model;
        _view = view;
    }

    public void AsteroidMove(GameObject gameObject, Rigidbody rigidbody)
    {
        rigidbody.AddForce(-gameObject.transform.forward * _model.asteroidForce, ForceMode.Impulse);
    }
    
    public void ChangeAsteroidPosition(GameObject gameObject, float screenDownPosition, float screenUpPosition)
    {
        if (gameObject.transform.position.y < screenDownPosition)
        {
            gameObject.transform.Translate(0, screenUpPosition, 0, Space.World);
        }
    }

    public Vector3 AsteroidRandomSpawnPosition()
    {
        float x = Random.Range(-10.0f, 10.0f);
        float z = Random.Range(-10.0f, 10.0f);

        return new Vector3(x, 0, z);
    }
}
