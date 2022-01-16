using UnityEngine;


public class BulletController
{
    private BulletModel _model;
    private BulletView _view;
    private Rigidbody _rigidbody;
    private GameObject _prefab;
    private Transform _spawnPosition;

    public BulletController(BulletData data, Transform spawnPosition)
    {
        _model = new BulletModel(data);
        _prefab = data.Prefab;
        _spawnPosition = spawnPosition;
    }

    public void Init()
    {
        GameObject bulletGameObject = Object.Instantiate(_prefab, _spawnPosition.position, Quaternion.identity);
        _view = bulletGameObject.GetComponent<BulletView>();
        _rigidbody = _view.Rigidbody;
        _view.Die(_model.LifeTime);
    }

    public void Move()
    {
        if (_rigidbody)
        {
            _rigidbody.velocity = Vector3.forward * _model.BulletSpeed;
        }
    }
}
