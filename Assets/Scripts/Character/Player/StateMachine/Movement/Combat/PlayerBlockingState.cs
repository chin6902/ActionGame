using UnityEngine;
using UnityEngine.InputSystem;

namespace ActionGame
{
    public class PlayerBlockingState : PlayerGroundedState
    {
        public PlayerBlockingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();

            StartAnimation(stateMachine.Player.AnimationData.BlockingParameterhash);

            stateMachine.ReusableData.MovementSpeedModifier = 0f;

            ResetVelocity();

            stateMachine.Player.Health.invincible = true;
        }

        public override void HandleInput()
        {
            base.HandleInput();

            if (!stateMachine.Player.Input.PlayerActions.Block.IsPressed())
            {
                stateMachine.ChangeState(stateMachine.IdlingState);
                return;
            }

            if (stateMachine.ReusableData.MovementInput != Vector2.zero)
            {
                OnMove();
                return;
            }

            if (stateMachine.Player.Input.PlayerActions.Attack.triggered)
            {
                stateMachine.ChangeState(stateMachine.CombatState);
            }
        }

        public override void Exit()
        {
            base.Exit();

            StopAnimation(stateMachine.Player.AnimationData.BlockingParameterhash);

            stateMachine.Player.Health.invincible = false;

            if(stateMachine.Player.Health.defendSuccess)
            {
                stateMachine.Player.Animator.SetBool("canCounter", true);
            }
        }

        protected override void OnMove()
        {
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
    }
}
