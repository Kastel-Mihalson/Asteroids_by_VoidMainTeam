public class BulletModel
{
    private float _nextShoot = 0f;
    private float _shootDelay = 0.2f;
    private float _bulletDestroy = 2f;
    private float _bulletSpeed = 15f;
    private float _lifeTime = 1f;

    public float NextShoot
    {
        get => _nextShoot;
        set { _nextShoot = value; }
    }
    public float ShootDelay
    {
        get => _shootDelay;
        set { _shootDelay = value; }
    }
    public float BulletDestroy
    {
        get => _bulletDestroy;
        set { _bulletDestroy = value; }
    }
    public float BulletSpeed
    {
        get => _bulletSpeed;
        set { _bulletSpeed = value; }
    }
    public float LifeTime
    {
        get => _lifeTime;
        set { _lifeTime = value; }
    }
}
