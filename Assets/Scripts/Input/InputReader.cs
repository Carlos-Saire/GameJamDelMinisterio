using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class InputReader : MonoBehaviour
{
    private PlayerInput playerInput;

    public static event Action<Vector2> OnInputMove;
    public static event Action<Vector2> OnArrowInput;
    public static event Action OnInputInteractue;
    public static event Action OnInputPause;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    private void OnEnable()
    {
        GameManager.OnEnableInput += OnEnableInput;
    }

    private void OnDisable()
    {
        GameManager.OnEnableInput -= OnEnableInput;
    }

    public void InputMove(InputAction.CallbackContext context)
    {
        OnInputMove?.Invoke(context.ReadValue<Vector2>());
    }

    public void InputInteractue(InputAction.CallbackContext context)
    {
        if (context.performed)
            OnInputInteractue?.Invoke();
    }

    public void InputArrowInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            OnArrowInput?.Invoke(context.ReadValue<Vector2>());
        }
    }

    public void InputPause(InputAction.CallbackContext context)
    {
        if (context.performed)
            OnInputPause?.Invoke();
    }

    private void OnEnableInput(bool value)
    {
        playerInput.enabled = value;
    }

    private void UpdateInputMap(string mapName)
    {
        playerInput.enabled = false;
        playerInput.defaultActionMap = mapName;
        playerInput.enabled = true;
    }
}
