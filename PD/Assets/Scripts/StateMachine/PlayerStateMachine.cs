using UnityEngine;

public abstract class PlayerStateMachine : MonoBehaviour
{
    public bool isComplete { get; protected set; }

    protected float startTime;

    protected CharacterController characterController;

    protected PlayerMovement movement;

    public float time => Time.time - startTime;

    public virtual void Enter() { }
    public virtual void Do() { }
    public virtual void Exit() { }

    public void Setup(CharacterController _characterController, PlayerMovement _movement)
    {
        characterController = _characterController;
        movement = _movement;
    }
}