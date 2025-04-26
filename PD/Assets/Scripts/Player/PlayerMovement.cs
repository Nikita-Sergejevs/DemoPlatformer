using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController characterController;

    public IdleState idleState;
    public WalkingState walkingState;
    public RunningState runningState;
    public JumpState jumpState;
    public FallingState fallingState;

    PlayerStateMachine state;

    public bool isGrounded { get; private set; }
    public bool isRunning { get; private set; }

    [Header("GroundCheck Parameters")]
    [SerializeField] private float sphereRadius;
    [SerializeField] private float checkDistance;
    [SerializeField] private LayerMask groundLayer;

    [Header("Gravity")]
    public float gravity;

    [Header("KeyCodes")]
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;
    [SerializeField] private KeyCode runningKey = KeyCode.LeftShift;

    public float xInput { get; private set; }
    public float yInput { get; private set; }

    [HideInInspector] public float verticalVelocity;
    [HideInInspector] public float currentSpeed;


    private void Start()
    {
        characterController = GetComponent<CharacterController>();

        idleState.Setup(characterController, this);
        walkingState.Setup(characterController, this);
        runningState.Setup(characterController, this);
        jumpState.Setup(characterController, this);
        fallingState.Setup(characterController, this);
        state = idleState;
    }

    private void Update()
    {
        Debug.Log(isGrounded);

        HandleInput();
        GroundCheck();
        ApplyGravity();

        if (state.isComplete)
            SelectState();
        state.Do();
    }

    private void HandleInput()
    {
        xInput = Input.GetAxis("Horizontal");
        yInput = Input.GetAxis("Vertical");

        isRunning = Input.GetKey(runningKey);
    }

    private void SelectState()
    {
        if (isGrounded)
        {
            if (Input.GetKeyDown(jumpKey))
            {
                currentSpeed = isRunning ? runningState.runningSpeed : walkingState.walkingSpeed;
                state = jumpState;
            }
            else if (xInput == 0 && yInput == 0)
                state = idleState;
            else if (Input.GetKey(runningKey))
                state = runningState;
            else
                state = walkingState;
        }
        else
            state = fallingState;
        state.Enter();
    }

    public void MoveCharacter(float speed, bool applyVelocity)
    {
        Vector3 move = transform.right * xInput + transform.forward * yInput;
        if (move.magnitude > 1)
            move.Normalize();

        if (applyVelocity)
            move.y = verticalVelocity;

        characterController.Move(move * speed * Time.deltaTime);
    }

    public void ApplyGravity()
    {
        if (isGrounded && verticalVelocity < 0)
            verticalVelocity = -1;
        else
            verticalVelocity -= gravity * Time.deltaTime;
    }

    private void GroundCheck()
    {
        Vector3 origin = transform.position;

        if (Physics.SphereCast(origin, sphereRadius, Vector3.down, out RaycastHit hit, checkDistance, groundLayer))
            isGrounded = true;
        else
            isGrounded = false;
    }
}