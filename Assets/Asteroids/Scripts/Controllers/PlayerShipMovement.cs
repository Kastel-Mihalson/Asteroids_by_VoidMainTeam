using UnityEngine;

public sealed class PlayerShipMovement : MovementController
{ 
    public override Vector3 GetMovementDirection()
    {
        float vertical = Input.GetAxis(AxisManager.VERTICAL);
        float horizontal = Input.GetAxis(AxisManager.HORIZONTAL);
        return new Vector3(horizontal, 0, vertical);
    }
}
