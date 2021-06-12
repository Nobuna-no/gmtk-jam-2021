using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.InputSystem;

public class Controller : MonoBehaviour
{
    private delegate void ControllerDelegate();
    private event ControllerDelegate HandleInputs;

    [SerializeField] private AbstractCharacter poulpe;
    [SerializeField] private AbstractCharacter poisson;

    [Header("Axis Names")]
    [SerializeField] private string horizontalKeyboardNameP1 = "Horizontal";
    [SerializeField] private string horizontalKeyboardNameP2 = "HorizontalP2";
    [SerializeField] private string verticalKeyboardNameJ1 = "Vertical";
    [SerializeField] private string verticalKeyboardNameJ2 = "VerticalP2";
    [SerializeField] private string horizontalGamepadNameJ1 = "HorizontalJ1";
    [SerializeField] private string verticalGamepadNameJ1 = "VerticalJ1";
    [SerializeField] private string vertical2GamepadNameJ1 = "Vertical2J1";
    [SerializeField] private string horizontal2GamepadNameJ1 = "Horizontal2J1";
    [SerializeField] private string horizontalGamepadNameJ2 = "HorizontalJ2";
    [SerializeField] private string verticalGamepadNameJ2 = "VerticalJ2";

    private Vector2 takoMouvement = Vector2.zero;
    private Vector2 fishMouvement = Vector2.zero;

    private float horizontalP1;
    private float horizontalP2;
    private float verticalP1;
    private float verticalP2;

    private bool featureAction = false;
    private ICharacter character = null;
    private void Start()
    {
        // this.character = this.characterComponent.GetComponent<ICharacter>();
        InitController();
    }

    private void InitController()
	{
        int j1Mode = PlayerPrefs.GetInt("P1Controls");
        if (j1Mode != 0)
        {
            int j2Mode = PlayerPrefs.GetInt("P2Controls");
            if (j1Mode == 1)
			{
                if (j2Mode == 0)
                    HandleInputs = MultiplayerActionsConfig1;
                else
                    HandleInputs = MultiplayerActionsConfig3;
			}
            else
			{
                if (j2Mode == 0)
                    HandleInputs = MultiplayerActionsConfig4;
                else
                    HandleInputs = MultiplayerActionsConfig2;
            }
        }
        else
            HandleInputs = SinglePlayerActions;
	}

    //DOUBLE KEYBOARD
    private void MultiplayerActionsConfig1()
	{
        horizontalP1 = Input.GetAxisRaw(horizontalKeyboardNameP1);
        horizontalP2 = Input.GetAxisRaw(horizontalKeyboardNameP2);
        verticalP1 = Input.GetAxisRaw(verticalKeyboardNameJ1);
        verticalP2 = Input.GetAxisRaw(verticalKeyboardNameJ2);
    }

    //DOUBLE JOYPAD
    private void MultiplayerActionsConfig2()
    {
        horizontalP1 = Input.GetAxisRaw(horizontalGamepadNameJ1);
        horizontalP2 = Input.GetAxisRaw(horizontalGamepadNameJ2);
        verticalP1 = Input.GetAxisRaw(verticalGamepadNameJ1);
        verticalP2 = Input.GetAxisRaw(verticalGamepadNameJ2);
    }

    //J1: KEYBOARD - J2: JOYPAD
    private void MultiplayerActionsConfig3()
    {
        horizontalP1 = Input.GetAxisRaw(horizontalKeyboardNameP1);
        horizontalP2 = Input.GetAxisRaw(horizontalGamepadNameJ2);
        verticalP1 = Input.GetAxisRaw(verticalKeyboardNameJ1);
        verticalP2 = Input.GetAxisRaw(verticalGamepadNameJ2);
    }

    //J1: JOYPAD - J2: KEYBOARD
    private void MultiplayerActionsConfig4()
    {
        horizontalP1 = Input.GetAxisRaw(horizontalGamepadNameJ1);
        horizontalP2 = Input.GetAxisRaw(horizontalKeyboardNameP1);
        verticalP1 = Input.GetAxisRaw(verticalGamepadNameJ1);
        verticalP2 = Input.GetAxisRaw(verticalKeyboardNameJ1);
    }

    //SOLO
    private void SinglePlayerActions()
	{
        horizontalP1 = Input.GetAxisRaw(horizontalKeyboardNameP1) != 0 ? Input.GetAxisRaw(horizontalKeyboardNameP1) : Input.GetAxisRaw(horizontalGamepadNameJ1);
        horizontalP2 = Input.GetAxisRaw(horizontalKeyboardNameP2) != 0 ? Input.GetAxisRaw(horizontalKeyboardNameP2) : Input.GetAxisRaw(horizontal2GamepadNameJ1);
        verticalP1 = Input.GetAxisRaw(verticalKeyboardNameJ1) != 0 ? Input.GetAxisRaw(verticalKeyboardNameJ1) : Input.GetAxisRaw(verticalGamepadNameJ1);
        verticalP2 = Input.GetAxisRaw(verticalKeyboardNameJ2) != 0 ? Input.GetAxisRaw(verticalKeyboardNameJ2) : Input.GetAxisRaw(vertical2GamepadNameJ1);
    }

	private void Update()
    {
        HandleInputs?.Invoke();

        takoMouvement = Vector2.zero;
        takoMouvement.x = horizontalP1;
        takoMouvement.y = verticalP1;

        fishMouvement = Vector2.zero;
        fishMouvement.x = horizontalP2;
        fishMouvement.y = verticalP2;

        poulpe.ApplyMove(takoMouvement);
        poisson.ApplyMove(fishMouvement);

        if (Input.GetButtonDown("Jump"))
            poulpe.StartAction();

        if (Input.GetButtonUp("Jump"))
            poulpe.StopAction();

    }

}
