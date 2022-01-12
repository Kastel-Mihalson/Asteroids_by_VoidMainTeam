public class ShipModel
{
    private float _moveSpeed = 10f;
    private float _turn = 4f;

    public float MoveSpeed
    {
        get => _moveSpeed;
        set { _moveSpeed = value; }
    }
    public float Turn
    {
        get => _turn;
        set { _turn = value; }
    }
}
