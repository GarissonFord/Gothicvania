using UnityEngine;

public class JumpState
{
    private Player player;

    public JumpState(Player player)
    {
        this.player = player;
    }

    public void Enter()
    {
        player.GetComponent<Animator>().SetTrigger("jump");
    }

    public void Update()
    {

    }

    public void Exit()
    {
    }
}
