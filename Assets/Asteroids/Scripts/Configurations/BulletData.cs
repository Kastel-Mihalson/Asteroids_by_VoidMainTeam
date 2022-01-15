using UnityEngine;

[CreateAssetMenu(fileName = "BulletData", menuName = "ScriptableObjects/BulletData", order = 1)]
public sealed class BulletData : ScriptableObject
{
    [SerializeField] private float _bulletDestroy = 2f;
    [SerializeField] private float _bulletSpeed = 15f;
    [SerializeField] private float _lifeTime = 1f;

    public float BulletDestroy => _bulletDestroy;    
    public float BulletSpeed=> _bulletSpeed;        
    public float LifeTime => _lifeTime;
}