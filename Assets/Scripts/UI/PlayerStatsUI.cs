using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI playerHp;
    [SerializeField] private TextMeshProUGUI playerAttack;
    [SerializeField] private Button hpButton;
    [SerializeField] private Button attackButton;

    private void Start()
    {
        hpButton.onClick.AddListener(IncreasePlayerHealth);
        attackButton.onClick.AddListener(IncreasePlayerAttack);
    }

    private void Update()
    {
        UpdatePlayerStats();
    }

    private void UpdatePlayerStats()
    {
        playerHp.text = PlayerStats.Instance.playerHealth.ToString();
        playerAttack.text = PlayerStats.Instance.playerAttack.ToString();
    }

    private void IncreasePlayerHealth()
    {
        if(GameManager.Instance.RedCrystalCount > 0)
        {
            PlayerStats.Instance.playerHealth += 10;
            PlayerStats.Instance.playerHealthMax += 10;
            GameManager.Instance.RedCrystalCount--;
        }
    }

    private void IncreasePlayerAttack()
    {
        if(GameManager.Instance.BlueCrystalCount > 1)
        {
            PlayerStats.Instance.playerAttack += 5;
            GameManager.Instance.BlueCrystalCount -= 2;
        }
    }
}
