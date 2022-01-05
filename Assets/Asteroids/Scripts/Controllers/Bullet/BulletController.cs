using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController
{
    public BulletModel _model;
    public BulletView _view;

    public BulletController(BulletModel model, BulletView view)
    {
        _model = model;
        _view = view;
    }

    public void BulletFly(Rigidbody rigidbody)
    {
        rigidbody.velocity = Vector3.forward * _model.bulletSpeed;
    }
}
