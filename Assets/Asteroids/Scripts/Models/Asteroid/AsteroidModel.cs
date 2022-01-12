public class AsteroidModel
{
    private float _minMoveSpeed = 4f;
    private float _maxMoveSpeed = 8f;
    private float _rotationSpeed = 3f;
    private float _minDelay = 0.5f;
    private float _maxDelay = 3f;
    private float _nextSpawn = 0f;
    private int _size = 1;

    public float MinMoveSpeed
    {
        get => _minMoveSpeed;
        set { _minMoveSpeed = value; }
    }
    public float MaxMoveSpeed
    {
        get => _maxMoveSpeed;
        set { _maxMoveSpeed = value; }
    }
    public float RotationSpeed
    {
        get => _rotationSpeed;
        set { _rotationSpeed = value; }
    }
    public float MinDelay
    {
        get => _minDelay;
        set { _minDelay = value; }
    }
    public float MaxDelay
    {
        get => _maxDelay;
        set { _maxDelay = value; }
    }
    public float NextSpawn
    {
        get => _nextSpawn;
        set { _nextSpawn = value; }
    }
    public int Size
    {
        get => _size;
        set { _size = value; }
    }
}
