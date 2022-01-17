using UnityEngine;


public class BulletController
{
    private BulletModel _model;
    private Rigidbody _rigidbody;

    public BulletController(BulletModel model, BulletView view)
    {
        _model = model;
        _rigidbody = view.Rigidbody;
    }

    public void BulletFly()
    {
        if (_rigidbody)
        {
            _rigidbody.velocity = Vector3.forward * _model.BulletSpeed;
        }
    }
}
