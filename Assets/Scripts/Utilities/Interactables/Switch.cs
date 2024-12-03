using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Switch : Interactable
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

        if(GameManager.Instance.Temple1_clear && GameManager.Instance.Temple2_clear)
        {
            PlayerStats.Instance.SaveStats();
            Loader.Load(Loader.Scene.BossStage);
        }
    }

}