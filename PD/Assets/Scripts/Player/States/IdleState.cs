using UnityEngine;

public class IdleState : PlayerStateMachine
{
    public override void Enter()
    {
        Debug.Log("Enter: " + this);
    }

    public override void Do()
    {
        if (movement.isGrounded || movement.xInput != 0 || movement.yInput != 0)
            isComplete = true;
    }
}