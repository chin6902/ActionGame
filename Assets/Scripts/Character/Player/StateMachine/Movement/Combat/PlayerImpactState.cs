using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActionGame
{
    public class PlayerImpactState : PlayerGroundedState
    {
        private Vector3 dampingVelocity;
        private float inputBufferTime = 0.1f;
        private float knockback = 1f;
        private float drag = 0.1f;

        public PlayerImpactState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();

            stateMachine.ReusableData.MovementSpeedModifier = 0f;

            stateMachine.Player.Rigidbody.velocity = Vector3.zero;

            StartAnimation(stateMachine.Player.AnimationData.ImpactParameterHash);

            stateMachine.Player.StartCoroutine(EnableInputAfterDelay());
        }

        public override void Exit()
        {
            base.Exit();

            StopAnimation(stateMachine.Player.AnimationData.ImpactParameterHash);

            ResetVelocity();
        }

        public override void OnAnimationTransitionEvent()
        {
            OnAnimationEnd();
        }

        private System.Collections.IEnumerator EnableInputAfterDelay()
        {
            yield return new WaitForSeconds(inputBufferTime);
        } 

        private void OnAnimationEnd()
        {
            if (stateMachine.ReusableData.MovementInput != Vector2.zero)
            {
                if (stateMachine.Player.Input.PlayerActions.Attack.triggered)
                {
                    stateMachine.ChangeState(stateMachine.CombatState);
                    return;
                }

                if (stateMachine.ReusableData.ShouldSprint)
                {
                    stateMachine.ChangeState(stateMachine.SprintingState);
                    return;
                }

                if (stateMachine.ReusableData.ShouldWalk)
                {
                    stateMachine.ChangeState(stateMachine.WalkingState);
                    return;
                }

                stateMachine.ChangeState(stateMachine.RunningState);
            }

            stateMachine.ChangeState(stateMachine.IdlingState);
        }
    }
}

