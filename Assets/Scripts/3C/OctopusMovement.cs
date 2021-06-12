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

    private Vector2 lastDirection;

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
        if (move != Vector2.zero)
        {
            this.lastDirection = move;
        }

        this.rb.AddForce(move * speed);
    }
}
