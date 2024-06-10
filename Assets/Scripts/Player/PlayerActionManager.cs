using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerActionManager : MonoBehaviour
{

    public Vector2 moveValue { get; private set; }
    public bool jumpValue { get; private set; }
    public bool glideValue { get; private set; }
    public bool interactValue { get; private set; }
    public bool escapeValue { get; private set; }
    private void OnEnable()
    {
        if (TryGetComponent(out PlayerInput input)) { RegisterActions(input); }

    }

    private void OnDisable()
    {
        if (TryGetComponent(out PlayerInput input)) { UnRegisterActions(input); }
    }


    //TODO move boolean here and make RegisterActions  and UnRegisterActions one function
    void RegisterActions(PlayerInput input)
    {
        input.actions.FindActionMap("UI").Enable();//TODO UI or Player?

        AssignCallbacksAI(input, "Move", SetMove, null, null, context => context.ReadValue<Vector2>(), true);
        AssignCallbacksAI(input, "Jump", SetJump, OnJumpCancel, started: null, context => context.ReadValueAsButton(), true);
        AssignCallbacksAI(input, "glide", SetGlide, SetGlide, null, context => context.ReadValueAsButton(), true);
        AssignCallbacksAI(input, "interact", null, OnInteractStart, null, context => context, true);

        //TODO Failed here, Make duplicate lines or change the funtions?
        //AssignCallbacksAI(input, "PauseMenu", OnEscape, SetEscape, null, context => context.ReadValueAsButton(), true);

        InputAction escapeAction = input.actions["Pausemenu"];
        if (escapeAction != null)
        {
            escapeAction.performed += context => OnEscape(context);
            escapeAction.canceled += context => SetEscape(context.ReadValueAsButton());
        }
    }

    void UnRegisterActions(PlayerInput input)
    {


        AssignCallbacksAI(input, "Move", SetMove, null, null, context => context.ReadValue<Vector2>(), false);
        AssignCallbacksAI(input, "Jump", SetJump, OnJumpCancel, null, context => context.ReadValueAsButton(), false);
        AssignCallbacksAI(input, "glide", SetGlide, SetGlide, null, context => context.ReadValueAsButton(), false);
        AssignCallbacksAI(input, "interact", null, OnInteractStart, null, context => context, false);

        //TODO  is this wrong?
        InputAction escapeAction = input.actions["Pausemenu"];
        if (escapeAction != null)
        {
            escapeAction.performed += context => OnEscape(context);
            escapeAction.canceled += context => SetEscape(context.ReadValueAsButton());
        }
    }


    public void AssignCallbacksAI<T>(PlayerInput input, string actionName, Action<T> performed = null, Action<T> canceled = null, Action<T> started = null, Func<InputAction.CallbackContext, T> converter = null, bool enable = true)
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
        //Debug.Log("interact");
    }

    void OnEscape(InputAction.CallbackContext context)
    {
        Debug.Log("escape!");
        SetEscape(context.ReadValueAsButton());
        PauseMenu.togglePause();
    }
}

