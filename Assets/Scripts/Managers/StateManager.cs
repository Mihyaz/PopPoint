using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using DG.Tweening;

public class StateManager : MonoBehaviour, IState
{
    [Inject]
    UIElementsManager UIElementsManager;
    [Inject]
    Turner Turner;

    private int _score;
    private int _collectableCollected;

    public int Score
    {
        get => _score;
        set
        {
            _score = value;
            UIElementsManager.Score.gameObject.transform.DOPunchScale(new Vector2(0.25f, 0.25f), 0.25f).OnComplete(() =>
            {
                UIElementsManager.Score.gameObject.transform.DOScale(new Vector2(1, 1), 0.25f);
            });
            UIElementsManager.Score.text = _score.ToString();
        }
    }

    public int CollectableCollected
    {
        get => _collectableCollected;
        set
        {
            _collectableCollected = value;
            if (_collectableCollected == 5)
            {
                Turner.gameObject.SetActive(false);
                _collectableCollected = 0;
            }
        }
    }
    private void Start()
    {
        UIElementsManager.Highscore.text = Highscore.ToString();
        Highscore = SaveManager.LoadScore();
    }
    public void HandleSave()
    {
        if (Score > Highscore)
        {
            Highscore = Score;
            SaveManager.SaveScore(Highscore);
            Highscore = SaveManager.LoadScore();
            UIElementsManager.Announcer.text = "new best";
        }
        else
            UIElementsManager.Announcer.text = "best " + Highscore;

        UIElementsManager.NewScore.text = UIElementsManager.Score.text;
        CollectableCollected = 0;
    }

    public void IncreaseScore()
    {
        Score++;
        CollectableCollected++;
    }

    public int Highscore { get ; set; }
    public bool IsHit { get; set; }
}
