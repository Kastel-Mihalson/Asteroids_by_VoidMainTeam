public interface IRole
{
    float RoleAngle { get; }
    float RoleSpeed { get; }
    void Role(float horizontal, float deltaTime);
}