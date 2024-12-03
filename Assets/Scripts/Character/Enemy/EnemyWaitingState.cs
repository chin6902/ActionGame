using UnityEngine;

namespace ActionGame
{
    public class EnemyWaitingState : EnemyBaseState
    {
        private readonly int LocomotionHash = Animator.StringToHash("Locomotion");
        private readonly int WalkBackHash = Animator.StringToHash("WalkBack");
        private readonly int SpeedHash = Animator.StringToHash("Speed");
        private const float CrossFadeDuration = 0.1f;

        private readonly float waitDuration = 1f;
        private float waitTimer;
        private Vector3 initialPosition;
        private bool stepBackCompleted;

        private const float StepBackDistance = 10f; // Desired step back distance

        public EnemyWaitingState(EnemyStateMachine stateMachine) : base(stateMachine)
        {
            waitTimer = waitDuration;
        }

        public override void Enter()
        {
            stateMachine.Animator.CrossFadeInFixedTime(LocomotionHash, CrossFadeDuration);
            stateMachine.Animator.SetFloat(SpeedHash, 0f);
        }

        public override void Tick(float deltaTime)
        {
            // Count down the timer
             waitTimer -= deltaTime;

             if (IsInChaseRange())
             {
                 FacePlayer();
             }

             if (waitTimer <= 0f)
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
                    stateMachine.SwitchState(new EnemyChasingState(stateMachine));
                }
             }
        }

        public override void Exit()
        {

        }
    }
}
