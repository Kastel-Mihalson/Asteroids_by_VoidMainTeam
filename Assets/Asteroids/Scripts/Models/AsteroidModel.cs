using UnityEngine;

public class AsteroidModel
{
    private float _minMoveSpeed;
    private float _maxMoveSpeed;
    private float _rotationSpeed;
    private int _damage;
    private float _lifeTime;
    private float _minSize;
    private float _maxSize;

    public float MinMoveSpeed
    {
        get => _minMoveSpeed;
        private set => _minMoveSpeed = value;
    }
    public float MaxMoveSpeed
    {
        get => _maxMoveSpeed;
        private set => _maxMoveSpeed = value;
    }
    public float RotationSpeed
    {
        get => _rotationSpeed;
        private set => _rotationSpeed = value;
    }
    public int Damage
    {
        get => _damage;
        private set => _damage = value;
    }
    public float LifeTime
    {
        get => _lifeTime;
        private set => _lifeTime = value;
    }

    public float MinSize
    {
        get => _minSize;
        private set => _minSize = value;
    }

    public float MaxSize
    {
        get => _maxSize;
        private set => _maxSize = value;
    }

    public AsteroidModel(AsteroidData data)
    {
         _minMoveSpeed = data.MinMoveSpeed;
        _maxMoveSpeed = data.MaxMoveSpeed;
        _rotationSpeed = data.RotationSpeed;
        _damage = data.Damage;
        _lifeTime = data.LifeTime;
        _minSize = data.MinSize;
        _maxSize = data.MaxSize;
    }
}
