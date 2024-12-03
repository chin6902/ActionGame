using System.Collections;
using UnityEngine;

public class ScreamEffectController : MonoBehaviour
{
    [SerializeField] private GameObject screamParent;    
    [SerializeField] private float targetScale = 20f;      
    [SerializeField] private float screamDuration = 0.5f; 

    private Vector3 originalScale; 
    private bool hasEnlarged = false;


    private void OnEnable()
    {
        if (screamParent != null)
        {
            originalScale = new Vector3(1,1,15);
        }
        hasEnlarged = false;
        ActivateScreamEffect();
    }

    public void ActivateScreamEffect()
    {
        if (!hasEnlarged)
        {
            hasEnlarged = true;
            StartCoroutine(GradualEnlargeAndReset());
        }
    }

    private IEnumerator GradualEnlargeAndReset()
    {
        float elapsedTime = 0f;
        Vector3 targetScaleVector = new Vector3(targetScale, targetScale, 15);

        // Gradually increase the scale to the target value
        while (elapsedTime < screamDuration)
        {
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / screamDuration;

            // Interpolate between the original scale and the target scale
            screamParent.transform.localScale = Vector3.Lerp(originalScale, targetScaleVector, progress);

            yield return null; // Wait for the next frame
        }
    }
}
