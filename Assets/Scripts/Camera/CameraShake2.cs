using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake2 : MonoBehaviour
{
    public static CameraShake2 Instance { get; private set; }

    private CinemachineVirtualCamera cinemachineVirtualCamera;
    private float shakeTimer;
    private float shakeTimerMax;
    private float startingIntensity;

    private void Awake()
    {
        Instance = this;
        cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
        if (cinemachineVirtualCamera == null)
        {
            Debug.LogError("CinemachineVirtualCamera component is missing!");
        }
    }

    private void OnEnable()
    {
        // Reset shake values when the camera is turned on
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        if (cinemachineBasicMultiChannelPerlin != null)
        {
            cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0f;
        }
        shakeTimer = 0f;
    }


    public void ShakeCamera(float intensity, float time)
    {
        var cinemachineBasicMultiChannelPerlin = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        if (cinemachineBasicMultiChannelPerlin == null)
        {
            return;
        }

        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;

        startingIntensity = intensity;
        shakeTimerMax = time;
        shakeTimer = time;
    }

    private void Update()
    {
        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;

            var cinemachineBasicMultiChannelPerlin = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            if (cinemachineBasicMultiChannelPerlin == null)
            {
                return;
            }

            // Gradually reduce amplitude over time
            float currentIntensity = Mathf.Lerp(0f, startingIntensity, shakeTimer / shakeTimerMax);
            cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = currentIntensity;

            // Reset to zero when the timer reaches zero
            if (shakeTimer <= 0f)
            {
                cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0f;
            }
        }
    }
}
