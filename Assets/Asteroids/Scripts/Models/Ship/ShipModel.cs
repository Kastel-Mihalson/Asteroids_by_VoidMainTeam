public class ShipModel
{
    private float _moveSpeed;
    private float _turnSpeed;

    public float MoveSpeed
    {
        get => _moveSpeed;
        private set { _moveSpeed = value; }
    }
    public float Turn
    {
        get => _turnSpeed;
        private set { _turnSpeed = value; }
    }

    public ShipModel(float moveSpeed, float turnSpeed)
    {
        _moveSpeed = moveSpeed;
        _turnSpeed = turnSpeed;
    }
}
