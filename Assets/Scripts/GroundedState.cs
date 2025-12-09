using UnityEngine;
using UnityEngine.InputSystem;

public abstract class GroundedState : IState
{
    protected Player player;
    protected Animator animator;

    protected BoxCollider2D groundCheckCollider;
    [SerializeField] private bool grounded;

    protected InputAction moveAction;
    protected InputAction jumpAction;

    protected float inputXDirection;

    public GroundedState(Player player)
    {
        this.player = player;
        animator = player.animator;
    }

    public virtual void Enter()
    {
        groundCheckCollider = player.groundCheckCollider;
        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
    }

    public virtual void Update()
    {
        LayerMask mask = LayerMask.GetMask("Ground");

        if (groundCheckCollider.IsTouchingLayers(mask))
        {
            animator.SetBool("grounded", true);
            grounded = true;
        }
        else
        {
            animator.SetBool("grounded", false);
            grounded = false;
            Exit();
            player.stateMachine.TransitionTo(player.stateMachine.jumpState);
        }

        if (grounded && jumpAction.WasPressedThisFrame())
        {
            animator.SetTrigger("jump");
            player.rb.AddForceY(player.jumpForce, ForceMode2D.Impulse);
        }

        Vector2 moveVector = moveAction.ReadValue<Vector2>();
        player.rb.linearVelocityX = moveVector.x * player.moveSpeed;

        inputXDirection = moveVector.x;
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

    public virtual void Exit()
    {

    }
}