using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameProgress : MonoBehaviour
{
    [SerializeField] private GameObject gameOverCanvas;
    [SerializeField] private GameObject gameClearCanvas;

    private void Start()
    {
        if(gameOverCanvas != null)
        {
            gameOverCanvas.SetActive(false);
        }
        if(gameClearCanvas != null)
        {
            gameClearCanvas.SetActive(false);
        }
    }

    private void Update()
    {
        if (GameManager.Instance.playerAlive == false)
        {
            StartCoroutine(PopGameOverCanvas());
        }

        if(GameManager.Instance.gameClear == true)
        {
            gameClearCanvas.SetActive(true);
        }
    }

    private IEnumerator PopGameOverCanvas()
    {
        yield return new WaitForSeconds(1.5f);

        gameOverCanvas.SetActive(true);
    }
}
