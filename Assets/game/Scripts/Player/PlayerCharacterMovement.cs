using UnityEngine;

public class PlayerCharacterMovement : MonoBehaviour
{
    private Vector3 _movementDirection;

    private float _currentSpeed = 0f;

    [SerializeField]
    private float _walkSpeed = 1f;

    [SerializeField]
    private float _sprintSpeed = 2f;

    [SerializeField]
    private float _acceleration = 1f;

    private bool _isSprint;

    private Vector3 _velocityXZ;

    [SerializeField]
    private float _gravityScale = 1f;

    private float _velocityY;

    private bool _isGrounded;

    [SerializeField]
    private CharacterController _characterController;

    public void SetMoveDirection(Vector2 inputDirection)
    {
        _movementDirection = new Vector3(
            inputDirection.x,
            0,
            inputDirection.y
        );
    }

    public void SetSprint(bool isSprint)
    {
        _isSprint = isSprint;
        Debug.Log("Sprint: " + isSprint);
    }

private void CalculateAcceleration()
{
    if (_movementDirection.magnitude <= 0.01f)
    {
        _currentSpeed = 0;
        return;
    }

    float targetSpeed;

    if (_isSprint)
    {
        targetSpeed = _sprintSpeed;
    }
    else
    {
        targetSpeed = _walkSpeed;
    }

    //acceleration
    _currentSpeed = Mathf.MoveTowards(
        _currentSpeed,
        targetSpeed,
        _acceleration * Time.deltaTime
    );
}

    private void CalculateVelocityXZ()
    {
        Transform cameraTransform = Camera.main.transform;

        Vector3 xDirection =
            _movementDirection.x * cameraTransform.right;

        Vector3 zDirection =
            _movementDirection.z * cameraTransform.forward;

        Vector3 direction = xDirection + zDirection;

        direction.y = 0;

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

    //gravity
    private void CalculateVelocityY()
    {
        _velocityY =
            _velocityY +
            Physics.gravity.y *
            _gravityScale *
            Time.deltaTime;
    }

    //grounded
    private void CheckIsGrounded()
    {
        LayerMask groundLayer = LayerMask.GetMask("Ground");

        _isGrounded = Physics.CheckSphere(
            transform.position,
            0.5f,
            groundLayer
        );
    }

    private void ResetVelocityY()
    {
        if (_isGrounded == true && _velocityY < 0)
        {
            _velocityY = -2f;
        }
    }

    //move
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
    //sprint
    public bool IsSprint => _isSprint;

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