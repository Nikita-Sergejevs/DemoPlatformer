using UnityEngine;

public class FallingState : PlayerStateMachine
{
    [Header("Falling Parameters")]
    [SerializeField] private float fallingSpeedMultiplier;

    public override void Enter()
    {
        Debug.Log("Enter: " + this);
        isComplete = false;
    }

    public override void Do()
    {
        if (movement.isGrounded)
            isComplete = true;

        Fall();
    }

    private void Fall()
    {
        movement.verticalVelocity -= movement.gravity * fallingSpeedMultiplier * Time.deltaTime;
        movement.MoveCharacter(movement.currentSpeed, true);
    }
}