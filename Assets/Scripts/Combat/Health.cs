using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] public int maxHealth = 100;
    public bool isPlayer;

    public event Action OnTakeDamage;
    public event Action OnDie;

    public int health;
    public bool invincible;
    public bool defendSuccess;

    private void Start()
    {
        if (isPlayer)
        {
            health = PlayerStats.Instance.playerHealth;
        }
        else
        {
            health = maxHealth;
        }
    }

    public void DealDamage(int damage)
    {
        if (health == 0 || invincible)
        {
            return;
        }

        int newHealth = Mathf.Max(health - damage, 0);

        if (newHealth < health)
        {
            health = newHealth;
            CameraShake.Instance.ShakeCamera(5f, 0.2f);
            OnTakeDamage?.Invoke();
        }     

        if (health == 0)
        {
            OnDie?.Invoke();
        }
    }

}
