using UnityEngine;


public class ShootingController
{
    private BulletController _bulletController;
    private float _nextShotTime;
    private float _shootingDistance = 10f;
    private LayerMask _enemyMask;
    private Transform _startPoint;
    private float _shootDelay;

    public ShootingController(Transform startPoint, BulletData bullet)
    {
        _enemyMask = LayerMask.GetMask("Enemy");
        _startPoint = startPoint;
        _bulletController = new BulletController(bullet, _startPoint);
        _shootDelay = bullet.ShootDelay;
    }

    public void Shoot()
    {
        bool canShoot = Time.time > _nextShotTime;
        bool isEnemyDetected = Physics.Raycast(_startPoint.position, Vector3.forward, _shootingDistance, _enemyMask);

        if (canShoot && isEnemyDetected)
        {
            _bulletController.Init();
            _bulletController.Move();
            _nextShotTime = Time.time + _shootDelay;
        }
    }
}
