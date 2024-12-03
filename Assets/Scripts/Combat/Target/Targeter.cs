using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targeter : MonoBehaviour
{
    public List<Target> targets = new List<Target>();

    private void Update()
    {
        GetClosestEnemyDirection();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent<Target>(out Target target))
        {
            return;
        }

        targets.Add(target);
        target.OnDestroyed += RemoveTarget;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.TryGetComponent<Target>(out Target target))
        {
            return;
        }

        target.OnDestroyed -= RemoveTarget;
        targets.Remove(target);
    }

    private void RemoveTarget(Target target)
    {
        target.OnDestroyed -= RemoveTarget;

        targets.Remove(target);
    }


    public Vector3 GetClosestEnemyDirection()
    {
        Target closestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        foreach (Target target in targets)
        {
            Vector3 directionToTarget = target.transform.position - currentPosition;
            float distanceSqrToTarget = directionToTarget.sqrMagnitude;

            if (distanceSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = distanceSqrToTarget;
                closestTarget = target;
            }
        }

        if (closestTarget != null)
        {
            Vector3 directionToClosestTarget = closestTarget.transform.position - currentPosition;
            directionToClosestTarget.y = 0f;
            return directionToClosestTarget.normalized;
        }

        return Vector3.zero;
    }

    public Vector3 GetClosestEnemyPosition()
    {
        Target closestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        foreach (Target target in targets)
        {
            Vector3 directionToTarget = target.transform.position - currentPosition;
            float distanceSqrToTarget = directionToTarget.sqrMagnitude;

            if (distanceSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = distanceSqrToTarget;
                closestTarget = target;
            }
        }

        if (closestTarget != null)
        {
            return closestTarget.transform.position;
        }

        return Vector3.zero;
    }

    private Target GetClosestEnemy()
    {
        Target closestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        foreach (Target target in targets)
        {
            Vector3 directionToTarget = target.transform.position - currentPosition;
            float distanceSqrToTarget = directionToTarget.sqrMagnitude;

            if (distanceSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = distanceSqrToTarget;
                closestTarget = target;
            }
        }

        return closestTarget;
    }
}
