using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    public bool canDealDamage;
    public List<GameObject> hasDealtDamage;
    [SerializeField] CapsuleCollider myCollider;
    [SerializeField] public float knockback = 1.5f;
    [SerializeField] float weaponLength;
    [SerializeField] private LayerMask enemyLayerMask;
    [SerializeField] float sphereRadius = 0.5f;

    private void Start()
    {
        canDealDamage = false;
        hasDealtDamage = new List<GameObject>();
    }

    private void Update()
    {
        if (canDealDamage)
        {
            RaycastHit hit;
            if (Physics.SphereCast(transform.position, sphereRadius, -transform.up, out hit, weaponLength, enemyLayerMask))
            {
                Debug.Log($"Raycast hit: {hit.transform.name} on layer {LayerMask.LayerToName(hit.transform.gameObject.layer)}");
                if (hit.transform.TryGetComponent<Health>(out Health health) && !hasDealtDamage.Contains(hit.transform.gameObject))
                {
                    health.DealDamage(PlayerStats.Instance.playerAttack);
                    hasDealtDamage.Add(hit.transform.gameObject);
                }
                if (hit.transform.TryGetComponent<ForceReceiver>(out ForceReceiver forceReceiver) && !!hasDealtDamage.Contains(hit.transform.gameObject))
                {
                    Vector3 direction = (hit.transform.position - myCollider.transform.position).normalized;
                    forceReceiver.AddForce(direction * knockback);
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        // Draw the sphere at the start position
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sphereRadius);

        // Draw the sphere at the end position
        Vector3 endPosition = transform.position + (transform.up * weaponLength);
        Gizmos.DrawWireSphere(endPosition, sphereRadius);

        // Draw lines connecting the spheres to show the sweep area
        Gizmos.DrawLine(transform.position + (transform.right * sphereRadius), endPosition + (transform.right * sphereRadius));
        Gizmos.DrawLine(transform.position + (-transform.right * sphereRadius), endPosition + (-transform.right * sphereRadius));
    }
}