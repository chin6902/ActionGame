using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlashEffect : MonoBehaviour
{
    public GameObject slashOnePrefab;
    public GameObject slashTwoPrefab;
    public GameObject slashThreePrefab;
    public GameObject slashFourPrefab;
    public GameObject slashFivePrefab;
    public Transform slashOneLocation;
    public Transform slashTwoLocation;
    public Transform slashThreeLocation;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void ShowSlashEffect()
    {
        slashOnePrefab.SetActive(true);
    }

    public void HideSlashEffect()
    {
        slashOnePrefab.SetActive(false);
    }

    public void ShowSlashTwoEffect()
    {
        slashTwoPrefab.SetActive(true);
    }

    public void HideSlashTwoEffect()
    {
        slashTwoPrefab.SetActive(false);
    }

    public void ShowSlashThreeEffect()
    {
        slashThreePrefab.SetActive(true);
    }

    public void HideSlashThreeEffect()
    {
        slashThreePrefab.SetActive(false);
    }

    public void ShowSlashFourEffect()
    {
        slashFourPrefab.SetActive(true);
    }

    public void HideSlashFourEffect()
    {
        slashFourPrefab.SetActive(false);
    }

    public void ShowSlashBigEffect()
    {
        slashFivePrefab.SetActive(true);
    }

    public void HideSlashBigEffect()
    {
        slashFivePrefab.SetActive(false);
    }
}
