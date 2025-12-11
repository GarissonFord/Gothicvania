using UnityEditor.Tilemaps;
using UnityEngine;

public class AttackState : GroundedState
{
    public AttackState(Player player) : base(player) { }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();

        player.rb.linearVelocityX = 0.0f;
    }
}
