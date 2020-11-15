using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class SwingButton : MonoBehaviour
{
    private Player _player;
    private EventBase _eventBase;

    private Button _button;

    [Inject]
    private void OnInstaller(EventBase eventBase, Player player)
	{
        _eventBase = eventBase;
        _player = player;
	}

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void Start()
    {
        _eventBase.Subscribe(EventTypes.OnGameOver,    () => { _button.interactable = false; });
        _eventBase.Subscribe(EventTypes.OnGameRestart, () => { _button.interactable = true; });

        _button.onClick.AddListener(() => { _player.Swing(); });
    }
}
