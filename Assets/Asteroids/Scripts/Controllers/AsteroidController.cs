using UnityEngine;

public sealed class AsteroidController
{
    private AsteroidModel _model;
    private AsteroidView _view;
    private Rigidbody _rigidBody;
    private GameObject _prefab;
    private float _leftScreenBorder;
    private float _rightScreenBorder;

    public AsteroidController(AsteroidData data, float leftScreenBorder, float rightScreenBorder)
    {
        _model = new AsteroidModel(data);
        _prefab = data.AsteroidPrefab;
        _leftScreenBorder = leftScreenBorder;
        _rightScreenBorder = rightScreenBorder;
    }

    public void Move()
    {
        if (_rigidBody)
        {
            _rigidBody.angularVelocity = Random.insideUnitSphere * _model.RotationSpeed;
            _rigidBody.velocity = Vector3.back * Random.Range(_model.MinMoveSpeed, _model.MaxMoveSpeed);
        }
    }

    public void Init()
    {
        GameObject asteroidGameObject = Object.Instantiate(_prefab,
            new Vector3(Random.Range(_leftScreenBorder, _rightScreenBorder), 0, 8f), 
            Quaternion.identity);
        _view = asteroidGameObject.GetComponent<AsteroidView>();
        _rigidBody = _view.Rigidbody;
        asteroidGameObject.transform.localScale = Vector3.one * Random.Range(_model.MinSize, _model.MaxSize);
        _view.Die(_model.LifeTime);
    }
}
