using UnityEngine;


public sealed class AsteroidController
{
    private AsteroidModel _model;
    private Rigidbody _rigidBody;

    public AsteroidController(AsteroidModel model, AsteroidView view)
    {
        _model = model;
        _rigidBody = view.Rigidbody;
    }

    public void AsteroidMove()
    {
        if (_rigidBody)
        {
            _rigidBody.angularVelocity = Random.insideUnitSphere * _model.RotationSpeed;
            _rigidBody.velocity = Vector3.back * Random.Range(_model.MinMoveSpeed, _model.MaxMoveSpeed);
        }
    }
}
