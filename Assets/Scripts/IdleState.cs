using UnityEngine;
using UnityEngine.InputSystem;

public class IdleState : GroundedState
{
    /*private Player player;
    private Animator animator;

    [SerializeField]
    private float inputXDirection;*/

    public IdleState(Player player) : base(player)
    {

    }

    public override void Update()
    {
        base.Update();

        if (inputXDirection != 0.0f)
        {
            Exit();
            player.stateMachine.TransitionTo(player.stateMachine.runState);
        }
    }
}