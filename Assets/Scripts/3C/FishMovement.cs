using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMovement : AbstractCharacter
{
    [SerializeField]
    private float torqueForce = 1;
    [SerializeField]
    float angle;
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
        
        this.rb.AddForce(move * speed);
        
        this.rb.AddTorque(move * speed);

        // float angle = Mathf.Atan2(move.x, move.y) * Mathf.Rad2Deg * Time.deltaTime;
        // rb.AddTorque(angle * torqueForce);
        // angle = Vector2.Angle(transform.rotation.eulerAngles, move);
        // this.rb.AddTorque(angle * torqueForce);
    }
}
