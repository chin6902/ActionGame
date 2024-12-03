using System.Collections;
using UnityEngine;

public class Temple2 : Interactable
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

        if (GameManager.Instance.BlueCrystalCount >= 3)
        {
            GameManager.Instance.BlueCrystalCount -= 3;
            GameManager.Instance.Temple2_clear = true;
        }
    }

}