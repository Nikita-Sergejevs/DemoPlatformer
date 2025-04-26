using UnityEngine;

public class WalkingState : PlayerStateMachine
{
    public float walkingSpeed;

    public override void Enter()
    {
        Debug.Log("Enter: " + this);

    }

    public override void Do()
    {
        movement.MoveCharacter(walkingSpeed, false);

        if (movement.xInput == 0 || movement.yInput == 0)
            isComplete = true;
    }
}