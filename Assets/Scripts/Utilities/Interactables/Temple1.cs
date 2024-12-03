using System.Collections;
using UnityEngine;

public class Temple1 : Interactable
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

        if(GameManager.Instance.RedCrystalCount >= 3 )
        {
            GameManager.Instance.RedCrystalCount -= 3;
            GameManager.Instance.Temple1_clear = true;
        }
    }

}