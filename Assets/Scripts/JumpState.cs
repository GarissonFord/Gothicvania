using UnityEngine;
using UnityEngine.InputSystem;

public class JumpState : IState
{
    private Player player;

    private InputAction moveAction;

    private Animator animator;

    public BoxCollider2D groundCheckCollider;
    // [SerializeField] private bool grounded;

    public JumpState(Player player)
    {
        this.player = player;
        animator = player.GetComponent<Animator>();
        groundCheckCollider = player.GetComponentInChildren<BoxCollider2D>();
    }

    public void Enter()
    {
        player.GetComponent<Animator>().SetTrigger("jump");
        moveAction = InputSystem.actions.FindAction("Move");
    }

    public void Update()
    {
        Vector2 moveVector = moveAction.ReadValue<Vector2>();
        player.rb.linearVelocityX = moveVector.x * player.moveSpeed;

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

        LayerMask mask = LayerMask.GetMask("Ground");

        if (groundCheckCollider.IsTouchingLayers(mask))
        {
            animator.SetBool("grounded", true);
            // grounded = true;
            Exit();
            player.stateMachine.TransitionTo(player.stateMachine.idleState);
        }
        else
        {
            animator.SetBool("grounded", false);
            // grounded = false;
            
        }
    }

    public void Exit()
    {
    }
}
