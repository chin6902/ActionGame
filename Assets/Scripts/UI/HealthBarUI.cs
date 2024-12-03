using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private Health health;
    [SerializeField] private Image barImage;

    private void Start()
    {
        if (health == null || barImage == null)
        {
            Debug.LogError("Health or Bar Image reference is missing in HealthBarUI.");
            return;
        }

        health.OnTakeDamage += Health_OnTakeDamage;
        health.OnDie += Health_OnDie;

        barImage.fillAmount = 1f;
    }

    private void OnDestroy()
    {
        if (health != null)
        {
            health.OnTakeDamage -= Health_OnTakeDamage;
            health.OnDie -= Health_OnDie;
        }
    }

    private void Health_OnTakeDamage()
    {
        //Debug.Log($"Health updated: {health.health}/{health.maxHealth}");
        if (barImage != null && health != null)
        {
            if (health.isPlayer)
            {
                barImage.fillAmount = (float)health.health / PlayerStats.Instance.playerHealthMax;
            }
            else
            {
                barImage.fillAmount = (float)health.health / health.maxHealth;
            }
        }
    }


    private void Health_OnDie()
    {
        health.OnTakeDamage -= Health_OnTakeDamage;
        health.OnDie -= Health_OnDie;

        Destroy(gameObject);
    }
}
