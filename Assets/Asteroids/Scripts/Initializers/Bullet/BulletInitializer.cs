using UnityEngine;

public class BulletInitializer
{
    private BulletData _bullet;
    private GameObject _bulletPrefab;
    private BulletModel _bulletModel;
    private BulletView _bulletView;
    private BulletController _bulletController;

    public BulletModel BulletModel => _bulletModel;
    public BulletView BulletView => _bulletView;
    public BulletController BulletController => _bulletController;

    public BulletInitializer(BulletData bullet, GameObject bulletPrefab)
    {
        _bullet = bullet;
        _bulletPrefab = bulletPrefab;
    }

    public void InitBullet(Vector3 spawnPosition)
    {
        GameObject bulletGameObject = Object.Instantiate(_bulletPrefab, spawnPosition, Quaternion.identity);
        _bulletModel = new BulletModel(_bullet);
        _bulletView = bulletGameObject.GetComponent<BulletView>();

        _bulletController = new BulletController(_bulletModel, _bulletView);
        _bulletController.BulletFly();
        _bulletView.Die(_bulletModel.LifeTime);
    }
}
