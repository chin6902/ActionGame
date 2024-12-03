using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsCameraShake : MonoBehaviour
{
    private void Shake()
    {
        CameraShake2.Instance.ShakeCamera(3f,0.2f);
    }
}
