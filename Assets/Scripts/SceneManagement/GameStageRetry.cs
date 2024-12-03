using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStageRetry : MonoBehaviour
{
    [SerializeField] private Button retryButton;

    private void Start()
    {
        retryButton.onClick.AddListener(ReloadGameScene);
    }

    private void ReloadGameScene()
    {
        Loader.Load(Loader.Scene.GameScene);
        GameManager.Instance.playerAlive = true;
    }
}
