using System;
using UnityEngine;

public class ShipController
{
    private ShipModel _model;
    private ShipView _view;

    public ShipController(ShipModel model, ShipView view)
    {
        _model = model;
        _view = view;
    }

    public void MoveWithRigidBody(Rigidbody rigidbody)
    {
        if (rigidbody)
        {
            float vertical = Input.GetAxis("Vertical");
            float horizontal = Input.GetAxis("Horizontal");

            rigidbody.velocity = new Vector3(horizontal, 0, vertical) * _model.moveSpeed;
            rigidbody.rotation = Quaternion.Euler(0, 0, -rigidbody.velocity.x * _model.turn);
        }
        else
        {
            throw new Exception("Component rigidBody for Player is not defind or null. Check it out");
        }
    }
}
