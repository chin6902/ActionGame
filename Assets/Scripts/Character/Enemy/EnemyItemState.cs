using ActionGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyItemState : EnemyBaseState
{
    public EnemyItemState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.EnemyVisual.SetActive(false);
        stateMachine.DropVisual.SetActive(true);
    }
    public override void Tick(float deltaTime)
    {
        
    }

    public override void Exit()
    {
        
    }
}
