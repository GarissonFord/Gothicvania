using UnityEngine;

// Copying from the state pattern section of the game programming patterns e-book
public interface IState
{
    public void Enter()
    {
        // When we first enter the state
    }

    public void Update()
    {
        // Frame logic as well as condition to transition to a new state
    }

    public void Exit()
    {
        // When we exit the state
    }
}
