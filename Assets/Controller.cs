using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.InputSystem;

public class Controller : MonoBehaviour
{
    [SerializeField]
    private TestCharacterMovement poulpe;
    [SerializeField]
    private TestCharacterMovement poisson;

    private Vector2 movementAction = Vector2.zero;
    private bool featureAction = false;
    private ICharacter character = null;

    private void Start()
    {
        // this.character = this.characterComponent.GetComponent<ICharacter>();
    }

    private void Update()
    {
        // this.character.ApplyMove(this.movementAction);
        this.movementAction = Vector2.zero;

        if (Input.GetKey(KeyCode.W))
        {
            this.movementAction.y += 1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            this.movementAction.x -= 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            this.movementAction.y -= 1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            this.movementAction.x += 1;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            this.poulpe.StartAction();
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            this.poulpe.StopAction();
        }
        this.poulpe.ApplyMove(this.movementAction);

        
        this.movementAction = Vector2.zero;

        if (Input.GetKey(KeyCode.UpArrow))
        {
            this.movementAction.y += 1;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            this.movementAction.x -= 1;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            this.movementAction.y -= 1;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            this.movementAction.x += 1;
        }

        this.poisson.ApplyMove(this.movementAction);
    }

    public void MovementActionCallback(InputAction.CallbackContext context)
    {
        this.movementAction = context.ReadValue<Vector2>();
    }

    public void JumpActionCallback(InputAction.CallbackContext context)
    {
        this.featureAction = context.ReadValue<int>() == 0;

        if (featureAction)
        {
            this.character.StartAction();
        }
        else
        {
            this.character.StopAction();
        }
    }
}
