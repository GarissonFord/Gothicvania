using UnityEngine;

// Copying from the state pattern section of the game programming patterns e-book
public interface IState
{
    public virtual void Enter()
    {
        // When we first enter the state
    }

    public virtual void Update()
    {
        // Frame logic as well as condition to transition to a new state
    }

    public virtual void Exit()
    {
        // When we exit the state
    }
}
