using UnityEngine;

public sealed class MovementController
{
    private Rigidbody _rigidbody;
    private ShipModel _shipModel;
    private Transform _transform;
    private float _offsetX;
    private float _offsetZ;
    private float _leftMovementLimit;
    private float _rightMovementLimit;
    private float _topMovementLimit;
    private float _bottomMovementLimit;
    private float _nextChangeDirectionTime;
    private Vector3 _movement;
    private ShipType _shipType;

    public MovementController(Rigidbody rigidbody, ShipModel shipModel, CapsuleCollider collider, ShipType shipType)
    {
        _rigidbody = rigidbody;
        _shipModel = shipModel;
        _transform = _rigidbody.transform;
        _offsetX = collider.radius;
        _offsetZ = collider.height / 2;
        _shipType = shipType;
        _leftMovementLimit = GameModel.ScreenBorder[Border.Left] + _offsetX;
        _rightMovementLimit = GameModel.ScreenBorder[Border.Right] - _offsetX;
        _topMovementLimit = GameModel.ScreenBorder[Border.Top] - _offsetZ;
        _bottomMovementLimit = GameModel.ScreenBorder[Border.Bottom] + _offsetZ;

    }

    private void GetInputMovementDirection()
    {
        float vertical = Input.GetAxis(AxisManager.VERTICAL);
        float horizontal = Input.GetAxis(AxisManager.HORIZONTAL);
        _movement = new Vector3(horizontal, 0, vertical);
    }

    private void GetRandomHorizontalMovementDirection()
    {
        _movement = new Vector3(Random.Range(-1f, 1f), 0f, 0f);
    }

    private void Move()
    {
        if (_rigidbody)
        {
            _rigidbody.velocity = _movement * _shipModel.MoveSpeed * Time.deltaTime * 60;
            _rigidbody.rotation = Quaternion.Euler(0, 0, -_rigidbody.velocity.x * _shipModel.TurnSpeed * Time.deltaTime * 60);
        }
    }

    private void LimitFlightArea()
    {
        if (_transform)
        {
            float x = Mathf.Clamp(_transform.position.x, _leftMovementLimit, _rightMovementLimit);
            float z = Mathf.Clamp(_transform.position.z, _bottomMovementLimit, _topMovementLimit);
            _transform.position = new Vector3(x, 0, z);
        }
    }

    public void Execute()
    {
        if (_shipType == ShipType.Player)
        {
            GetInputMovementDirection();
        }
        else if (_shipType == ShipType.Enemy && Time.time > _nextChangeDirectionTime)
        {
            GetRandomHorizontalMovementDirection();
            _nextChangeDirectionTime += Random.Range(1f, 5f);
        }

        LimitFlightArea();
    }

    public void FixedExecute()
    {
        Move();
    }
}
