public sealed class Ship : IMove, IRole
{
    private readonly IMove _movement;
    private readonly IRole _rolling;

    public float Speed => _movement.Speed;
    public float RoleAngle => _rolling.RoleAngle;
    public float RoleSpeed => _rolling.RoleSpeed;

    public Ship(IMove movement, IRole rolling)
    {
        _movement = movement;
        _rolling = rolling;
    }

    public void Role(float horizontal, float deltaTime)
    {
        _rolling.Role(horizontal, deltaTime);
    }

    public void Move(float horizontal, float vertical, float deltaTime)
    {
        _movement.Move(horizontal, vertical, deltaTime);
    }
}
