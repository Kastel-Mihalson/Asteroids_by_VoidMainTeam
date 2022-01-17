using UnityEngine;


public class ShootingController
{
    private BulletController _bulletController;
    private float _nextShotTime;
    private float _shootingDistance = 10f;
    private Transform _startPoint;
    private float _shootDelay;

    public ShootingController(Transform startPoint, BulletData bullet)
    {
        _startPoint = startPoint;
        _bulletController = new BulletController(bullet, _startPoint);
        _shootDelay = bullet.ShootDelay;
    }

    public void Shoot()
    {
        bool canShoot = Time.time > _nextShotTime;
        bool isEnemyDetected = false;

        if (_startPoint != null)
        {
            isEnemyDetected = Physics.Raycast(_startPoint.position, _startPoint.forward, out RaycastHit hit, _shootingDistance);
            // TODO shoot on bullet
            if (hit.transform.gameObject.TryGetComponent<IBullet>(out _))
            {
                return;
            }
        }

        if (canShoot && isEnemyDetected)
        {
            _bulletController.Init();
            _bulletController.Move();
            _bulletController.OnEnable();
            _nextShotTime = Time.time + _shootDelay;
        }
    }
}
