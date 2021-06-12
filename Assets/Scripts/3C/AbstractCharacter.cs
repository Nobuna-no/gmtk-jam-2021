using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacter
{
    void Kill();
    bool IsDead();

    void ApplyMove(Vector2 move);
    void StartAction();
    void StopAction();

    void Respawn(Vector3 position);
}

[RequireComponent(typeof(Rigidbody2D))]
public abstract class AbstractCharacter : MonoBehaviour, ICharacter
{
    [SerializeField]
    protected float movementSpeed = 0.5f;
    [SerializeField]
    protected float torqueSpeed = 1;
    [SerializeField]
    protected float angle;

    protected Rigidbody2D rb;
    protected bool actionIsActive = false;

    private bool isAlive = true;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        this.rb = GetComponent<Rigidbody2D>();
    }

    public virtual void Kill()
    {
        this.StopAction();
        this.isAlive = false;
    }

    public virtual bool IsDead()
    {
        return !isAlive;
    }

    public void ApplyMove(Vector2 move)
    {
        if (!this.isAlive)
        {
            return;
        }

        this.ApplyMoveInternal(move);
    }

    public void StartAction()
    {
        if (!this.isAlive)
        {
            return;
        }

        this.actionIsActive = true;
        this.StartActionInternal();
    }

    public void StopAction()
    {
        if (!this.isAlive)
        {
            return;
        }

        this.actionIsActive = false;
        this.StopActionInternal();
    }

    protected virtual void ApplyMoveInternal(Vector2 move)
    { }

    protected virtual void StartActionInternal()
    { }

    protected virtual void StopActionInternal()
    { }


    public virtual void Respawn(Vector3 position)
    {
        rb.rotation = 0;
        rb.position = position;
        this.isAlive = true;
    }

}
