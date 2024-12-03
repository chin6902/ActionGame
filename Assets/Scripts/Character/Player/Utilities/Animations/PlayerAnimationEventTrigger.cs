using UnityEngine;

namespace ActionGame
{
    public class PlayerAnimationEventTrigger : MonoBehaviour
    {
        private Player player;
        private WeaponHandler weaponHandler;

        private void Awake()
        {
            player = transform.parent.GetComponent<Player>();
            weaponHandler = player.GetComponentInChildren<WeaponHandler>();
        }

        public void TriggerOnMovementStateAnimationEnterEvent()
        {
            if (IsInAnimationTransition())
            {
                return;
            }

            player.OnMovementStateAnimationEnterEvent();
        }

        public void TriggerOnMovementStateAnimationExitEvent()
        {
            if (IsInAnimationTransition())
            {
                return;
            }

            player.OnMovementStateAnimationExitEvent();
        }

        public void TriggerOnMovementStateAnimationTransitionEvent()
        {
            if (IsInAnimationTransition())
            {
                return;
            }

            player.OnMovementStateAnimationTransitionEvent();
        }

        private bool IsInAnimationTransition(int layerIndex = 0)
        {
            return player.Animator.IsInTransition(layerIndex);
        }

        public void StartDealDamage()
        {
            weaponHandler.canDealDamage = true;
            weaponHandler.hasDealtDamage.Clear();
        }
        public void EndDealDamage()
        {
            weaponHandler.canDealDamage = false;
        }
    }
}