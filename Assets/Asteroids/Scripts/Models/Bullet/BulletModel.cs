using UnityEngine;

public class BulletModel
{
    private float _bulletDestroy;
    private float _bulletSpeed;
    private float _lifeTime;

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

    public BulletModel(BulletData data)
    {
        _bulletDestroy = data.BulletDestroy;
        _bulletSpeed = data.BulletSpeed;
        _lifeTime = data.LifeTime;
    }
}
