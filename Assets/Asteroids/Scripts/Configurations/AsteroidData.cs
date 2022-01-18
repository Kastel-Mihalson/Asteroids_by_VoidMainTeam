using UnityEngine;


[CreateAssetMenu(fileName = "AsteroidData", menuName = "ScriptableObjects/AsteroidData", order = 1)]
public sealed class AsteroidData : ScriptableObject
{
    [SerializeField] private float _minMoveSpeed = 4f;
    [SerializeField] private float _maxMoveSpeed = 8f;
    [SerializeField] private float _moveSpeed = 4f;
    [SerializeField] private float _rotationSpeed = 3f;
    [SerializeField] private int _hp = 1;
    [SerializeField] private int _damage = 1;
    [SerializeField] private float _lifeTime = 1;
    [SerializeField] private GameObject _asteroidPrefab;
    [SerializeField] private float _minSize = 0.4f;
    [SerializeField] private float _maxSize = 1f;
    [SerializeField] private float _size = 3f;

    public float MinMoveSpeed => _minMoveSpeed;
    public float MaxMoveSpeed => _maxMoveSpeed;
    public float MoveSpeed => _moveSpeed;
    public float RotationSpeed => _rotationSpeed;
    public int HP => _hp;
    public int Damage => _damage;
    public float LifeTime => _lifeTime;
    public GameObject AsteroidPrefab => _asteroidPrefab;
    public float MinSize => _minSize;
    public float MaxSize => _maxSize;
    public float Size => _size;
}