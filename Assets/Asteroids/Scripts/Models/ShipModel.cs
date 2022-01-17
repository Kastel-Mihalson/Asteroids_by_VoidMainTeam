using UnityEngine;

public sealed class ShipModel
{
    private float _moveSpeed;
    private float _turnSpeed;

    public float MoveSpeed
    {
        get => _moveSpeed;
        private set => _moveSpeed = value;
    }
    public float TurnSpeed
    {
        get => _turnSpeed;
        private set => _turnSpeed = value;
    }

    public ShipModel(ShipData data)
    {
        _moveSpeed = data.MoveSpeed;
        _turnSpeed = data.TurnSpeed;
    }
}
