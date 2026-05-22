using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static GameInput;

public class InputManager : MonoBehaviour, IPlayerActions
{
    // Movement event
    public UnityEvent<Vector2> OnMoveInput;

    // Sprint event
    public UnityEvent<bool> OnSprintInput;

    // Input system reference
    private GameInput _inputAction;

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

        Debug.Log(moveDirection);

        // Send movement input
        OnMoveInput?.Invoke(moveDirection);
    }

    // SPRINT
    public void OnSprint(InputAction.CallbackContext context)
    {
        // Button pressed
        if (context.performed)
        {
            Debug.Log("Sprint Start");

            // Send TRUE when sprinting
            OnSprintInput?.Invoke(true);
        }

        // Button released
        if (context.canceled)
        {
            Debug.Log("Sprint Stop");

            // Send FALSE when stop sprinting
            OnSprintInput?.Invoke(false);
        }
    }

    // INTERACT
    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Interact");
        }
    }
}