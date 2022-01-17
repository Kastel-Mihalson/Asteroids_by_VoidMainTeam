using UnityEngine;

public sealed class ShipModel
{
    private float _moveSpeed;
    private float _turnSpeed;
    private Transform _bulletSpawnPosition;

    public float MoveSpeed
    {
        get => _moveSpeed;
        private set => _moveSpeed = value;
    }
    public float Turn
    {
        get => _turnSpeed;
        private set => _turnSpeed = value;
    }

    public Transform BulletSpawnPoint
    {
        get => _bulletSpawnPosition;
        set => _bulletSpawnPosition = value; 
    }

    public ShipModel(ShipData data)
    {
        _moveSpeed = data.MoveSpeed;
        _turnSpeed = data.TurnSpeed;

        var spawnObject = Object.FindObjectOfType<BulletSpawnMarker>().gameObject;

        if (spawnObject != null)
        {
            _bulletSpawnPosition = spawnObject.transform;
        }
    }
}
