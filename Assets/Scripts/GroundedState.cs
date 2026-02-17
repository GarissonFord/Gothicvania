using UnityEngine;
using UnityEngine.InputSystem;

public abstract class GroundedState : IState
{
    protected Player player;
    protected Animator animator;

    protected InputAction moveAction;
    protected InputAction jumpAction;
    protected InputAction attackAction;

    protected float inputXDirection;

    protected bool canMove;

    public GroundedState(Player player)
    {
        this.player = player;
        animator = player.animator;
    }

    public virtual void Enter()
    {
        animator.SetBool("grounded", true);
        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
        attackAction = InputSystem.actions.FindAction("Attack");
    }

    public virtual void Update()
    {
        if (!player.isGrounded)
        {
            Exit();
            player.stateMachine.TransitionTo(player.stateMachine.jumpState);
        }

        if (jumpAction.WasPressedThisFrame())
        {
            player.rb.AddForceY(player.jumpForce, ForceMode2D.Impulse);
            Exit();
            player.stateMachine.TransitionTo(player.stateMachine.jumpState);
        }

        if (attackAction.WasPressedThisFrame())
        {
            Debug.Log("Conditions to transition to attack");
            player.rb.linearVelocityX = 0.0f;
            player.stateMachine.TransitionTo(player.stateMachine.attackState);
            Exit();
        } else
        {
            canMove = true;
        }

        if (canMove)
        {
            Vector2 moveVector = moveAction.ReadValue<Vector2>();
            player.rb.linearVelocityX = moveVector.x * player.moveSpeed;
            
            inputXDirection = moveVector.x;
            animator.SetFloat("x-input", Mathf.Abs(inputXDirection));
            animator.SetInteger("x-direction", Mathf.CeilToInt(inputXDirection));

            if (moveVector.x < 0.0f)
            {
                Vector3 localScale = player.transform.localScale;
                localScale.x = -1.0f;
                player.transform.localScale = localScale;
            }
            else if (moveVector.x > 0.0f)
            {
                Vector3 localScale = player.transform.localScale;
                localScale.x = 1.0f;
                player.transform.localScale = localScale;
            }
        }
    }

    public virtual void Exit()
    {

    }
}