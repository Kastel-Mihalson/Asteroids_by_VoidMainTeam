using UnityEngine;

public class ShipView : MonoBehaviour, IInteractiveObject, IShip
{
    public Rigidbody Rigidbody => gameObject.GetComponent<Rigidbody>();
}