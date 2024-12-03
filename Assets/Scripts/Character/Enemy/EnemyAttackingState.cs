using UnityEngine;

namespace ActionGame
{
    public class EnemyAttackingState : EnemyBaseState
    {
        private readonly int AttackHash = Animator.StringToHash("Attack01");
        private readonly int AttackHash2 = Animator.StringToHash("Attack02");
        private readonly int FlameAttackHash = Animator.StringToHash("FlameAttack");

        private const float TransitionDuration = 0.1f;
        private int screamAttackCount;

        public EnemyAttackingState(EnemyStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Enter()
        {
            FacePlayer();

            screamAttackCount = Random.Range(2, 4);

            if (stateMachine.Health.health > stateMachine.Health.maxHealth * 0.5f)
            {
                stateMachine.EnemyDamage.SetAttack(stateMachine.AttackDamage);
                stateMachine.Animator.CrossFadeInFixedTime(AttackHash, TransitionDuration);
            }
            else
            {
                if (IsInAttackRange1() && DragonPhase2())
                {
                    stateMachine.EnemyDamage.SetAttack(stateMachine.AttackDamage2);
                    stateMachine.Animator.CrossFadeInFixedTime(FlameAttackHash, TransitionDuration);
                }
                else
                {
                    stateMachine.EnemyDamage.SetAttack(stateMachine.AttackDamage2);
                    stateMachine.Animator.CrossFadeInFixedTime(AttackHash2, TransitionDuration);
                }
            }
        }

        public override void Tick(float deltaTime)
        {
            if (GetNormalizedTime() >= 1)
            {
                stateMachine.AttackCount++;

                if (stateMachine.AttackCount >= screamAttackCount)
                {
                    // Only switch to EnemyScreamState if isDragon and conditions met
                    if (DragonPhase2())
                    {
                        stateMachine.SwitchState(new EnemyScreamState(stateMachine));
                    }
                    else
                    {
                        stateMachine.SwitchState(new EnemyWaitingState(stateMachine));
                    }
                }
                else
                {
                    stateMachine.SwitchState(new EnemyWaitingState(stateMachine));
                }
            }
        }

        public override void Exit()
        {
        }

        private bool IsInAttackRange1()
        {
            return Vector3.Distance(stateMachine.Player.transform.position, stateMachine.transform.position) <= stateMachine.AttackRange;
        }

        private bool DragonPhase2()
        {
            return stateMachine.isDragon && stateMachine.Health.health <= stateMachine.Health.maxHealth * 0.6f && stateMachine.canScream;
        }
    }
}
