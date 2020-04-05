﻿using UnityEngine;
using UnityEngine.UI;
using OnurMihyaz;
using Zenject;

public class Player : MonoBehaviour
{
    [Inject] AnimatorManager AnimatorManager;

    public ObjectPooler ObjectPooler;
    public Button SwingButton;

    #region Interface Objects
    private IMove _movement;
    private IState _state;
    private IComponent _component;
    #endregion

    private void Start()
    {
        
        GameManager.OnGameOver += () =>
        {
            _state.HandleSave();
            SwingButton.interactable = false;
        };

        GameManager.OnRestart += () =>
        {
            _component.HandleRestart();
            _state.Score = 0;
            enabled = true;

            AnimatorManager.GameOver.SetTrigger("restart");
            ObjectPooler.enabled = true;
            SwingButton.interactable = true;
        };

    }

    private void Awake()
    {
        _movement = GetComponent<IMove>();
        _state = GetComponent<IState>();
        _component = GetComponent<IComponent>();     
    }
    
    private void OnEnable()
    {
        _movement.RotationSpeed = 10f;
        _movement.RotationDegree = 1;
    }

    private void Update()
    {

        transform.Rotate(0, 0, _movement.RotationSpeed * Time.deltaTime * 15 * _movement.RotationDegree);
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Swing();
        }
#endif
    }

    public void Swing()
    {
        SoundManager.Instance.PlaySingle(SoundTypes.Swing);
        _movement.RotationDegree *= -1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ruvy"))
        {
            SoundManager.Instance.PlaySingle(SoundTypes.Score);
            collision.gameObject.SetActive(false);
            _state.IncreaseScore();
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            SoundManager.Instance.PlaySingle(SoundTypes.Die);
            enabled = false;
            _component.HandleReset();
            AnimatorManager.GameOver.SetTrigger("gameOver");
            StartCoroutine(MihyazDelay.Delay(1.3f, () => SoundManager.Instance.PlaySingle(SoundTypes.GameOver)));
            GameManager.Instance.GameOver();
            ObjectPooler.enabled = false;
        }
    }

}
