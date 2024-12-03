using System.Collections;
using UnityEngine;

public class MarketPlace : Interactable
{
    [Header("Item Data")]
    [SerializeField] private string itemName;
    [SerializeField] private GameObject playerParent;
    [SerializeField] private GameObject StatsUI;

    public override void Start()
    {
        base.Start();
        interactableName = itemName;
    }

    protected override void Interaction()
    {
        base.Interaction();

        StatsUI.SetActive(true);
    }

}