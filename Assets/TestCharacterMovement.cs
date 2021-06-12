using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacter
{
    void ApplyMove(Vector2 move);
    void StartAction();
    void StopAction();
}

[RequireComponent(typeof(Rigidbody2D))]
public class TestCharacterMovement : MonoBehaviour, ICharacter
{
    [SerializeField]
    protected float speed = 0.5f;
    [SerializeField]
    protected float propulsionForce = 5;
    [SerializeField]
    private Transform backTransform;

    protected Rigidbody2D rb;
    private bool actionIsActive = false;

    private Vector2 lastDirection;

    public enum PropulsionType
    {
        LastDirection,
        Forward,
        VectorToPoisson
    }

    [SerializeField]
    private PropulsionType propulsionType = PropulsionType.Forward;
    
    // Start is called before the first frame update
    void Start()
    {
        this.rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (this.actionIsActive)
        {
            switch(propulsionType)
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

    public virtual void ApplyMove(Vector2 move)
    {
        if (move != Vector2.zero)
        {
            this.lastDirection = move;
        }

        this.rb.AddForce(move * speed);
    }

    public virtual void StartAction()
    {
        this.actionIsActive = true;
    }

    public virtual void StopAction()
    {
        this.actionIsActive = false;
    }
}
