using UnityEngine;


public class ShipView : MonoBehaviour
{
    public Rigidbody Rigidbody => gameObject.GetComponent<Rigidbody>();
}
