using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;


public class PlayerActionManager : MonoBehaviour
{

    public Vector2 moveValue { get; private set; }
    public bool jumpValue { get; private set; }
    public bool glideValue { get; private set; }
    public bool interactValue { get; private set; }
    public bool escapeValue { get; private set; }
    public PlayerInstrument.InstrumentType selectedInsturument { get; private set; } = PlayerInstrument.InstrumentType.Wind;
    private void OnEnable()
    {
        if (TryGetComponent(out PlayerInput input))
        {
            input.actions.FindActionMap("UI").Enable();
            ManageActions(input, true);
        }

    }

    private void OnDisable()
    {
        if (TryGetComponent(out PlayerInput input)) { ManageActions(input, false); }
    }


    void ManageActions(PlayerInput input, bool onEnable)
    {

        AssignCallbacks(input, "Move", SetMove, null, null, context => context.ReadValue<Vector2>(), onEnable);
        AssignCallbacks(input, "Jump", SetJump, OnJumpCancel, started: null, context => context.ReadValueAsButton(), onEnable);
        AssignCallbacks(input, "glide", SetGlide, SetGlide, null, context => context.ReadValueAsButton(), onEnable);
        AssignCallbacks(input, "interact", null, OnInteractStart, null, context => context, onEnable);
        AssignCallbacks(input, "Select Instrument", null, null, OnSelectInsturument, context => context, onEnable);

        AssignCallbacks(input, "PauseMenu", OnEscape, null, null, context => context, onEnable);
    }

    public void AssignCallbacks<T>(PlayerInput input, string actionName, Action<T> performed = null, Action<T> canceled = null, Action<T> started = null, Func<InputAction.CallbackContext, T> converter = null, bool enable = true)
    {
        InputAction action = input.actions[actionName];
        if (action != null)
        {
            if (enable)
            {
                action.started += context => OnActionTaken(context, started, converter);
                action.performed += context => OnActionTaken(context, performed, converter);
                action.canceled += context => OnActionTaken(context, canceled, converter);
            }
            else
            {
                action.started -= context => OnActionTaken(context, started, converter);
                action.performed -= context => OnActionTaken(context, performed, converter);
                action.canceled -= context => OnActionTaken(context, canceled, converter);
            }
        }
    }
    private void OnActionTaken<T>(InputAction.CallbackContext context, Action<T> action, Func<InputAction.CallbackContext, T> buttonData)
    {
        if (action == null) return;

        action(buttonData(context));
    }


    void SetMove(Vector2 value) { moveValue = value; }
    void SetJump(bool value) { jumpValue = value; }
    void SetGlide(bool value) { glideValue = value; /*Debug.Log("glide value: " + value);*/ }
    void SetInteract(bool value) { interactValue = value; }
    void SetEscape(bool value) { escapeValue = value; }
    void OnJumpCancel(bool value)
    {
        LevelEventsManager.Instance.JumpCancel();
        jumpValue = value;
    }
    void OnInteractStart(InputAction.CallbackContext context)
    {
        SetInteract(context.ReadValueAsButton());
        LevelEventsManager.Instance.Interact();
        Debug.Log("interact");
    }

    void OnEscape(InputAction.CallbackContext context)
    {
        Debug.Log("escape!");
        SetEscape(context.ReadValueAsButton());
        PauseMenu.togglePause();
    }

    void OnSelectInsturument(InputAction.CallbackContext context)
    {

        //TODO this does not allow multiple button pressed which creates unresponsive controls.
        var control = context.control;
        if (control is KeyControl keyControl)
        {
            switch (keyControl.keyCode)
            {
                case Key.Digit1:
                selectedInsturument = PlayerInstrument.InstrumentType.Wind;
                Debug.Log("Key 1 pressed");
                break;

                case Key.Digit2:
                selectedInsturument = PlayerInstrument.InstrumentType.Percussion;
                Debug.Log("Key 2 pressed");
                break;

                case Key.Digit3:
                selectedInsturument = PlayerInstrument.InstrumentType.String;
                Debug.Log("Key 3 pressed");
                break;

                default:
                Debug.Log("Other key pressed " + keyControl.keyCode.ToString());
                break;
            }
        }



    }
}

