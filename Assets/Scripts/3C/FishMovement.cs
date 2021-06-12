using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMovement : AbstractCharacter
{
    [SerializeField]
    private GameObject fishVisual;
    private Vector2 lastMoveInput;
    private HingeJoint2D jointComponent;
    [SerializeField]
    private float eulerAngleZ;
    protected override void Start()
    {
        base.Start();
        this.jointComponent = this.GetComponent<HingeJoint2D>();
    }

    Quaternion destinationRotation;
    bool isUpsideDown = false;
    protected void Update()
    {
        if (fishVisual == null)
        {
            return;
        }

        eulerAngleZ = transform.rotation.eulerAngles.z;

        if (eulerAngleZ <= 120f || eulerAngleZ >= 270f)
        {
            if(isUpsideDown)
            {
                isUpsideDown = false;
                // fishVisual.transform.Rotate(Vector3.right, 180);

                // Quaternion origin = fishVisual.transform.rotation;
                fishVisual.transform.Rotate(Vector3.right, 180);
                // destinationRotation = fishVisual.transform.rotation;
                // fishVisual.transform.rotation = origin;
            }
        }
        else if (!isUpsideDown)
        {
            isUpsideDown = true;

            // Quaternion origin = fishVisual.transform.rotation;
            fishVisual.transform.Rotate(Vector3.right, 180);
            // destinationRotation = fishVisual.transform.rotation;
            // fishVisual.transform.rotation = origin;
        }

        // fishVisual.transform.rotation = Quaternion.LerpUnclamped(fishVisual.transform.rotation, destinationRotation, Time.deltaTime);
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
