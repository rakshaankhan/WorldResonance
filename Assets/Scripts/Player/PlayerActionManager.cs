using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;


public class PlayerActionManager : MonoBehaviour
{

    [SerializeField]
    private GameEvent interactGameEvent;

    [SerializeField]
    public bool disabledMovement;

    public Vector2 moveValue { get; private set; }
    public bool jumpValue { get; private set; }
    public bool glideValue { get; private set; }
    public bool interactValue { get; private set; }
    public bool escapeValue { get; private set; }
    public PlayerInstrument.InstrumentType selectedInstrument { get; private set; } = PlayerInstrument.InstrumentType.Wind;

    private PlayerInput input;

    private List<Action> startActionList;
    private List<Action<InputAction.CallbackContext>> performedActionList;
    private List<Action> cancelActionList;


    private PlayerInstrument playerInstrument;
    private void Awake()
    {
        input = GetComponent<PlayerInput>();

        startActionList = new List<Action>();
        performedActionList = new();
        cancelActionList = new List<Action>();
        playerInstrument = GetComponent<PlayerInstrument>();
    }
    private void OnEnable()
    {
        if (TryGetComponent(out PlayerInput input))
        {

        }
        input.actions.FindActionMap("UI").Enable();
        ManageActions(input, true);
    }

    private void OnDisable()
    {
        ManageActions(input, false);
        performedActionList.Clear();
        // if (TryGetComponent(out PlayerInput input)) {  }
    }


    void ManageActions(PlayerInput input, bool onEnable)
    {

        AssignCallbacks(input, "Move", SetMove, null, started: null, context => context.ReadValue<Vector2>(), onEnable);
        AssignCallbacks(input, "Jump", SetJump, OnJumpCancel, started: null, context => context.ReadValueAsButton(), onEnable);
        AssignCallbacks(input, "glide", SetGlide, SetGlide, null, context => context.ReadValueAsButton(), onEnable);
        AssignCallbacks(input, "interact", performed: null, OnInteractStart, null, context => context, onEnable);
        AssignCallbacks(input, "Select Instrument", null, null, OnSelectInstrumentName, context => context, onEnable);
        AssignCallbacks(input, "Play Note", OnPlayNote6, null, null, context => context, onEnable);

        AssignCallbacks(input, "PauseMenu", OnEscape, null, null, context => context, onEnable);
    }

    public void AssignCallbacks<T>(PlayerInput input, string actionName, Action<T> performed = null, Action<T> canceled = null, Action<T> started = null, Func<InputAction.CallbackContext, T> converter = null, bool enable = true)
    {
        InputAction action = input.actions[actionName];
        if (action != null)
        {
            if (enable)
            {//TODO this get very complicated  and dirty very quickly.


                Action<InputAction.CallbackContext> performedAction = context => OnActionTaken(context, performed, converter);
                performedActionList.Add(performedAction);
                action.performed += performedAction;

                Action<InputAction.CallbackContext> canceledAction = context => OnActionTaken(context, canceled, converter);
                performedActionList.Add(canceledAction);
                action.canceled += canceledAction;

                Action<InputAction.CallbackContext> startedAction = context => OnActionTaken(context, started, converter);
                performedActionList.Add(startedAction);
                action.started += startedAction;


            }
            else
            {
                //TODO Is there any negative outcome from unsubing everything?
                foreach (var performedRemove in performedActionList)
                {
                    action.performed -= performedRemove;
                    action.canceled -= performedRemove;
                    action.started -= performedRemove;
                }
            }
        }
    }
    private void OnActionTaken<T>(InputAction.CallbackContext context, Action<T> action, Func<InputAction.CallbackContext, T> buttonData)
    {
        if (action == null) return;

        action(buttonData(context));
    }




    void SetMove(Vector2 value)
    {
        moveValue = value;
        if ((disabledMovement == true)) moveValue = Vector2.zero;
    }
    void SetJump(bool value)
    {
        jumpValue = value;
        if ((disabledMovement == true)) jumpValue = false;
    }
    void SetGlide(bool value)
    {

        glideValue = value;
        if ((disabledMovement == true)) glideValue = false;
    }
    void SetInteract(bool value)
    {
        interactValue = value;

    }
    void SetEscape(bool value) { escapeValue = value; }
    void OnJumpCancel(bool value)
    {
        LevelEventsManager.Instance.JumpCancel();
        jumpValue = value;
    }
    void OnInteractStart(InputAction.CallbackContext context)
    {
        if ((disabledMovement == true)) return;

        SetInteract(context.ReadValueAsButton());
        interactGameEvent.TriggerEvent();
        LevelEventsManager.Instance.Interact();

    }

    void OnEscape(InputAction.CallbackContext context)
    {
        SetEscape(context.ReadValueAsButton());
        PauseMenu.togglePause();
    }

    public void OnSelectInstrumentName(InputAction.CallbackContext context)
    {
        if ((disabledMovement == true)) return;

        var control = context.control;
        if (control is KeyControl keyControl)
        {
            switch (keyControl.keyCode)
            {
                case Key.Digit1:
                selectedInstrument = PlayerInstrument.InstrumentType.String;
                Debugger.Log("Key 1 pressed", Debugger.PriorityLevel.Medium);
                break;

                case Key.Digit2:
                selectedInstrument = PlayerInstrument.InstrumentType.Percussion;
                Debugger.Log("Key 2 pressed", Debugger.PriorityLevel.Medium);
                break;

                case Key.Digit3:
                selectedInstrument = PlayerInstrument.InstrumentType.Wind;
                Debugger.Log("Key 3 pressed", Debugger.PriorityLevel.Medium);
                break;

                default:
                Debugger.Log("Other key pressed " + keyControl.keyCode.ToString());
                break;
            }

            playerInstrument.ChangeInstrument(selectedInstrument);
        }
    }


    public void OnPlayNote6(InputAction.CallbackContext context)
    {
        var control = context.control;

        int id = 0;
        //When using passthrough action only triggers performed twice instead of started,performed order. This lets us only fire at started event
        if (control.IsPressed() == false) return;

        if (control is KeyControl keyControl)
        {
            switch (keyControl.keyCode)
            {
                case Key.UpArrow:
                id = 1;
                Debug.Log("Up Arrow pressed");
                break;

                case Key.DownArrow:
                id = 2;
                Debug.Log("Down Arrow pressed");
                break;

                case Key.LeftArrow:
                id = 3;
                Debug.Log("Left Arrow pressed");
                break;

                case Key.RightArrow:
                id = 4;
                Debug.Log("Right Arrow pressed");
                break;


                default:

                Debug.Log("Other key pressed " + keyControl.keyCode.ToString());
                break;
            }

            playerInstrument.ChooseNoteAndPlay(id);
        }
    }


    public void DisableMovement()
    {
        disabledMovement = true;
        moveValue = Vector2.zero;
        jumpValue = false;
    }

    public void EnableMovement()
    {
        disabledMovement = false;
    }
}

