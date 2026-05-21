using UnityEngine;

public class PlayerCharacterMovement : MonoBehaviour
{
    // Stores movement direction
    private Vector3 _movementDirection;

    // Movement speed
    private float _currentSpeed = 5f;

    // Stores movement velocity
    private Vector3 _velocityXZ;

    // Reference to CharacterController
    [SerializeField]
    private CharacterController _characterController;

    // Receives movement input from InputManager event
    public void SetMoveDirection(Vector2 inputDirection)
    {
        // Convert Vector2 input into Vector3 movement
        _movementDirection = new Vector3(
            inputDirection.x,
            0,
            inputDirection.y
        );
    }

    // Calculates movement velocity based on camera direction
    private void CalculateVelocityXZ()
    {
        // Get camera transform
        Transform cameraTransform = Camera.main.transform;

        // Horizontal movement relative to camera
        Vector3 xDirection =
            _movementDirection.x * cameraTransform.right;

        // Forward/backward movement relative to camera
        Vector3 zDirection =
            _movementDirection.z * cameraTransform.forward;

        // Combine movement directions
        Vector3 direction = xDirection + zDirection;

        // Prevent vertical movement
        direction.y = 0;

        // Check if player is moving
        if (_movementDirection.magnitude > 0.01f)
        {
            // Calculate final movement velocity
            _velocityXZ =
                direction.normalized *
                _currentSpeed *
                Time.deltaTime;
        }
        else
        {
            // Stop movement
            _velocityXZ = Vector3.zero;
        }
    }

    // Moves the character
    public void Move()
    {
        // Calculate movement velocity
        CalculateVelocityXZ();

        // Move character using CharacterController
        _characterController.Move(_velocityXZ);
    }

    // Runs every frame
    private void Update()
    {
        Move();
        Debug.Log(_velocityXZ);
    }
}