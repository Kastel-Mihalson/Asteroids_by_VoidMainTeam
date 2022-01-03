using UnityEngine;


public sealed class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 10f;
    [SerializeField] private float _roleAngle = 15f;
    [SerializeField] private float _roleSpeed = 20f;
    [SerializeField] private float _screenSizeTop = 5f;
    [SerializeField] private GameObject _shot;
    [SerializeField] private Transform _shotSpawn;
    [SerializeField] private float _shotDelay = 2f;

    private float _nextShot = 0f;
    private Ship _ship;

    private void Start()
    {
        IMove movement = new ShipMovement(transform, _speed);
        IRole rolling = new ShipRolling(transform, _roleAngle, _roleSpeed);

        _ship = new Ship(movement, rolling);
    }

    private void Update()
    {
        _ship.Move(Input.GetAxis(AxisManager.HORIZONTAL), Input.GetAxis(AxisManager.VERTICAL),
            Time.deltaTime);
        _ship.Role(Input.GetAxis(AxisManager.HORIZONTAL), Time.deltaTime);

        if (Input.GetKey(KeyCode.Space) && Time.time > _nextShot)
        {
            _nextShot = Time.time + _shotDelay;
            Instantiate(_shot, _shotSpawn.position, Quaternion.Euler(0, 0, 0));
        }        
    }
}
