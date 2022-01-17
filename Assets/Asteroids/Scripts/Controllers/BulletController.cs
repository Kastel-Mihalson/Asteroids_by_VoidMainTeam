using UnityEngine;


public class BulletController
{
    private BulletModel _model;
    private BulletView _view;
    private BulletData _data;
    private Rigidbody _rigidbody;
    private GameObject _prefab;
    private Transform _spawnPosition;
    private GameObjectPool _bulletPool;

    public BulletController(BulletData data, Transform spawnPosition)
    {
        _data = data;
        _prefab = data.Prefab;
        _spawnPosition = spawnPosition;
        _bulletPool = new GameObjectPool(_prefab);
    }

    public void Init()
    {
        _model = new BulletModel(_data);
        GameObject bulletGameObject = _bulletPool.GetGameObject();
        bulletGameObject.transform.position = _spawnPosition.position;
        _view = bulletGameObject.GetComponent<BulletView>();
        _rigidbody = _view.Rigidbody;
    }

    public void Move()
    {
        if (_rigidbody)
        {
            _rigidbody.velocity = _spawnPosition.forward * _model.BulletSpeed;
            //_view.Die(_model.LifeTime);
        }
    }

    public void AddToQueue(GameObject bullet)
    {
        _bulletPool.AddToQueue(bullet);
    }

    public void OnEnable()
    {
        _view.ReturnBulletToPoolEvent += AddToQueue;
        _view.GetBulletDamageEvent += GetDamage;
    }

    public void OnDisable()
    {
        _view.ReturnBulletToPoolEvent -= AddToQueue;
        _view.GetBulletDamageEvent -= GetDamage;
    }

    private int? GetDamage()
    {        
        return _model.Damage;
    }
}
