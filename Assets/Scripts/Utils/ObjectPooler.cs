using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ObjectPooler : MonoBehaviour
{
    public GameObject Turner;

    private EventBase _eventBase;
    private EnemyPool _enemyPool;
    private CollectablePool _collectablePool;

    [Inject]
    private void OnInstaller(EventBase eventBase, EnemyPool enemyPool, CollectablePool collectablePool)
	{
        _eventBase = eventBase;
        _enemyPool = enemyPool;
        _collectablePool = collectablePool;
    }

    private void Awake()
    {
        _eventBase.Subscribe(EventTypes.OnGameOver,    () => { enabled = false; });
        _eventBase.Subscribe(EventTypes.OnGameRestart, () => { enabled = true; });
    }


    public GameObject GetPooledTurner()
    {
        if(!Turner.activeInHierarchy)
        {
            return Turner;
        }
        return null;
    }

    private void OnEnable()
    {
        // I know I hate this string parameters
        InvokeRepeating(nameof(InstantiateEnemy), 2, 1.25f);
        InvokeRepeating(nameof(InstantiateTurner), 4, 2);
    }

    private void InstantiateEnemy()
    {
        _enemyPool.Spawn();
    }

    private void InstantiateTurner()
    {
        GameObject Turner = GetPooledTurner();
        if(Turner != null)
        {
            Turner.gameObject.SetActive(true);

            for (int i = 0; i < _collectablePool.NumTotal; i++)
            {
                _collectablePool.Spawn();
            }
        }
    }

    private void OnDisable()
    {
        CancelInvoke();
    }
}
