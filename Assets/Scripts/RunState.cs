using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class RunState : GroundedState
{
/*    private Player player;

    private Animator animator;

    [SerializeField]
    private float inputXDirection;*/

    public RunState(Player player) : base(player)
    {

    }

    public override void Update()
    {
        Vector2 moveVector = moveAction.ReadValue<Vector2>();
        player.rb.linearVelocityX = moveVector.x * player.moveSpeed;

        LayerMask mask = LayerMask.GetMask("Ground");

        if (groundCheckCollider.IsTouchingLayers(mask))
        {
            animator.SetBool("grounded", true);
        }
        else
        {
            animator.SetBool("grounded", false);
            Exit();
            player.stateMachine.TransitionTo(player.stateMachine.jumpState);
        }

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
