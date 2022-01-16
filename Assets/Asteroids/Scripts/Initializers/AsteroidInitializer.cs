using UnityEngine;

public class AsteroidInitializer
{
    private float _leftScreenBorder;
    private float _rightScreenBorder;

    private AsteroidData _asteroid;
    private GameObject _asteroidPrefab;
    private AsteroidModel _asteroidModel;
    private AsteroidView _asteroidView;
    private AsteroidController _asteroidController;

    public AsteroidModel ShipModel => _asteroidModel;
    public AsteroidView ShipView => _asteroidView;
    public AsteroidController ShipController => _asteroidController;

    public AsteroidInitializer(float leftScreenBorder, float rightScreenBorder)
    {
        _leftScreenBorder = leftScreenBorder;
        _rightScreenBorder = rightScreenBorder;
    }

    public void InitAsteroid(AsteroidData asteroid)
    {
        _asteroid = asteroid;
        _asteroidPrefab = asteroid.AsteroidPrefab;
        GameObject asteroidGameObject = Object.Instantiate(_asteroidPrefab,
            new Vector3(Random.Range(_leftScreenBorder, _rightScreenBorder), 0, 8f), 
            Quaternion.identity);

        _asteroidModel = new AsteroidModel(_asteroid);
        _asteroidView = asteroidGameObject.GetComponent<AsteroidView>();

        asteroidGameObject.transform.localScale = Vector3.one * Random.Range(
            _asteroidModel.MinSize, 
            _asteroidModel.MaxSize);

        _asteroidController = new AsteroidController(_asteroidModel, _asteroidView);
        _asteroidView.Die(_asteroidModel.LifeTime);
        _asteroidController.AsteroidMove();
    }
}
