using UnityEngine;

[CreateAssetMenu(fileName = "ShipData", menuName = "ScriptableObjects/ShipData", order = 1)]
public sealed class ShipData : ScriptableObject
{
    [SerializeField] private float _moveSpeed = 10f;
    [SerializeField] private float _turnSpeed = 4f;
    [SerializeField] private Vector3 _startPosition = new Vector3(0f, 0f, -4.5f);
    [SerializeField] private GameObject _shipPrefab;

    public float MoveSpeed => _moveSpeed;
    public float TurnSpeed => _turnSpeed;
    public Vector3 StartPosition => _startPosition;
    public GameObject ShipPrefab => _shipPrefab;
}