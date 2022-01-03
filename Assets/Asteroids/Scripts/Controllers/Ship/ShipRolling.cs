using UnityEngine;


public sealed class ShipRolling : IRole
{
    private readonly Transform _transform;

    public float RoleAngle { get; protected set; }
    public float RoleSpeed { get; protected set; }

    public ShipRolling(Transform transfrom, float roleAngle, float roleSpeed)
    {
        _transform = transfrom;
        RoleAngle = roleAngle;
        RoleSpeed = roleSpeed;
    }

    public void Role(float horizontal, float deltaTime)
    {
        Quaternion angle = Quaternion.Euler(0f, -RoleAngle * horizontal, 0f);
        float roleSpeed = RoleSpeed * deltaTime;

        _transform.rotation = Quaternion.Slerp(_transform.rotation, angle, roleSpeed);
    }
}

