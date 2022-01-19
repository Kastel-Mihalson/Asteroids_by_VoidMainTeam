using UnityEngine;


public class ShootingController
{
    private BulletController _bulletController;
    private float _nextShotTime;
    private float _shootingDistance = 10f;
    private Transform _startPoint;
    private float _shootDelay;
    private LayerMask _layer;

    public ShootingController(Transform startPoint, BulletData bullet, LayerMask layer)
    {
        _startPoint = startPoint;
        _bulletController = new BulletController(bullet, _startPoint);
        _shootDelay = bullet.ShootDelay;
        _layer = layer;
    }

    public void Shoot()
    {
        bool canShoot = Time.time > _nextShotTime;
        bool isEnemyDetected = false;

        if (_startPoint != null)
        {
            isEnemyDetected = Physics.Raycast(_startPoint.position, _startPoint.forward, _shootingDistance, _layer);
        }

        if (canShoot && isEnemyDetected)
        {
            _bulletController.Init();
            _bulletController.OnDisable();
            _bulletController.OnEnable();
            _bulletController.Move();
            AudioController.PlayShot();
            _nextShotTime = Time.time + _shootDelay;
        }
    }
}
