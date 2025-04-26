using UnityEngine;

public class RunningState : PlayerStateMachine
{
    public float runningSpeed;

    public override void Enter()
    {
        Debug.Log("Enter: " + this);
    }

    public override void Do()
    {
        movement.MoveCharacter(runningSpeed, false);

        if (!movement.isRunning)
            isComplete = true;
    }
}