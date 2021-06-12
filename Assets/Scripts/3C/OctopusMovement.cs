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
    protected float inkBurstForce = 10;
    [SerializeField]
    protected float inkPropulsionForce = 5;

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

    protected override void Start()
    {
        base.Start();

#if !UNITY_EDITOR
        Debug.Assert(InkBurstParticleSystem != null);
        Debug.Assert(InkContinuousParticleSystem != null);
#endif

        this.inkContinuousParticleSystem.Stop();
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (this.actionIsActive)
        {
            this.rb.AddForce((transform.position - backTransform.position) * inkPropulsionForce);
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

    public override void StartAction()
    {
        base.StartAction();

        this.rb.AddForce((transform.position - backTransform.position) * inkBurstForce, ForceMode2D.Impulse);
        
        if (this.inkBurstParticleSystem)
        {
            this.inkBurstParticleSystem.Play();
        }

        this.inkContinuousParticleSystem?.Play();
    }

    public override void StopAction()
    {
        base.StopAction();

        this.inkContinuousParticleSystem?.Stop();
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, -this.transform.right);

        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, this.lastMoveInput);
    }
}
