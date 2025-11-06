using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class RunState : IState
{
    private Player player;

    private Animator animator;

    private InputAction moveAction;

    [SerializeField]
    private float inputXDirection;

    public BoxCollider2D groundCheckCollider;
    // [SerializeField] private bool grounded;

    public RunState(Player player)
    {
        this.player = player;
        animator = player.GetComponent<Animator>();
        groundCheckCollider = player.GetComponentInChildren<BoxCollider2D>();
    }
    public void Enter()
    {
        moveAction = InputSystem.actions.FindAction("Move");
    }

    public void Update()
    {
        Vector2 moveVector = moveAction.ReadValue<Vector2>();
        player.rb.linearVelocityX = moveVector.x * player.moveSpeed;

        LayerMask mask = LayerMask.GetMask("Ground");

        if (groundCheckCollider.IsTouchingLayers(mask))
        {
            animator.SetBool("grounded", true);
            // grounded = true;
        }
        else
        {
            animator.SetBool("grounded", false);
            // grounded = false;
            Exit();
            player.stateMachine.TransitionTo(player.stateMachine.jumpState);
        }

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

        if (inputXDirection == 0.0f)
        {
            Exit();
            player.stateMachine.TransitionTo(player.stateMachine.idleState);
        }
    }

    public void Exit()
    {
    }
}
