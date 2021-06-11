using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.InputSystem;

public class Controller : MonoBehaviour
{
    private Vector2 movementAction = Vector2.zero;
    private bool featureAction = false;

    public void MovementActionCallback(InputAction.CallbackContext context)
    {
        this.movementAction = context.ReadValue<Vector2>();
    }

    public void JumpActionCallback(InputAction.CallbackContext context)
    {
        this.featureAction = context.ReadValue<int>() == 0;

        // if (featureAction)
        // {
        // start feature...
        // }
        // else
        // {
        // stop feature...
        // }
    }
}
