using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEffectHandler : MonoBehaviour
{
    [SerializeField] private GameObject AttackEffect;

    private void EffectOn()
    {
        AttackEffect.SetActive(true);
    }

    private void EffectOff()
    {
        AttackEffect.SetActive(false);
    }
}
