using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldDragon : MonoBehaviour
{
    public GameObject startWaypoint;
    public GameObject endWaypoint;
    public GameObject portal;
    public GameObject dragon;
    public GameObject dragonCamera;
    public GameObject playerCamera;
    public Animator animator;
    public float moveSpeed = 2f;

    private bool flameTriggered;
    private bool finishTriggered;
    private bool canSpawn;

    private void Start()
    {
        canSpawn = false;
        flameTriggered = false;
        finishTriggered = false;
        dragon.SetActive(false);
        transform.position = startWaypoint.transform.position;
    }

    private void Update()
    {
        if ((GameManager.Instance.Temple1_clear/* || GameManager.Instance.Temple2_clear*/))
        {
            if(!canSpawn)
            {
                CameraShake.Instance.ShakeCamera(10f, 3f);
                playerCamera.SetActive(false);
                dragonCamera.SetActive(true);
            }
            dragon.SetActive(true);
            canSpawn = true;
            transform.position = Vector3.MoveTowards(transform.position, endWaypoint.transform.position, moveSpeed * Time.deltaTime);

            float distanceToEnd = Vector3.Distance(startWaypoint.transform.position, endWaypoint.transform.position);
            float currentDistance = Vector3.Distance(transform.position, endWaypoint.transform.position);

            if (currentDistance <= 0.8f * distanceToEnd)
            {
                playerCamera.SetActive(true);
                dragonCamera.SetActive(false);
            }

                if (currentDistance <= 0.7f * distanceToEnd)
            {
                if (!flameTriggered)
                {
                    animator.SetTrigger("Flame");
                    flameTriggered = true;
                }

                // Check if the flame animation has completed
                AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
                if (flameTriggered && stateInfo.IsTag("Flame") && stateInfo.normalizedTime >= 1f && !finishTriggered)
                {
                    animator.SetTrigger("Finish");    // Trigger the transition back to Idle Flying
                    finishTriggered = true;
                }
            }

            if (currentDistance <= 0.2f * distanceToEnd)
            {
                portal.SetActive(true);
            }

            if (currentDistance == 0)
            {
                portal.SetActive(false);
                Destroy(gameObject);
            }
        }
    }
}
