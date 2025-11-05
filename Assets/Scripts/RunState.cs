using UnityEngine;
using UnityEngine.InputSystem;

public class RunState : IState
{
    private Player player;

    private InputAction moveAction;

    public RunState(Player player)
    {
        this.player = player;
        
    }
    public void Enter()
    {
        moveAction = InputSystem.actions.FindAction("Move");
    }

    public void Update()
    {
        Vector2 moveVector = moveAction.ReadValue<Vector2>();
        player.rb.linearVelocityX = moveVector.x * player.moveSpeed;
    }

    public void Exit()
    {
    }
}
