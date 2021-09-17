using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public delegate void GameEventHandler();

    public static event GameEventHandler OnGameOver;
    public static event GameEventHandler OnRestart;

    private void Awake()
    {
        Instance = this;
        Application.targetFrameRate = 300;
    }

    public void GameOver()
    {
        OnGameOver();
    }

    public void Restart()
    {
        OnRestart();
    }
    public void RateMe()
    {
        Application.OpenURL("market://details?id=" + Application.identifier);
    }


}
