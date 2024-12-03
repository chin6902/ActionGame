using ActionGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathState : EnemyBaseState
{
    private readonly int DeathHash = Animator.StringToHash("Die");

    private const float CrossFadeDuration = 1.0f;

    public EnemyDeathState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        if(stateMachine.Phase2Visual != null)
        {
            stateMachine.Phase2Visual.SetActive(false);
        }

        if(stateMachine.isDragon)
        {
            GameManager.Instance.dragonDead = true;
        }

        stateMachine.Animator.CrossFadeInFixedTime(DeathHash, CrossFadeDuration);
        stateMachine.EnemyDamage.gameObject.SetActive(false);
        GameObject.Destroy(stateMachine.Target);
    }

    public override void Tick(float deltaTime)
    {
        AnimatorStateInfo stateInfo = stateMachine.Animator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.shortNameHash == DeathHash && stateInfo.normalizedTime >= 2f && !stateMachine.isDragon)
        {
            stateMachine.SwitchState(new EnemyItemState(stateMachine));
        }
    }

    public override void Exit()
    {

    }
}
