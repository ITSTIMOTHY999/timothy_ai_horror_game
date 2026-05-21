using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static GameInput;

public class InputManager : MonoBehaviour, IPlayerActions
{
    // Event to send movement input
    public UnityEvent<Vector2> OnMoveInput;

    // Input system reference
    private GameInput _inputAction;

    private void Awake()
    {
        _inputAction = new GameInput();

        _inputAction.Enable();

        _inputAction.Player.Enable();

        _inputAction.Player.SetCallbacks(this);
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Interact");
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 moveDirection =
            context.ReadValue<Vector2>();

        Debug.Log(moveDirection);

        // SEND movement to listeners
        OnMoveInput?.Invoke(moveDirection);
    }
}