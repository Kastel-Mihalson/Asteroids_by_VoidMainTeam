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
    
    public void AsteroidMove(Rigidbody rigidbody)
    {
        rigidbody.angularVelocity = Random.insideUnitSphere * _model.RotationSpeed;
        rigidbody.velocity = Vector3.back * Random.Range(_model.MinMoveSpeed, _model.MaxMoveSpeed);
    }
}
