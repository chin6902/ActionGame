using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActionGame
{
    public class PlayerDeathState : PlayerGroundedState
    {
        public PlayerDeathState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            
            stateMachine.ReusableData.MovementSpeedModifier = 0f;
            DisableCameraRecentering();
            stateMachine.Player.Input.PlayerActions.Jump.Disable();
            stateMachine.Player.Ragdoll.ToggleRagdoll(true);
            GameManager.Instance.playerAlive = false;
        }

        public override void Update()
        {
        }

        public override void HandleInput()
        {
        }
    }
}

