using System.Collections;
using System.Collections.Generic;
using Kitchen.Abstract.Controller;
using UnityEngine;

public class MoveWithCharCont : IMover
{
    private readonly CharacterController _characterController;
    private float rotationFactor = 15.0f;

    
    public MoveWithCharCont(IEntityController entityController)
    {
        _characterController = entityController.transform.GetComponent<CharacterController>();
    }
    public void MoveAction(Vector3 direction, float speed)
    {
        if(direction.magnitude == 0f) return;
        //Vector3 worldPosition = _characterController.transform.TransformDirection(direction);
        
        // Detection objects in front of our Player
        var moveDistance = speed * Time.fixedDeltaTime;
        const float playerRadius = 0.7f;
        const float playerHeight = 2f;
        
        Vector3 movement = direction * Time.fixedDeltaTime * speed;
        bool canMove =  !Physics.CapsuleCast(_characterController.transform.position,_characterController.transform.position + Vector3.up *  playerHeight, playerRadius, movement, moveDistance);

        // For Diagonal movements
        if(!canMove)
        {
            // Can not move towards movement
            //Attempts only X movement
            Vector3 moveDirX = new Vector3(movement.x, 0, 0);
            canMove = movement.x != 0 && !Physics.CapsuleCast(_characterController.transform.position,_characterController.transform.position + Vector3.up *  playerHeight, playerRadius, moveDirX, moveDistance);

            if (canMove)
            {
                // Can move only on the X
                movement = moveDirX;
            }
            else
            {
                // Con not move only on the X
                
                // Attempt ony Z movement
                Vector3 moveDirZ = new Vector3(0, 0, movement.z);
                canMove = movement.z != 0 && !Physics.CapsuleCast(_characterController.transform.position,_characterController.transform.position + Vector3.up *  playerHeight, playerRadius, moveDirZ, moveDistance);
                if (canMove)
                {
                    // Can move only on the Z
                    movement = moveDirZ;
                }
                else
                {
                    // Con not move in any direction
                }
            }
        }
        
        if (canMove)
        {
            _characterController.Move(movement);
            movement.y = 0;
            if (movement == Vector3.zero) return;
            _characterController.transform.rotation = Quaternion.LookRotation(movement);
        }
        //_characterController.transform.forward = Vector3.Slerp(_characterController.transform.forward, movement,rotationFactor* Time.deltaTime);
    }

   
    
  
}
