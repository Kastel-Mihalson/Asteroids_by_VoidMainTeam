using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private AsteroidModel _asteroidModel;
    private AsteroidView _asteroidView;
    private AsteroidController _asteroidController;
    private GameObject _asteroidPrefab;
    private GameObject _asteroidGameObject;
    private Rigidbody _asteroidRigidBody;

    void Start()
    {
        InitAsteroid();
        //InstanceAndMoveAsteroid();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Space is entered");
            InstanceAndMoveAsteroid();
        }
        //_asteroidController.ChangeAsteroidPosition(_asteroidGameObject, -5, 10);
    }

    public void InitAsteroid()
    {
        _asteroidPrefab = Resources.Load("AsteroidPrefab") as GameObject;
        _asteroidRigidBody = _asteroidPrefab.GetComponent<Rigidbody>();
        _asteroidView = _asteroidPrefab.GetComponent<AsteroidView>();
        _asteroidModel = new AsteroidModel();
        _asteroidController = new AsteroidController(_asteroidModel, _asteroidView);
    }

    public void InstanceAndMoveAsteroid()
    {
        _asteroidGameObject = Instantiate(
            _asteroidPrefab,
            _asteroidController.AsteroidRandomSpawnPosition(),
            Quaternion.identity);

        _asteroidController.AsteroidMove(_asteroidGameObject, _asteroidRigidBody);
    }
}
