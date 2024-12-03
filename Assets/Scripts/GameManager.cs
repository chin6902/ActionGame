using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public float BlueCrystalCount;
    public float RedCrystalCount;
    public bool Temple1_clear;
    public bool Temple2_clear;
    public bool playerAlive;
    public bool dragonDead;
    public bool gameClear;


    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        BlueCrystalCount = 0;
        RedCrystalCount = 0;
        Temple1_clear = false;
        Temple2_clear = false;
        gameClear = false;
        playerAlive = true;
        dragonDead = false;
    }

    public void ResetGame()
    {
        BlueCrystalCount = 0;
        RedCrystalCount = 0;
        Temple1_clear = false;
        Temple2_clear = false;
        gameClear = false;
        playerAlive = true;
        dragonDead = false;
    }
}
