using UnityEngine;
using UnityEngine.InputSystem;

public class IdleState : IState
{
    private Player player;
    // private Rigidbody2D rb;
    private Animator animator;

    public BoxCollider2D groundCheckCollider;
    [SerializeField] private bool grounded;

    private InputAction moveAction;
    private InputAction jumpAction;

    [SerializeField]
    private float inputXDirection;

    public IdleState(Player player)
    {
        this.player = player;
        // moveAction = InputSystem.actions.FindAction("Move");
        animator = player.GetComponent<Animator>();
        groundCheckCollider = player.GetComponentInChildren<BoxCollider2D>();
    }

    public void Enter()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
    }

    public void Update()
    {
        Vector2 moveVector = moveAction.ReadValue<Vector2>();
        player.rb.linearVelocityX = moveVector.x * player.moveSpeed;

        inputXDirection = moveVector.x;
        animator.SetInteger("x-direction", Mathf.CeilToInt(inputXDirection));

        // Debug.Log("Am I actually in the idle state?");

        if (inputXDirection != 0.0f)
        {
            Exit();
            player.stateMachine.TransitionTo(player.stateMachine.runState);
        }
        
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
    }

    public void Exit() { 

    }
}