using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ActionGame
{
    public class Interactor : MonoBehaviour
    {
        [SerializeField] private GameObject playerParent;
        [SerializeField] private GameObject StatsUI;
        [SerializeField] float maxInteractingDistance = 10;
        [SerializeField] float interactingRadius = 1;

        private InputAction interactAction;
        private InputAction closeAction;
        [SerializeField] LayerMask layerMask;
        private Transform cameraTransform;

        // For Gizmo
        private Vector3 origin;
        private Vector3 direction;
        private Vector3 hitPosition;
        private float hitDistance;

        [HideInInspector] public Interactable interactableTarget;

        private void Start()
        {
            cameraTransform = Camera.main.transform;

            var playerInput = playerParent.GetComponent<PlayerInput>();
            if (playerInput != null)
            {
                interactAction = playerInput.PlayerActions.Interact;
                interactAction.performed += Interact;
                closeAction = playerInput.PlayerActions.CloseUI;
                closeAction.performed += CloseUI;
            }
            else
            {
                Debug.LogError("PlayerInput component not found on the specified parent!");
            }
        }

        private void Update()
        {
            direction = cameraTransform.forward;
            origin = cameraTransform.position;
            RaycastHit hit;

            if (Physics.SphereCast(origin, interactingRadius, direction, out hit, maxInteractingDistance, layerMask))
            {
                hitPosition = hit.point;
                hitDistance = hit.distance;
                if (hit.transform.TryGetComponent<Interactable>(out interactableTarget))
                {
                    interactableTarget.TargetOn();
                }
            }
            else if (interactableTarget)
            {
                interactableTarget.TargetOff();
                interactableTarget = null;
            }
        }

        private void Interact(InputAction.CallbackContext context)
        {
            if (interactableTarget != null)
            {
                if (Vector3.Distance(transform.position, interactableTarget.transform.position) <= interactableTarget.interactionDistance)
                {
                    interactableTarget.Interact();
                }
            }
            else
            {
                Debug.Log("Nothing to interact with!");
            }
        }
        private void CloseUI(InputAction.CallbackContext obj)
        {
            if(StatsUI != null)
            {
                StatsUI.SetActive(false);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(origin, origin + direction * hitDistance);
            Gizmos.DrawWireSphere(hitPosition, interactingRadius);
        }

        private void OnDestroy()
        {
            // Unregister the interact action
            if (interactAction != null)
            {
                interactAction.performed -= Interact;
            }
        }
    }
}
