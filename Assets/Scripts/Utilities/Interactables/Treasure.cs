using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : Interactable
{
    [Header("Item Data")]
    [SerializeField] private string itemName;

    public override void Start()
    {
        base.Start();
        interactableName = itemName;
    }

    protected override void Interaction()
    {
        base.Interaction();

        if(GameManager.Instance.dragonDead == true )
        {
            GameManager.Instance.gameClear = true;
        }
    }
}
