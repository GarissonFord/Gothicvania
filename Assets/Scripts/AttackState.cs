using UnityEditor.Animations;
using UnityEditor.Tilemaps;
using UnityEngine;

public class AttackState : GroundedState
{
    protected int currentState;
    protected int attackState;

    public AttackState(Player player) : base(player) { }

    public override void Enter()
    {
        base.Enter();
        // Debug.Log("AttackState entered");
        player.animator.SetTrigger("attack");
        player.rb.linearVelocityX = 0.0f;
        canMove = false;
        attackState = Animator.StringToHash("Base Layer.PlayerAttack");
    }

    public override void Update()
    {
        base.Update();

        player.rb.linearVelocityX = 0.0f;

        AnimatorStateInfo animatorState = animator.GetCurrentAnimatorStateInfo(0);
        int currentState = animatorState.fullPathHash;

        if (!currentState.Equals(attackState))
        {
            // Debug.Log("Current state is not attack state, transitioning back");
            if (inputXDirection == 0.0f)
            {
                player.stateMachine.TransitionTo(player.stateMachine.idleState);
            } else
            {
                player.stateMachine.TransitionTo(player.stateMachine.runState);
            }

            Exit();
        }
    }

    public override void Exit()
    {
        canMove = true;
    }
}
