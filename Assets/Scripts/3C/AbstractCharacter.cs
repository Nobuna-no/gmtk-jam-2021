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


    // Start is called before the first frame update
    protected virtual void Start()
    {
        this.rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
       
    }

    public virtual void ApplyMove(Vector2 move)
    {

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
