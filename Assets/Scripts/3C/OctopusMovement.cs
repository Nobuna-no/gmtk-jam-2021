using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OctopusMovement : AbstractCharacter
{
    [SerializeField]
    protected float inkBurstForce = 10;
    [SerializeField]
    protected AnimationCurve inkPropulsionCurve;
    [SerializeField, Min(0.01f)]
    protected float maxPropulsionIncreaseDuration = 1f;
    [SerializeField, Min(0.01f)]
    protected float propulsionIncreaseResetDuration = 1f;

    [SerializeField]
    private Transform backTransform;

    [SerializeField]
    private bool inverseControl = false;

    [SerializeField]
    private ParticleSystem inkBurstParticleSystem;
    [SerializeField]
    private ParticleSystem inkContinuousParticleSystem;

    private Vector2 lastDirection; 
    private Vector2 lastMoveInput;

    private float propulsionIncreaseTime = 0f;

    protected override void Start()
    {
        base.Start();

        this.inkContinuousParticleSystem.Stop();
    }

    protected void FixedUpdate()
    {
        if (this.IsDead())
        {
            return;
        }

        if (this.actionIsActive)
            AddForce(inkPropulsionCurve.Evaluate(propulsionIncreaseTime / maxPropulsionIncreaseDuration));

        float dtCoef = this.actionIsActive ? 1f : - maxPropulsionIncreaseDuration / propulsionIncreaseResetDuration;
        propulsionIncreaseTime = Mathf.Clamp(propulsionIncreaseTime + Time.deltaTime * dtCoef, 0, maxPropulsionIncreaseDuration);
        // print("add="+Time.deltaTime * dtCoef + ", time=" + propulsionIncreaseTime);
    }

    protected override void ApplyMoveInternal(Vector2 move)
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

    protected override void StartActionInternal()
    {
        AddForce(inkBurstForce, ForceMode2D.Impulse);       

        if (this.inkBurstParticleSystem)
        {
            this.inkBurstParticleSystem.Play();
        }

        this.inkContinuousParticleSystem?.Play();
    }

    protected override void StopActionInternal()
    {
        this.inkContinuousParticleSystem?.Stop();
    }

    private void AddForce(float force, ForceMode2D forceMode = ForceMode2D.Force)
    {
        this.rb.AddForce((transform.position - backTransform.position) * force, forceMode);
    }

    protected void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, -this.transform.right);

        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, this.lastMoveInput);
    }
}
