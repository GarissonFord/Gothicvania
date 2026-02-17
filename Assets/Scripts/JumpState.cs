using UnityEngine;
using UnityEngine.InputSystem;

public class JumpState : IState
{
    private Player player;
    private InputAction moveAction;
    private Animator animator;

    private Vector2 moveVector;

    public JumpState(Player player)
    {
        this.player = player;
        animator = player.GetComponent<Animator>();
    }

    public void Enter()
    {
        animator.SetBool("grounded", false);
        moveAction = InputSystem.actions.FindAction("Move");
    }

    public void Update()
    {
        moveVector = moveAction.ReadValue<Vector2>();
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

        if (player.isGrounded)
        {
            Exit();
            
            if (moveVector.x != 0.0f)
            {
                player.stateMachine.TransitionTo(player.stateMachine.runState);
            }
            else
            {
                player.stateMachine.TransitionTo(player.stateMachine.idleState);
            }
        }
    }

    public void Exit()
    {
        
    }
}
