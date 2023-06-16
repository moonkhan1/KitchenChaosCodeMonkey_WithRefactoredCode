using Kitchen.Abstract.Controller;
using Unity.Netcode;
using UnityEngine;

public class MoveWithCharCont : IMover
{
    private readonly CharacterController _characterController;
    private float rotationFactor = 20.0f;

    
    public MoveWithCharCont(IEntityController entityController)
    {
        _characterController = entityController.transform.GetComponent<CharacterController>();
    }
    
    public void MoveAction(Vector3 direction, float speed)
    {
        if (direction.magnitude == 0f)
            return;
    
        Vector3 movement = direction * Time.fixedDeltaTime * speed;
    
        if (!CanMoveInDirection(movement))
        {
            if (CanMoveOnlyInX(movement))
            {
                movement = new Vector3(movement.x, 0f, 0f);
            }
            else if (CanMoveOnlyInZ(movement))
            {
                movement = new Vector3(0f, 0f, movement.z);
            }
            else
            {
                // Cannot move in any direction
                return;
            }
        }
    
        _characterController.Move(movement);
        movement.y = 0f;
        if (movement == Vector3.zero)
            return;
        _characterController.transform.rotation = Quaternion.LookRotation(movement);
    }

    private bool CanMoveInDirection(Vector3 movement)
    {
        const float playerRadius = 0.7f;
        const float playerHeight = 2f;
        var moveDistance = movement.magnitude;

        return !Physics.CapsuleCast(_characterController.transform.position, _characterController.transform.position + Vector3.up * playerHeight, playerRadius, movement, moveDistance);
    }

    private bool CanMoveOnlyInX(Vector3 movement)
    {
        const float playerRadius = 0.7f;
        const float playerHeight = 2f;
        var moveDistance = movement.magnitude;

        Vector3 moveDirX = new Vector3(movement.x, 0f, 0f);
        return (movement.x < -0.5f || movement.x > 0.5f) && !Physics.CapsuleCast(_characterController.transform.position, _characterController.transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);
    }

    private bool CanMoveOnlyInZ(Vector3 movement)
    {
        const float playerRadius = 0.7f;
        const float playerHeight = 2f;
        var moveDistance = movement.magnitude;

        Vector3 moveDirZ = new Vector3(0f, 0f, movement.z);
        return (movement.z < -0.5f || movement.z > 0.5f) && !Physics.CapsuleCast(_characterController.transform.position, _characterController.transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);
    }
    
}
