using UnityEngine;


public class ShootingController
{
    private float _nextShotTime;
    private float _shootingDistance = 10f;
    private LayerMask _enemyMask;
    private Transform _startPoint;
    private BulletInitializer _bulletInitializer;

    public ShootingController(Transform startPoint, BulletData bullet)
    {
        _enemyMask = LayerMask.GetMask("Enemy");
        _startPoint = startPoint;
        _bulletInitializer = new BulletInitializer(bullet);
    }

    public void Shoot()
    {
        bool canShoot = Time.time > _nextShotTime;
        bool isEnemyDetected = Physics.Raycast(_startPoint.position, Vector3.forward, _shootingDistance, _enemyMask);

        if (canShoot && isEnemyDetected)
        {
            _bulletInitializer.InitBullet(_startPoint.position);
            // Important! Get ShootDelay must be after InitBullet()
            _nextShotTime = Time.time + _bulletInitializer.BulletModel.ShootDelay;
        }
    }
}
