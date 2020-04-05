﻿using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class Turner : MonoBehaviour
{
    [Inject]
    private Player _player;

    [SerializeField] private GameObject _shader;

    private IMove _movement;
    private void OnEnable()
    {
        _movement.RotationDegree = Random.Range(-1, 2);
        if (_movement.RotationDegree == 0)
            _movement.RotationDegree = -1;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, _player.transform.rotation.eulerAngles.z + 90 * _movement.RotationDegree));
        _shader.gameObject.SetActive(true);
    }

    private void Awake()
    {
        _movement = GetComponent<IMove>();
    }

    void Start()
    {
        GameManager.OnGameOver += DisableGameObject;
        _movement.RotationSpeed = 5f;
    }
    
    void Update()
    {
        transform.Rotate(0, 0, _movement.RotationSpeed * Time.deltaTime * 10 * _movement.RotationDegree);
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