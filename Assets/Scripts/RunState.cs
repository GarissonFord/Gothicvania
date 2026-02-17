using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class RunState : GroundedState
{
    public RunState(Player player) : base(player)
    {

    }

    public override void Update()
    {
        base.Update();

        Vector2 moveVector = moveAction.ReadValue<Vector2>();
        player.rb.linearVelocityX = moveVector.x * player.moveSpeed;

        if (inputXDirection == 0.0f)
        {
            Exit();
            player.stateMachine.TransitionTo(player.stateMachine.idleState);
        }
    }

    public override void Exit()
    {

    }
}
