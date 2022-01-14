using UnityEngine;


[CreateAssetMenu]
public sealed class ShipData : ScriptableObject
{
    [SerializeField] private float _moveSpeed = 10f;
    [SerializeField] private float _turnSpeed = 4f;
    [SerializeField] private GameObject _prefab;
    [SerializeField] private Vector3 _startPosition = new Vector3(0f, 0f, -4.5f);

    public float MoveSpeed => _moveSpeed;
    public float TurnSpeed => _turnSpeed;
    public GameObject Prefab => _prefab;
    public Vector3 StartPosition => _startPosition;
}