using UnityEngine;

public sealed class AsteroidController
{
    private AsteroidModel _model;
    private AsteroidView _view;
    private AsteroidData _data;
    private Rigidbody _rigidBody;
    private GameObject _prefab;

    public AsteroidController(AsteroidData data)
    {
        _data = data;
        _prefab = data.AsteroidPrefab;
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
        _model = new AsteroidModel(_data);
        GameObject asteroidGameObject = Object.Instantiate(_prefab,
            new Vector3(Random.Range(GameModel.ScreenBorder[Border.Left], GameModel.ScreenBorder[Border.Right]), 0, 8f), 
            Quaternion.identity);
        asteroidGameObject.transform.localScale = Vector3.one * Random.Range(_model.MinSize, _model.MaxSize);
        _view = asteroidGameObject.GetComponent<AsteroidView>();
        _rigidBody = _view.Rigidbody;
        _view.Die(_model.LifeTime);
    }
}
