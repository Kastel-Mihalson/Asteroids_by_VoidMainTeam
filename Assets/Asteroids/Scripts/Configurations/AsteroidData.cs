using UnityEngine;


[CreateAssetMenu]
public sealed class AsteroidData : ScriptableObject
{
    [SerializeField] private float _minMoveSpeed = 4f;
    [SerializeField] private float _maxMoveSpeed = 8f;
    [SerializeField] private float _rotationSpeed = 3f;
    [SerializeField] private float _minDelay = 0.5f;
    [SerializeField] private float _maxDelay = 3f;
    [SerializeField] private float _nextSpawn = 0f;
    [SerializeField] private int _damage = 1;
    [SerializeField] private GameObject _prefab;
    //[SerializeField] private int _size = 1;

    public float MinMoveSpeed => _minMoveSpeed;
    public float MaxMoveSpeed => _maxMoveSpeed;
    public float RotationSpeed => _rotationSpeed;
    public float MinDelay => _minDelay;
    public float MaxDelay => _maxDelay;
    public float NextSpawn => _nextSpawn;
    public int Damage => _damage;
    public GameObject Prefab => _prefab;
    //public int Size => _size;
}