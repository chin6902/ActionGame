using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponHandler : MonoBehaviour
{
    [SerializeField] private GameObject EnemyWeapon;
    [SerializeField] private GameObject BreathWeapon;
    [SerializeField] private GameObject ScreamWeapon;


    public void EnableBreathWeapon()
    {
        BreathWeapon.SetActive(true);
    }

    public void DisableBreathWeapon()
    {
        BreathWeapon.SetActive(false);
    }

    public void EnableWeapon()
    {
        EnemyWeapon.SetActive(true);
    }

    public void DisableWeapon()
    {
        EnemyWeapon.SetActive(false);
    }

    public void EnableScreamWeapon()
    {
        ScreamWeapon.SetActive(true);
    }

    public void DisableScreamWeapon()
    {
        ScreamWeapon.SetActive(false);
    }
}
