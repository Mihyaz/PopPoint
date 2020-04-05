using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Enemy : MovingObject
{
    private Transform _transform;
    private IMove _movement;
    private bool _isHit;

    private void Awake()
    {
        _movement = GetComponent<IMove>();
        _transform = transform;
    }

    private void Start()
    {
        GameManager.OnGameOver += PlayAnim;
    }

    void OnEnable()
    {
        _transform.DOScale(0.3f, 0.5f);
        _movement.Angle = Random.Range(0, 360);
        _movement.RadAngle = _movement.Angle * Mathf.Deg2Rad;
        _isHit = false;
    }

    private void Update()
    {
        if (!_isHit)
        {
            float step = 1.25f * Time.deltaTime;
            _transform.position = Vector2.MoveTowards(_transform.position, new Vector2(Mathf.Cos(_movement.RadAngle) * 2, Mathf.Sin(_movement.RadAngle) * 2), step);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {       
        if (collision.gameObject.CompareTag("CircleArea"))
        {
            _isHit = true;
            DieTween();
        }
    }
    private void OnDisable()
    {
        _transform.position = new Vector2(0, 0);
    }

    public void PlayAnim()
    {
        if (this.gameObject != null)
            DieTween();
    }

    public Tween DieTween()
    {
        return _transform.DOScale(0, 0.5f).OnComplete(() =>
        {
            gameObject.SetActive(false);
        });
    }
}
