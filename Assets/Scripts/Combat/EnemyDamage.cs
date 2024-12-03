using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] private Collider myCollider;
    public int damage;

    private List<Collider> alreadyCollidedWith = new List<Collider>();

    private void OnEnable()
    {
        alreadyCollidedWith.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == myCollider || alreadyCollidedWith.Contains(other))
            return;

        alreadyCollidedWith.Add(other);

        if (other.TryGetComponent<Health>(out Health health))
        {
            if(health.isPlayer)
            {
                if (health.invincible)
                {
                    health.defendSuccess = true;
                }
                else
                {
                    health.defendSuccess = false;
                    health.DealDamage(damage);
                }
            }
        }
    }


    public void SetAttack(int damage)
    {
        this.damage = damage;
    }
}
