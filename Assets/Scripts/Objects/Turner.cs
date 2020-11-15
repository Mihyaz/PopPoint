using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class Turner : MonoBehaviour
{
    [SerializeField] private GameObject _shader;

    private Player _player;
    private EventBase _eventBase;
    private IMove _movement;

    [Inject]
    private void OnInstaller(EventBase eventBase, Player player)
    {
        _eventBase = eventBase;
        _player = player;
    }

    private void OnEnable()
    {
        _movement.RotationDegree = Random.Range(-1, 2);
        if (_movement.RotationDegree == 0)
            _movement.RotationDegree = -1;
        _movement.Transform.rotation = Quaternion.Euler(new Vector3(0, 0, _player.transform.rotation.eulerAngles.z + 90 * _movement.RotationDegree));
        _shader.gameObject.SetActive(true);
    }

    private void Awake()
    {
        _movement = GetComponent<IMove>();
        _movement.Transform = transform;
    }

    void Start()
    {
        _eventBase.Subscribe(EventTypes.OnGameOver, DisableGameObject);
        _movement.RotationSpeed = 5f;
    }
    
    void Update()
    {
        _movement.Rotate(SpeedTypes.Turner);
    }

    private void OnDisable()
    {
        _shader.gameObject.SetActive(false);
    }

    private void DisableGameObject()
    {
        if (this.gameObject != null)
            gameObject.SetActive(false);
    }
}
