using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class SwingButton : MonoBehaviour
{
    [Inject]
    Player _player;

    private Button _button;
    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void Start()
    {
        GameManager.OnGameOver += () => { _button.interactable = false; };
        GameManager.OnRestart  += () => { _button.interactable = true; };
        _button.onClick.AddListener(() => { _player.Swing(); });
    }
}
