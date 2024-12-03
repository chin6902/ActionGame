using Cinemachine;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraCursor : MonoBehaviour
{
    [SerializeField] private InputActionReference cameraToggleInputAction;
    [SerializeField] private bool startHidden;

    [SerializeField] private CinemachineInputProvider inputProvider;
    [SerializeField] private bool disableCameraLookOnCursorVisible;
    [SerializeField] private bool disableCameraZoomOnCursorVisible;

    [SerializeField] private bool fixedCinemachineVersion;

    private void Awake()
    {
        if (startHidden)
        {
            ToggleCursor();
        }
    }

    private void OnEnable()
    {
        // Enable the action map and subscribe to the action
        if (cameraToggleInputAction?.asset != null)
        {
            cameraToggleInputAction.asset.Enable();
            cameraToggleInputAction.action.started += OnCameraCursorToggled;
        }

        // Reset cursor state when entering a scene
        UpdateCursorState(startHidden);

        // Reinitialize input provider settings
        UpdateInputProviderState(Cursor.visible);
    }

    private void OnDisable()
    {
        // Unsubscribe from the action to avoid duplicates
        if (cameraToggleInputAction?.asset != null)
        {
            cameraToggleInputAction.action.started -= OnCameraCursorToggled;
            cameraToggleInputAction.asset.Disable();
        }
    }

    private void OnCameraCursorToggled(InputAction.CallbackContext context)
    {
        ToggleCursor();
    }

    private void ToggleCursor()
    {
        Cursor.visible = !Cursor.visible;
        UpdateCursorState(Cursor.visible);
        UpdateInputProviderState(Cursor.visible);
    }

    private void UpdateCursorState(bool isVisible)
    {
        Cursor.lockState = isVisible ? CursorLockMode.None : CursorLockMode.Locked;
    }

    private void UpdateInputProviderState(bool isCursorVisible)
    {
        if (!inputProvider) return;

        if (!fixedCinemachineVersion)
        {
            inputProvider.enabled = !isCursorVisible;
            return;
        }

        if (isCursorVisible)
        {
            if (disableCameraLookOnCursorVisible) inputProvider.XYAxis?.action.Disable();
            if (disableCameraZoomOnCursorVisible) inputProvider.ZAxis?.action.Disable();
        }
        else
        {
            inputProvider.XYAxis?.action.Enable();
            inputProvider.ZAxis?.action.Enable();
        }
    }
}
