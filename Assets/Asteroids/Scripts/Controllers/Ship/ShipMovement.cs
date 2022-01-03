using UnityEngine;


public sealed class ShipMovement: IMove
{
    private readonly Transform _transform;
    private readonly Vector3 _direction;

    public float Speed { get; protected set; }

    public ShipMovement(Transform transfrom, float speed)
    {
        _transform = transfrom;
        Speed = speed;
    }

    public void Move(float horizontal, float vertical, float deltaTime)
    {
        //TODO Ограничение передвижения по размеру экрана

        float speed = Speed * deltaTime;
        _direction.Set(horizontal * speed, vertical * speed, 0.0f);

        _transform.localPosition += _direction;
    }
}
