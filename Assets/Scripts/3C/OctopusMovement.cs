using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OctopusMovement : AbstractCharacter
{
    public enum PropulsionType
    {
        LastDirection,
        Forward,
        VectorToPoisson
    }

    [SerializeField]
    private PropulsionType propulsionType = PropulsionType.Forward;

    [SerializeField]
    protected float propulsionForce = 5;

    [SerializeField]
    private Transform backTransform;

    [SerializeField]
    private bool inverseControl = false;

    private Vector2 lastDirection; 
    private Vector2 lastMoveInput;    

    // Update is called once per frame
    protected override void Update()
    {
        if (this.actionIsActive)
        {
            switch (propulsionType)
            {
                case PropulsionType.Forward:
                    this.rb.AddForce((transform.position - backTransform.position) * propulsionForce);
                    break;
                case PropulsionType.LastDirection:
                    this.rb.AddForce(lastDirection * propulsionForce);
                    break;
                case PropulsionType.VectorToPoisson:
                    break;
            }
        }
    }

    public override void ApplyMove(Vector2 move)
    {
        if (inverseControl)
        {
            move *= -1;
        }

        if (move != Vector2.zero)
        {
            this.lastDirection = move;
        }

        this.rb.AddForce(move * movementSpeed);

        angle = Vector2.SignedAngle(-this.transform.right, move) * Time.deltaTime;
        this.rb.AddTorque(angle * torqueSpeed);

        lastMoveInput = move;
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, -this.transform.right);

        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, this.lastMoveInput);
    }
}
