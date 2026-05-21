using UnityEngine;
using UnityEngine.InputSystem;
using static GameInput;

public class InputManager : MonoBehaviour, IPlayerActions
{
    // Variable to store input action reference
    private GameInput _inputAction;

    private void Awake()
    {
        // Create input action object
        _inputAction = new GameInput();

        // Enable input system
        _inputAction.Enable();

        // Enable Player action map
        _inputAction.Player.Enable();

        // Tell Unity this script handles Player input callbacks
        _inputAction.Player.SetCallbacks(this);
    }

    // Called when Interact input happens
    public void OnInteract(InputAction.CallbackContext context)
    {
        // Only trigger when button is actually pressed
        if (context.performed)
        {
            Debug.Log("Interact");
        }
    }

    // Called when movement input happens
    public void OnMove(InputAction.CallbackContext context)
    {
        // Read movement vector (WASD / joystick)
        Vector2 moveDirection = context.ReadValue<Vector2>();

        Debug.Log(moveDirection);
    }
}