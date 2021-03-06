using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.InputSystem;

public class Controller : MonoBehaviour
{
    private delegate void ControllerDelegate();
    private event ControllerDelegate HandleInputs;

    [SerializeField] private CharacterCore core;
    [SerializeField] private AbstractCharacter tako;
    [SerializeField] private AbstractCharacter fish;

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
    [SerializeField] private string takoAction = "Jump";
    [SerializeField] private string fishAction = "ActionFish";
    [SerializeField] private string fishActionJ2 = "ActionFishJ2";
    [SerializeField] private string fishActionJ1 = "ActionFishJ1";


    [Header("Debug")]
    [SerializeField] private Checkpoint[] checkpoints;


    private Vector2 takoMouvement = Vector2.zero;
    private Vector2 fishMouvement = Vector2.zero;

    private float horizontalP1;
    private float horizontalP2;
    private float verticalP1;
    private float verticalP2;

    private bool actionFishDown;
    private bool actionFishUp;

    private void Start()
    {
        // this.character = this.characterComponent.GetComponent<ICharacter>();
        InitController();

        checkpoints = FindObjectsOfType<Checkpoint>();
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
        actionFishDown = Input.GetButtonDown(fishAction);
        actionFishUp = Input.GetButtonUp(fishAction);
    }

    //DOUBLE JOYPAD
    private void MultiplayerActionsConfig2()
    {
        horizontalP1 = Input.GetAxisRaw(horizontalGamepadNameJ1);
        horizontalP2 = Input.GetAxisRaw(horizontalGamepadNameJ2);
        verticalP1 = Input.GetAxisRaw(verticalGamepadNameJ1);
        verticalP2 = Input.GetAxisRaw(verticalGamepadNameJ2);
        actionFishDown = Input.GetButtonDown(fishActionJ2);
        actionFishUp = Input.GetButtonUp(fishActionJ2);
    }

    //J1: KEYBOARD - J2: JOYPAD
    private void MultiplayerActionsConfig3()
    {
        horizontalP1 = Input.GetAxisRaw(horizontalKeyboardNameP1);
        horizontalP2 = Input.GetAxisRaw(horizontalGamepadNameJ2);
        verticalP1 = Input.GetAxisRaw(verticalKeyboardNameJ1);
        verticalP2 = Input.GetAxisRaw(verticalGamepadNameJ2);
        actionFishDown = Input.GetButtonDown(fishActionJ2);
        actionFishUp = Input.GetButtonUp(fishActionJ2);
    }

    //J1: JOYPAD - J2: KEYBOARD
    private void MultiplayerActionsConfig4()
    {
        horizontalP1 = Input.GetAxisRaw(horizontalGamepadNameJ1);
        horizontalP2 = Input.GetAxisRaw(horizontalKeyboardNameP1);
        verticalP1 = Input.GetAxisRaw(verticalGamepadNameJ1);
        verticalP2 = Input.GetAxisRaw(verticalKeyboardNameJ1);
        actionFishDown = Input.GetButtonDown(fishAction);
        actionFishUp = Input.GetButtonUp(fishAction);
    }

    //SOLO
    private void SinglePlayerActions()
	{
        horizontalP1 = Input.GetAxisRaw(horizontalKeyboardNameP1) != 0 ? Input.GetAxisRaw(horizontalKeyboardNameP1) : Input.GetAxisRaw(horizontalGamepadNameJ1);
        horizontalP2 = Input.GetAxisRaw(horizontalKeyboardNameP2) != 0 ? Input.GetAxisRaw(horizontalKeyboardNameP2) : Input.GetAxisRaw(horizontal2GamepadNameJ1);
        verticalP1 = Input.GetAxisRaw(verticalKeyboardNameJ1) != 0 ? Input.GetAxisRaw(verticalKeyboardNameJ1) : Input.GetAxisRaw(verticalGamepadNameJ1);
        verticalP2 = Input.GetAxisRaw(verticalKeyboardNameJ2) != 0 ? Input.GetAxisRaw(verticalKeyboardNameJ2) : Input.GetAxisRaw(vertical2GamepadNameJ1);
        actionFishDown = Input.GetButtonDown(fishAction) || Input.GetButtonDown(fishActionJ1);
        actionFishUp = Input.GetButtonUp(fishAction) || Input.GetButtonUp(fishActionJ1);
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

        tako.ApplyMove(takoMouvement);
        fish.ApplyMove(fishMouvement);

        if (Input.GetButtonDown(takoAction))
            tako.StartAction();

        if (Input.GetButtonUp(takoAction))
            tako.StopAction();

        if (actionFishDown)
            fish.StartAction();

        if (actionFishUp)
            fish.StopAction();


#if UNITY_EDITOR
        TryTeleport(KeyCode.F1, 0);
        TryTeleport(KeyCode.F2, 1);
        TryTeleport(KeyCode.F3, 2);
        TryTeleport(KeyCode.F4, 3);
        TryTeleport(KeyCode.F5, 4);
        TryTeleport(KeyCode.F6, 5);
        TryTeleport(KeyCode.F7, 6);
        TryTeleport(KeyCode.F8, 7);
        TryTeleport(KeyCode.F9, 8);
        TryTeleport(KeyCode.F10, 9);
        TryTeleport(KeyCode.F11, 10);
        TryTeleport(KeyCode.F12, 11);
#endif
    }


    private void TryTeleport(KeyCode key, int index)
    {
        if (Input.GetKeyDown(key) && checkpoints.Length > index)
        {
            core.Teleport(checkpoints[index].transform.position);
        }
    }
}
