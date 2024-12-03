using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActionGame
{
    public abstract class EnemyBaseState : State
    {
        protected EnemyStateMachine stateMachine;

        public EnemyBaseState(EnemyStateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
        }

        protected bool IsInChaseRange()
        {
            float playerDistanceSqr = (stateMachine.Player.transform.position - stateMachine.transform.position).sqrMagnitude;

            return playerDistanceSqr <= stateMachine.PlayerChasingRange * stateMachine.PlayerChasingRange ;
        }

        protected void Move(float deltaTime)
        {
            Move(Vector3.zero, deltaTime);
        }

        protected void FacePlayer()
        {
            // Get the direction towards the player
            Vector3 directionToPlayer = (stateMachine.Player.transform.position - stateMachine.transform.position).normalized;

            // Ignore vertical difference (Y-axis) by flattening the direction
            directionToPlayer.y = 0f;

            // Calculate the desired rotation to face the player
            Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);

            float RotationSpeed = 2.5f;
            // Apply the rotation smoothly
            stateMachine.transform.rotation = Quaternion.Slerp(
                stateMachine.transform.rotation,
                targetRotation,
                Time.deltaTime * RotationSpeed // Adjust the rotation speed if needed
            );
        }

        protected void Move(Vector3 motion, float deltaTime)
        {
            stateMachine.Controller.Move((motion + stateMachine.ForceReceiver.Movement) * deltaTime);
        }

        protected float GetNormalizedTime()
        {
            AnimatorStateInfo currentInfo = stateMachine.Animator.GetCurrentAnimatorStateInfo(0);
            AnimatorStateInfo nextInfo = stateMachine.Animator.GetNextAnimatorStateInfo(0);

            if(stateMachine.Animator.IsInTransition(0) && nextInfo.IsTag("Attack"))
            {
                return nextInfo.normalizedTime;
            }
            else if(!stateMachine.Animator.IsInTransition(0) && currentInfo.IsTag("Attack"))
            {
                return currentInfo.normalizedTime;
            }
            else
            {
                return 0f;
            }
        }
    }
}

