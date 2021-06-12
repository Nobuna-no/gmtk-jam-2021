using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMovement : AbstractCharacter
{
    private Vector2 lastMoveInput;
    private HingeJoint2D jointComponent;

    protected override void Start()
    {
        base.Start();
        this.jointComponent = this.GetComponent<HingeJoint2D>();
    }

    public override void Kill()
    {
        this.jointComponent.enabled = false;
        base.Kill();
    }

    public override void Respawn(Vector3 position)
    {
        base.Respawn(position);
     
        // Move then reactive the joint.
        this.jointComponent.enabled = true;
    }

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
