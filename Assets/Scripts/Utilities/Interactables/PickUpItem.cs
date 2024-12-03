using System.Collections;
using UnityEngine;

public class PickUpItem : Interactable
{
    [Header("Item Data")]
    [SerializeField] private string itemName;
    [SerializeField] private GameObject ParentObject;

    public override void Start()
    {
        base.Start();
        interactableName = itemName;
    }

    protected override void Interaction()
    {
        base.Interaction();

        GameManager.Instance.RedCrystalCount++;

        Destroy(ParentObject);
    }

}