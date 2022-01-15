using UnityEngine;


[CreateAssetMenu(fileName = "AsteroidData", menuName = "ScriptableObjects/AsteroidData", order = 1)]
public sealed class AsteroidData : ScriptableObject
{
    [SerializeField] private float _minMoveSpeed = 4f;
    [SerializeField] private float _maxMoveSpeed = 8f;
    [SerializeField] private float _rotationSpeed = 3f;
    [SerializeField] private int _damage = 1;
    [SerializeField] private float _lifeTime = 1;
    //[SerializeField] private int _size = 1;

    public float MinMoveSpeed => _minMoveSpeed;
    public float MaxMoveSpeed => _maxMoveSpeed;
    public float RotationSpeed => _rotationSpeed;
    public int Damage => _damage;
    public float LifeTime => _lifeTime;
    //public int Size => _size;
}