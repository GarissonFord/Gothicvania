using System;
using UnityEngine;

[SerializableAttribute]
public class StateMachine
{
    public IState CurrentState { get; private set; }

    public IdleState idleState;
    public RunState runState;
    public JumpState jumpState;
    public AttackState attackState;

    public StateMachine(Player player)
    {
        this.idleState = new IdleState(player);
        this.runState = new RunState(player);
        this.jumpState = new JumpState(player);
        this.attackState = new AttackState(player);
    }

    public void Initialize(IState startingState)
    {
        CurrentState = startingState;
        startingState.Enter();
    }

    public void TransitionTo(IState nextState)
    {
        CurrentState.Exit();
        CurrentState = nextState;
        nextState.Enter();
    }

    public void Update()
    {
        if (CurrentState != null)
        {
            CurrentState.Update();
        }
    }
}