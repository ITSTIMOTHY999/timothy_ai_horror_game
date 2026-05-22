using UnityEngine;

public class PlayerCharacterMovement : MonoBehaviour
{
    // Stores movement direction
    private Vector3 _movementDirection;

    // Current movement speed
    private float _currentSpeed = 0f;

    // Walk speed
    [SerializeField]
    private float _walkSpeed = 1f;

    // Sprint speed
    [SerializeField]
    private float _sprintSpeed = 2f;

    // Acceleration amount
    [SerializeField]
    private float _acceleration = 1f;

    // Sprint status
    private bool _isSprint;

    // Stores movement velocity on XZ
    private Vector3 _velocityXZ;

    // Gravity strength
    [SerializeField]
    private float _gravityScale = 1f;

    // Vertical velocity
    private float _velocityY;

    // Grounded state
    private bool _isGrounded;

    // Reference to CharacterController
    [SerializeField]
    private CharacterController _characterController;

    // Receives movement input from InputManager
    public void SetMoveDirection(Vector2 inputDirection)
    {
        _movementDirection = new Vector3(
            inputDirection.x,
            0,
            inputDirection.y
        );
    }

    // Receives sprint input from InputManager
    public void SetSprint(bool isSprint)
    {
        _isSprint = isSprint;
        Debug.Log("Sprint: " + isSprint);
    }

    // Calculate acceleration/deceleration
private void CalculateAcceleration()
{
    // If not moving
    if (_movementDirection.magnitude <= 0.01f)
    {
        _currentSpeed = 0;
        return;
    }

    // Determine target speed
    float targetSpeed;

    if (_isSprint)
    {
        targetSpeed = _sprintSpeed;
    }
    else
    {
        targetSpeed = _walkSpeed;
    }

    // Smoothly move toward target speed
    _currentSpeed = Mathf.MoveTowards(
        _currentSpeed,
        targetSpeed,
        _acceleration * Time.deltaTime
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
                _currentSpeed;
        }
        else
        {
            _velocityXZ = Vector3.zero;
        }
    }

    // Calculate gravity velocity
    private void CalculateVelocityY()
    {
        _velocityY =
            _velocityY +
            Physics.gravity.y *
            _gravityScale *
            Time.deltaTime;
    }

    // Check if player is grounded
    private void CheckIsGrounded()
    {
        LayerMask groundLayer = LayerMask.GetMask("Ground");

        _isGrounded = Physics.CheckSphere(
            transform.position,
            0.5f,
            groundLayer
        );
    }

    // Reset gravity when grounded
    private void ResetVelocityY()
    {
        if (_isGrounded == true && _velocityY < 0)
        {
            _velocityY = -2f;
        }
    }

    // Move player
    public void Move()
    {
        // Calculate horizontal movement
        CalculateVelocityXZ();

        // Calculate gravity
        CalculateVelocityY();

        // Combine velocity
        Vector3 velocity = new Vector3(
            _velocityXZ.x,
            _velocityY,
            _velocityXZ.z
        );

        // Move character
        _characterController.Move(velocity * Time.deltaTime);
    }

    private void Update()
    {
        CheckIsGrounded();

        CalculateAcceleration();

        ResetVelocityY();

        Move();

        Debug.Log(
            "_isSprint = " + _isSprint +
            " | currentSpeed = " + _currentSpeed
        );
    }
}