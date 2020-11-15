using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MihyazUtils.Events;
using Zenject;

public class GameManager : MonoBehaviour
{
    [Inject]
    EventBase _eventBase;

    public static GameManager Instance;
    public delegate void GameEventHandler();

    private void Awake()
    {
        Instance = this;
        Application.targetFrameRate = 300;
    }

    public void GameOver()
    {
        _eventBase.Invoke(EventTypes.OnGameOver);
    }

    public void Restart()
    {
        _eventBase.Invoke(EventTypes.OnGameRestart);
    }

    public void RateMe()
    {
        Application.OpenURL("market://details?id=" + Application.identifier);
    }
}
