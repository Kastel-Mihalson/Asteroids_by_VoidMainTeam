using UnityEngine;

public sealed class ShipController
{
    private ShipModel _model;
    private ShipView _view;
    private ShipData _data;
    private Rigidbody _rigidBody;
    private GameObject _gameObject;
    private GameObject _prefab;
    private Vector3 _startPosition;
    private Vector3 _movement;
    private float _leftScrenBorder;
    private float _rightScrenBorder;
    private float _topScrenBorder;
    private float _bottomScrenBorder;

    public ShipController(ShipData data, GameModel gameModel)
    {
        _data = data;
        _startPosition = data.StartPosition;
        _prefab = data.ShipPrefab;
        _leftScrenBorder = gameModel.LeftScreenBorder;
        _rightScrenBorder = gameModel.RightScreenBorder;
        _topScrenBorder = gameModel.TopScreenBorder;
        _bottomScrenBorder = gameModel.BottomScreenBorder;
    }

    public void Init()
    {
        _model = new ShipModel(_data);
        GameObject shipGameObject = Object.Instantiate(_prefab, _startPosition, Quaternion.identity);
        _view = shipGameObject.GetComponent<ShipView>();
        _rigidBody = _view.Rigidbody;
        _gameObject = _view.gameObject;
    }

    public void Execute()
    {
        _movement = GetMovementDirection();
        LimitFlightArea(_leftScrenBorder, _rightScrenBorder, _topScrenBorder, _bottomScrenBorder);
    }

    public void FixedExecute()
    {
        MoveWithRigidBody(_movement);
    }
    private Vector3 GetMovementDirection()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        return new Vector3(horizontal, 0, vertical);
    }

    private void MoveWithRigidBody(Vector3 movement)
    {
        if (_rigidBody)
        {        
            _rigidBody.velocity = movement * _model.MoveSpeed;
            _rigidBody.rotation = Quaternion.Euler(0, 0, -_rigidBody.velocity.x * _model.Turn);
        }
    }

    private void LimitFlightArea(float leftLimit, float rightLimit, float topLimit, float bottomLimit)
    {
        if (_gameObject)
        {
            float x = Mathf.Clamp(_gameObject.transform.position.x, leftLimit, rightLimit);
            float z = Mathf.Clamp(_gameObject.transform.position.z, bottomLimit, topLimit);

            _gameObject.transform.position = new Vector3(x, 0, z);
        }
    }
}
