using UnityEngine;


public class BulletModel
{
    private float _nextShoot;
    private float _shootDelay;
    private float _bulletDestroy;
    private float _bulletSpeed;
    private float _lifeTime;
    private Transform _spawnPosition;

    public float NextShoot
    {
        get => _nextShoot;
        set { _nextShoot = value; } // TODO private
    }
    public float ShootDelay
    {
        get => _shootDelay;
        private set { _shootDelay = value; }
    }
    public float BulletDestroy
    {
        get => _bulletDestroy;
        private set { _bulletDestroy = value; }
    }
    public float BulletSpeed
    {
        get => _bulletSpeed;
        private set { _bulletSpeed = value; }
    }
    public float LifeTime
    {
        get => _lifeTime;
        private set { _lifeTime = value; }
    }
    public Transform SpawnPosition
    {
        get => _spawnPosition;
        set { _spawnPosition = value; }
    }

    public BulletModel(BulletData data)
    {
        _nextShoot = data.NextShoot;
        _shootDelay = data.ShootDelay;
        _bulletDestroy = data.BulletDestroy;
        _bulletSpeed = data.BulletSpeed;
        _lifeTime = data.LifeTime;
    }
}
