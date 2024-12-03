using TMPro;
using UnityEngine;

public class CrystalCountUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI crystalRedText;
    [SerializeField] private TextMeshProUGUI crystalBlueText;

    private void Update()
    {
        UpdateCrystalCounts();
    }

    private void UpdateCrystalCounts()
    {
        crystalRedText.text = GameManager.Instance.RedCrystalCount.ToString();
        crystalBlueText.text = GameManager.Instance.BlueCrystalCount.ToString();
    }
}
