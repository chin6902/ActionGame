using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameRetry : MonoBehaviour
{
    [SerializeField] private Button retryButton;

    private void Start()
    {
        retryButton.onClick.AddListener(ReloadBossScene);
    }

    private void ReloadBossScene()
    {
        Loader.Load(Loader.Scene.BossStage);
        GameManager.Instance.playerAlive = true;
    }
}
