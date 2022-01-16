using UnityEngine;

public sealed class ShipController
{
    private ShipModel _model;
    private Rigidbody _rigidBody;
    private GameObject _gameObject;

    public ShipController(ShipModel model, ShipView view)
    {
        _model = model;
        _rigidBody = view.Rigidbody;
        _gameObject = view.gameObject;
    }

    public void MoveWithRigidBody(Vector3 movement)
    {
        if (_rigidBody)
        {        
            _rigidBody.velocity = movement * _model.MoveSpeed;
            _rigidBody.rotation = Quaternion.Euler(0, 0, -_rigidBody.velocity.x * _model.Turn);
        }
    }

    public void LimitFlightArea(float leftLimit, float rightLimit, float topLimit, float bottomLimit)
    {
        if (_gameObject)
        {
            float x = Mathf.Clamp(_gameObject.transform.position.x, leftLimit, rightLimit);
            float z = Mathf.Clamp(_gameObject.transform.position.z, bottomLimit, topLimit);

            _gameObject.transform.position = new Vector3(x, 0, z);
        }
    }

    public Vector3 GetMovementDirection()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        return new Vector3(horizontal, 0, vertical);
    }
}
