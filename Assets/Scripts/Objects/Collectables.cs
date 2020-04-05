using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectables : MovingObject
{
    [SerializeField] private GameObject _particleSystem;
    private IMove _movement;
    private bool _isHit;

    private void Awake()
    {
        _movement = GetComponent<IMove>();
    }
    private void Start()
    {
        GameManager.OnGameOver += DisableThisObject;
    }
    
    void OnEnable()
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

    private void OnDisable()
    {
        gameObject.transform.position = new Vector2(0, 0);
    }

    public void DisableThisObject()
    {
        if(this.gameObject != null)
            gameObject.SetActive(false);
    }
}
