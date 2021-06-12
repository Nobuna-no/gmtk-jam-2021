using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMovement : AbstractCharacter
{
    private Vector2 lastMoveInput;

    // Update is called once per frame
    public override void ApplyMove(Vector2 move)
    {
        // TODO: Find a way to apply the torque for the fist

        //float angularChangeInDegrees = 0;
        //if (move.y > 0)
        //{
        //    angularChangeInDegrees += torqueForce;
        //}
        //if (move.y < 0)
        //{
        //    angularChangeInDegrees -= torqueForce;
        //}

        //var impulse = (angularChangeInDegrees * Mathf.Deg2Rad) * rb.inertia;
        
        this.rb.AddForce(move * movementSpeed);


        angle = Vector2.SignedAngle(this.transform.right, move) * Time.deltaTime;
        this.rb.AddTorque(angle * torqueSpeed);

        lastMoveInput = move;
        // float angle = Mathf.Atan2(move.x, move.y) * Mathf.Rad2Deg * Time.deltaTime;
        // rb.AddTorque(angle * torqueForce);
        // angle = Vector2.Angle(transform.rotation.eulerAngles, move);
        // this.rb.AddTorque(angle * torqueForce);
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, this.transform.right);
        
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, this.lastMoveInput);
    }
}
