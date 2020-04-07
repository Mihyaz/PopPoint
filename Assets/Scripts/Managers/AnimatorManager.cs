using UnityEngine;
using DG.Tweening;
using Zenject;
using OnurMihyaz;

public class AnimatorManager : MonoBehaviour
{
    [Inject]
    UIElementsManager UIElementsManager;

    public void TweenGameOver()
    {
        StartCoroutine(MihyazDelay.Delay(1, () =>
        {
            UIElementsManager.Score.DOFade(0f, 0.15f);
            StartCoroutine(MihyazDelay.Delay(0.15f, () =>
            {
                UIElementsManager.GameOverCanvas.DOAnchorPosY(0, 0.9f);
                UIElementsManager.InGameCanvas.transform.DOLocalMoveY(20, 1.25f);
            }));
        }));
    }
    public void TweenRestart()
    {
        UIElementsManager.Score.DOFade(1f, 2f);
        UIElementsManager.GameOverCanvas.DOAnchorPosY(-2000, 1f);
        UIElementsManager.InGameCanvas.transform.DOLocalMoveY(0, 0.75f);
    }
}
