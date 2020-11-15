using UnityEngine;
using UnityEngine.UI;
using MihyazUtils.Timer;
using Zenject;

public class Player : MonoBehaviour
{
	private AnimatorManager _animatorManager;
	private EventBase _eventBase;
	private EnemyPool _enemyPool;
	private CollectablePool _collectablePool;

	[Inject]
	private void OnInstaller(
		EventBase eventBase,
		AnimatorManager animatorManager,
		EnemyPool enemyPool,
		CollectablePool collectablePool)
	{
		_eventBase = eventBase;
		_animatorManager = animatorManager;
		_enemyPool = enemyPool;
		_collectablePool = collectablePool;
	}

	#region Interface Objects
	private IMove _movement;
	private IState _state;
	private IComponent _component;
	#endregion

	private void Start()
	{
		_eventBase.Subscribe(EventTypes.OnGameOver, () =>
		{
			_component.HandleReset();
			_state.HandleSave();
			enabled = false;

			_animatorManager.TweenGameOver();
		});

		_eventBase.Subscribe(EventTypes.OnGameRestart, () =>
		{
			_component.HandleRestart();
			_state.Score = 0;
			enabled = true;

			_animatorManager.TweenRestart();

		});
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
		_movement.Rotate(SpeedTypes.Player);
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
		if (collision.TryGetComponent(out Collectable item1))
		{
			SoundManager.Instance.PlaySingle(SoundTypes.Score);
			_collectablePool.Despawn(item1);
			_state.IncreaseScore();
		}
		if (collision.TryGetComponent(out Enemy item2))
		{
			GameManager.Instance.GameOver();
			SoundManager.Instance.PlaySingle(SoundTypes.Die);
			StartCoroutine(MihyazDelay.Delay(1.3f, () => SoundManager.Instance.PlaySingle(SoundTypes.GameOver)));
		}
	}

}
