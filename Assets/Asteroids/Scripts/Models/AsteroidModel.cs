using UnityEngine;

public class AsteroidModel
{
    private float _minMoveSpeed;
    private float _maxMoveSpeed;
    private float _moveSpeed;
    private float _rotationSpeed;
    private int _currentHP;
    private int _maxHP;
    private int _damage;
    private float _lifeTime;
    private float _minSize;
    private float _maxSize;
    private float _size;

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
    public float MoveSpeed
    {
        get => _moveSpeed;
        private set => _moveSpeed = value;
    }
    public float RotationSpeed
    {
        get => _rotationSpeed;
        private set => _rotationSpeed = value;
    }
    public int CurrentHP
    {
        get => _currentHP;
        set => _currentHP = value;
    }
    public int Damage
    {
        get => _damage;
        set => _damage = value;
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
    public float Size
    {
        get => _size;
        set => _size = value;
    }

    public AsteroidModel(AsteroidData data)
    {
         _minMoveSpeed = data.MinMoveSpeed;
        _maxMoveSpeed = data.MaxMoveSpeed;
        _moveSpeed = data.MoveSpeed;
        _rotationSpeed = data.RotationSpeed;
        _maxHP = data.HP;
        _currentHP = _maxHP;
        _damage = data.Damage;
        _lifeTime = data.LifeTime;
        _minSize = data.MinSize;
        _maxSize = data.MaxSize;
        _size = data.Size;
    }
}
