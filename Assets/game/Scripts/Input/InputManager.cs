using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static GameInput;

public class InputManager : MonoBehaviour, IPlayerActions
{
    public UnityEvent<Vector2> OnMoveInput;

    public UnityEvent<bool> OnSprintInput;

    private GameInput _inputAction;

    public UnityEvent OnInteractInput;

    public UnityEvent OnFlashlightInput;

    private void Awake()
    {
        _inputAction = new GameInput();

        _inputAction.Enable();

        _inputAction.Player.Enable();

        _inputAction.Player.SetCallbacks(this);
    }

    // MOVE
    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 moveDirection = context.ReadValue<Vector2>();
        OnMoveInput?.Invoke(moveDirection);
    }

    // SPRINT
    public void OnSprint(InputAction.CallbackContext context)
    {
        // Button pressed
        if (context.performed)
        {
            OnSprintInput?.Invoke(true);
        }

        // Button released
        if (context.canceled)
        {
            OnSprintInput?.Invoke(false);
        }
    }

    // INTERACT
    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnInteractInput?.Invoke();
        }
    }

    //FLASHLIGHT
    public void OnFlashlight(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnFlashlightInput?.Invoke();
        }
    }
}