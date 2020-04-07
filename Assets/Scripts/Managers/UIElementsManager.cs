using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UIElementsManager : MonoBehaviour
{
    public Text Score;
    public Text NewScore;
    public Text Announcer;
    public Text Highscore;
    public Text Combo;

    public RectTransform GameOverCanvas;
    public GameObject InGameCanvas;

    private void Awake()
    {
       Score.text = "0";
       NewScore.text = "0";
       Highscore.text = "0";
       Announcer.text = "your score";
    }
}
