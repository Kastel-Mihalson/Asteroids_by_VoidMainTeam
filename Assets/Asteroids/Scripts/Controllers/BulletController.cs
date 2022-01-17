using UnityEngine;


public class BulletController
{
    private BulletModel _model;
    private BulletView _view;
    private BulletData _data;
    private Rigidbody _rigidbody;
    private GameObject _prefab;
    private Transform _spawnPosition;

    public BulletController(BulletData data, Transform spawnPosition)
    {
        _data = data;
        _prefab = data.Prefab;
        _spawnPosition = spawnPosition;
    }

    public void Init()
    {
        _model = new BulletModel(_data);
        GameObject bulletGameObject = Object.Instantiate(_prefab, _spawnPosition.position, Quaternion.identity);
        _view = bulletGameObject.GetComponent<BulletView>();
        _rigidbody = _view.Rigidbody;
        _view.Die(_model.LifeTime);
    }

    public void Move()
    {
        if (_rigidbody)
        {
            _rigidbody.velocity = _spawnPosition.forward * _model.BulletSpeed;
        }
    }
}
