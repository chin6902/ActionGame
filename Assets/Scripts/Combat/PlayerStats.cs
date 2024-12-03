using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance;

    public int playerHealthMax;
    public int playerHealth = 100;
    public int playerAttack = 10;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keeps this object across scene loads
        }
        else
        {
            Destroy(gameObject); // Ensures there's only one instance
        }
    }

    private void Start()
    {
        playerHealthMax = playerHealth;
    }

    public void SaveStats()
    {
        PlayerPrefs.SetInt("PlayerHealth", playerHealth);
        PlayerPrefs.SetInt("PlayerAttack", playerAttack);
        PlayerPrefs.Save();
    }

    public void LoadStats()
    {
        if (PlayerPrefs.HasKey("PlayerHealth"))
            playerHealth = PlayerPrefs.GetInt("PlayerHealth");

        if (PlayerPrefs.HasKey("PlayerAttack"))
            playerAttack = PlayerPrefs.GetInt("PlayerAttack");
    }
}
