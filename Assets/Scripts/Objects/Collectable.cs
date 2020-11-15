using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Collectable : MovingObject
{
    [SerializeField] private GameObject _particleSystem;
    private IMove _movement;
    private bool _isHit;

    [Inject]
    protected EventBase _eventBase;

    public void Construct()
    {
        _movement = GetComponent<IMove>();
        _eventBase.Subscribe(EventTypes.OnGameOver, DisableThisObject);
    }

    public void Initialize()
    {
        _movement.RotationSpeed = 5f;
        _movement.RotationDegree = 1f;
        _particleSystem.transform.parent = transform;
        _particleSystem.transform.localPosition = new Vector2(-0.02f, 3.07f);
        _isHit = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var position = _particleSystem.transform.position;
            _particleSystem.transform.parent = null;
            _particleSystem.transform.position = position;
            _particleSystem.SetActive(true);
        }
    }

    public void Reset()
    {
        _movement.Transform.position = new Vector2(0, 0);
    }

    public void DisableThisObject()
    {
        if(this.gameObject != null)
            gameObject.SetActive(false);
    }
}
