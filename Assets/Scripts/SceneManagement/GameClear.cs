using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameClear : MonoBehaviour
{
    [SerializeField] private Button homeButton;

    private void Start()
    {
        homeButton.onClick.AddListener(ReloadHomeScene);
    }

    private void ReloadHomeScene()
    {
        Loader.Load(Loader.Scene.MainMenu);
        GameManager.Instance.ResetGame();
    }
}
