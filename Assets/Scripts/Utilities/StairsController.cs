using Cinemachine;
using System.Collections;
using UnityEngine;

public class StairsController : MonoBehaviour
{
    [SerializeField] private GameObject StairsObject;
    [SerializeField] private GameObject PlayerCamera;
    [SerializeField] private GameObject StairsCamera;

    private bool activeStairs;

    private void Start()
    {
        activeStairs = false;
        StairsObject.SetActive(false);
    }

    private void Update()
    {
        if (/*GameManager.Instance.Temple1_clear && */GameManager.Instance.Temple2_clear && !activeStairs)
        {
            StartCoroutine(HandleStairsSequence());
        }
    }

    private IEnumerator HandleStairsSequence()
    {
        activeStairs = true;

        // Switch to StairsCamera
        PlayerCamera.SetActive(false);
        StairsCamera.SetActive(true);

        yield return new WaitForSeconds(1f);

        // Activate the stairs
        StairsObject.SetActive(true);

        yield return new WaitForSeconds(2f);

        // Switch back to PlayerCamera
        StairsCamera.SetActive(false);
        PlayerCamera.SetActive(true);
    }
}
