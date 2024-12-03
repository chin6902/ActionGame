using ActionGame;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScreamState : EnemyBaseState
{
    private bool spawnMeteor;

    private readonly int ScreamHash = Animator.StringToHash("Scream");

    private const float CrossFadeDuration = 1.0f;

    public EnemyScreamState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.TriggerStartScream();
        // Reset the attack count when entering the scream state
        stateMachine.AttackCount = 0;

        // Only apply changes if the enemy is in Dragon form
        if (stateMachine.isDragon)
        {
            stateMachine.canScream = true;
            stateMachine.Health.invincible = true;
            stateMachine.Animator.CrossFadeInFixedTime(ScreamHash, CrossFadeDuration);
        }
    }

    public override void Tick(float deltaTime)
    {
        AnimatorStateInfo stateInfo = stateMachine.Animator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.shortNameHash == ScreamHash && stateInfo.normalizedTime >= 0.3f)
        {
            CameraShake.Instance.ShakeCamera(5f, 0.2f);
            FacePlayer();
        }
        if (stateInfo.shortNameHash == ScreamHash && stateInfo.normalizedTime >= 1f)
        {
            stateMachine.SwitchState(new EnemyChasingState(stateMachine));
        }
    }

    public override void Exit()
    {
        stateMachine.TriggerStopScream();
        stateMachine.Health.invincible = false;
    }
}
