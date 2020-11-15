using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Zenject;

public class Enemy : MovingObject
{
    private IMove _movement;
    private bool _isHit;

    [Inject]
    private EnemyPool _enemyPool;
    [Inject]
    protected EventBase _eventBase;

    public void Construct()
    {
        _movement = GetComponent<IMove>();
        _movement.Transform = transform;
    }

    public void Initialize()
    {
        _eventBase.Subscribe(EventTypes.OnGameOver, Die);
        _movement.Transform.DOScale(new Vector2(0.3f, 0.3f), 0.5f);
        _movement.Angle = Random.Range(0, 360);
        _movement.RadAngle = _movement.Angle * Mathf.Deg2Rad;
        _isHit = false;
    }

    private void Update()
    {
        if (!_isHit)
        {
            float step = 1.25f * Time.deltaTime;
            _movement.Transform.position = Vector2.MoveTowards(_movement.Transform.position, new Vector2(Mathf.Cos(_movement.RadAngle) * 2, Mathf.Sin(_movement.RadAngle) * 2), step);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {       
        if (collision.gameObject.CompareTag("CircleArea"))
        {
            if (_isHit)
                return;
            Die();
        }
    }
    public void Reset()
    {
        _eventBase.Unsubscribe(EventTypes.OnGameOver, Die);
        _movement.Transform.position = new Vector2(0, 0);
    }

    public void Die()
    {
        _isHit = true;
        _movement.Transform.DOScale(0, 0.5f).OnComplete(() =>
        {
            _enemyPool.Despawn(this);
        });
    }
}
