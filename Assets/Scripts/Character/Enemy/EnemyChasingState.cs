using ActionGame;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChasingState : EnemyBaseState
{
    private readonly int LocomotionHash = Animator.StringToHash("Locomotion");
    private readonly int SpeedHash = Animator.StringToHash("Speed");

    private const float CrossFadeDuration = 1.0f;
    private const float AnimatorDampTime = 1.0f;

    public EnemyChasingState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(LocomotionHash, CrossFadeDuration);
    }

    public override void Tick(float deltaTime)
    {
        if (!IsInChaseRange())
        {
            stateMachine.SwitchState(new EnemyIdleState(stateMachine));
            return;
        }
        else if (IsInAttackRange())
        {
            stateMachine.SwitchState(new EnemyAttackingState(stateMachine));
            return;
        }

        MoveToPlayer(deltaTime);
        FacePlayer();

        stateMachine.Animator.SetFloat(SpeedHash, 1f, AnimatorDampTime, deltaTime);
    }

    private bool IsInAttackRange()
    {
        float playerDistanceSqr = (stateMachine.Player.transform.position - stateMachine.transform.position).sqrMagnitude;

        if(stateMachine.Health.health < stateMachine.Health.maxHealth * 0.5f)
        {
            return playerDistanceSqr <= stateMachine.AttackRange2 * stateMachine.AttackRange2;
        }
        else
        {
            return playerDistanceSqr <= stateMachine.AttackRange * stateMachine.AttackRange;
        }
    }

    private void MoveToPlayer(float deltaTime)
    {
        if(stateMachine.Agent.isOnNavMesh)
        {
            stateMachine.Agent.destination = stateMachine.Player.transform.position;

            Move(stateMachine.Agent.desiredVelocity.normalized * stateMachine.MovementSpeed, deltaTime);
        }

        stateMachine.Agent.velocity = stateMachine.Controller.velocity;
    }

    public override void Exit()
    {
        if(stateMachine.Agent.isOnNavMesh )
        {
            stateMachine.Agent.ResetPath();
        }
        stateMachine.Agent.velocity = Vector3.zero;
    }

}
