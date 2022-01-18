using UnityEngine;

public class BulletModel
{
    private float _bulletSpeed;
    private float _lifeTime;
    private float _shootDelay;
    private int _damage;

    public float BulletSpeed
    {
        get => _bulletSpeed;
        private set => _bulletSpeed = value;
    }
    public float LifeTime
    {
        get => _lifeTime;
        private set => _lifeTime = value;
    }

    public float ShootDelay
    {
        get => _shootDelay;
        set => _shootDelay = value;
    }
    public int Damage
    {
        get => _damage;
        private set => _damage = value;
    }

    public BulletModel(BulletData data)
    {
        _bulletSpeed = data.BulletSpeed;
        _lifeTime = data.LifeTime;
        _shootDelay = data.ShootDelay;
        _damage = data.Damage;
    }
}
