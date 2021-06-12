using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMovement : AbstractCharacter
{
    private Vector2 lastMoveInput;

    // Update is called once per frame
    protected override void ApplyMoveInternal(Vector2 move)
    {
        this.rb.AddForce(move * movementSpeed);
        
        angle = Vector2.SignedAngle(this.transform.right, move) * Time.deltaTime;
        this.rb.AddTorque(angle * torqueSpeed);

        lastMoveInput = move;
    }

    protected void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, this.transform.right);
        
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, this.lastMoveInput);
    }
}
