using UnityEngine;
using UnityEngine.InputSystem;

public abstract class GroundedState : IState
{
    protected Player player;
    protected Animator animator;

    protected InputAction moveAction;
    protected InputAction jumpAction;
    protected InputAction attackAction;
    protected InputAction testKnockbackAction;

    protected float inputXDirection;

    protected bool canMove;
    protected bool facingRight;

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
        testKnockbackAction = InputSystem.actions.FindAction("Interact");

        canMove = true;
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
            // Debug.Log("Conditions to transition to attack");
            player.rb.linearVelocityX = 0.0f;
            player.stateMachine.TransitionTo(player.stateMachine.attackState);
            Exit();
        }

        if (canMove)
        {
            HandleMovement();
        }

        // To test the hurt animation
        if (testKnockbackAction.WasPressedThisFrame())
        {
            animator.SetTrigger("hurt");
            Knockback();
        }
    }

    public virtual void Exit()
    {
        
    }

    private void HandleMovement()
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
            facingRight = false;
        }
        else if (moveVector.x > 0.0f)
        {
            Vector3 localScale = player.transform.localScale;
            localScale.x = 1.0f;
            player.transform.localScale = localScale;
            facingRight = true;
        }
    }

    private void Knockback()
    {
        player.rb.AddForce(-player.rb.linearVelocity, ForceMode2D.Impulse);
        //Debug.Log("x velocity after initial force: " + rb.velocity.x);

        Vector2 knockbackVector;

        if (facingRight)
        {
            knockbackVector = (Vector2.left + Vector2.up) * player.knockbackForce;
        }
        else
        {
            knockbackVector = (Vector2.right + Vector2.up) * player.knockbackForce;
        }

        Debug.Log("knockbackVector: " + knockbackVector);
        player.rb.AddForce(knockbackVector, ForceMode2D.Impulse);
    }
}