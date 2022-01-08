using UnityEngine;


public class BulletController
{
    private BulletModel _model;
    private BulletView _view;

    public BulletController(BulletModel model, BulletView view)
    {
        _model = model;
        _view = view;
    }

    public void BulletFly(Rigidbody rigidbody)
    {
        rigidbody.velocity = Vector3.forward * _model.BulletSpeed;
    }
}
