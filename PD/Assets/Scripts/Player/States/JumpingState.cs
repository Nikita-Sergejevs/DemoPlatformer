using UnityEngine;

public class JumpState : PlayerStateMachine
{
    [Header("Jump Parameters")]
    [SerializeField] private int maxJumpCount;
    [SerializeField] private float jumpHeight;

    public int jumpsLeft;
    private bool jumpPressedAgain;

    public override void Enter()
    {
        movement.verticalVelocity = jumpHeight;
        jumpsLeft = Mathf.Max(jumpsLeft, maxJumpCount - 1);
        isComplete = false;
        jumpPressedAgain = false;

        Debug.Log("Enter: " + this);

    }

    public override void Do()
    {
        HandleJump();

        if (movement.verticalVelocity <= 0) 
            isComplete = true;
    }

    private void HandleJump()
    {
        movement.MoveCharacter(movement.currentSpeed, true);
    }
}