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
        Debug.Log("AttackState entered");
        player.animator.SetTrigger("attack");
        // player.rb.linearVelocityX = 0.0f;
        attackState = Animator.StringToHash("Base Layer.PlayerAttack");
    }

    public override void Update()
    {
        // base.Update();

        // player.rb.linearVelocityX = 0.0f;

        AnimatorStateInfo animatorState = animator.GetCurrentAnimatorStateInfo(0);
        int currentState = animatorState.fullPathHash;

        if (!currentState.Equals(attackState))
        {
            player.stateMachine.TransitionTo(player.stateMachine.idleState);
        }
    }

    public void OnAttackAnimationEnd(string time)
    {
        Debug.Log("attack animation ended at " + time);
        
    }
}
