using UnityEngine;
using UnityEngine.InputSystem;

public class IdleState : IState
{
    private Player player;
    // private Rigidbody2D rb;
    // private Animator animator;

    private InputAction moveAction;
    // private InputAction jumpAction;

    [SerializeField]
    private int xDirection;

    public IdleState(Player player)
    {
        this.player = player;
        // moveAction = InputSystem.actions.FindAction("Move");
    }

    public void Enter()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        // jumpAction = InputSystem.actions.FindAction("Jump");
        // animator = player.GetComponent<Animator>();
    }

    public void Update()
    {
        Vector2 moveVector = moveAction.ReadValue<Vector2>();
        // rb.linearVelocityX = moveVector.x * moveSpeed;

        xDirection = Mathf.CeilToInt(moveVector.x);
        // animator.SetInteger("x-direction", xDirection);

        if (xDirection != 0)
        {
            Exit();
            player.stateMachine.TransitionTo(player.stateMachine.runState);
        }

        // LayerMask mask = LayerMask.GetMask("Ground");
        // if (groundCheckCollider.IsTouchingLayers(mask))
        // {
        // animator.SetBool("grounded", true);
        // grounded = true;
        // }
        // else
        // {
        // animator.SetBool("grounded", false);
        // grounded = false;
        // }
    }

    public void Exit() { 

    }
}