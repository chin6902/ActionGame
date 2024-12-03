using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [Header("Interaction Data")]
    public string interactableName = "";
    public float interactionDistance = 2;
    [SerializeField] bool isInteractable = true;

    public virtual void Start()
    {
    }

    public void TargetOn()
    {
    }

    public void TargetOff()
    {
    }

    public void Interact()
    {
        if (isInteractable) Interaction();
    }

    protected virtual void Interaction()
    {
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, interactionDistance);
    }
    private void OnDestroy()
    {
        TargetOff();
    }
}