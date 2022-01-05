using UnityEngine;

public class AsteroidController
{
    public AsteroidModel _model;
    public AsteroidView _view;

    public AsteroidController(AsteroidModel model, AsteroidView view)
    {
        _model = model;
        _view = view;
    }
    
    public void AsteroidMove(Rigidbody rigidbody)
    {
        rigidbody.angularVelocity = Random.insideUnitSphere * _model.rotationSpeed;
        rigidbody.velocity = Vector3.back * Random.Range(_model.minMoveSpeed, _model.maxMoveSpeed);
    }
}
