using UnityEngine;


[CreateAssetMenu]
public sealed class BulletData : ScriptableObject
{
    [SerializeField] private float _nextShoot = 0f;
    [SerializeField] private float _shootDelay = 0.2f;
    [SerializeField] private float _bulletDestroy = 2f;
    [SerializeField] private float _bulletSpeed = 15f;
    [SerializeField] private float _lifeTime = 1f;
    [SerializeField] private GameObject _prefab;

    public float NextShoot => _nextShoot;
    public float ShootDelay=> _shootDelay;
    public float BulletDestroy => _bulletDestroy;    
    public float BulletSpeed=> _bulletSpeed;        
    public float LifeTime => _lifeTime;
    public GameObject Prefab => _prefab;
}