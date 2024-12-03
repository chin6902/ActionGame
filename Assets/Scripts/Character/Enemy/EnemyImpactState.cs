using ActionGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyImpactState : EnemyBaseState
{
    private readonly int ImpactHash = Animator.StringToHash("GetHit");

    private const float CrossFadeDuration = 1.0f;

    private float duration = 1.5f;

    public EnemyImpactState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(ImpactHash, CrossFadeDuration);
    }

    public override void Tick(float deltaTime)
    {
        Move(deltaTime);

        duration -= deltaTime;

        if(duration <= 0f)
        {
            //Go to scream state
            if ((stateMachine.Health.health <= stateMachine.Health.maxHealth * 0.6f && stateMachine.isDragon && !stateMachine.canScream))
            {
                stateMachine.SwitchState(new EnemyScreamState(stateMachine));
            }
            else
            {
                //Activate effect if have phase2 visual and below 50 health
                if (stateMachine.Health.health < stateMachine.Health.maxHealth * 0.5f && stateMachine.Phase2Visual != null)
                {
                    stateMachine.Phase2Visual.SetActive(true);
                }
                stateMachine.SwitchState(new EnemyIdleState(stateMachine));
            }
        }
    }

    public override void Exit()
    {

    }
}
