using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public List<GameObject> CollectableList = new List<GameObject>();
    public List<GameObject> EnemyList = new List<GameObject>();

    public GameObject Turner;
    private void Awake()
    {
        GameManager.OnGameOver += () => { enabled = false; };
        GameManager.OnRestart  += () => { enabled = true; };
    }

    private void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            Turner.SetActive(false);
            EnemyList[i].SetActive(false);
        }
    }

    public GameObject GetPooledCollectable()
    {
        for (int i = 0; i < CollectableList.Count; i++)
        {
            if (!CollectableList[i].activeInHierarchy)
            {
                return CollectableList[i];
            }
        }
        return null;
    }

    public GameObject GetPooledTurner()
    {
        if(!Turner.activeInHierarchy)
        {
            return Turner;
        }
        return null;
    }

    public GameObject GetPooledEnemy()
    {
        for (int i = 0; i < EnemyList.Count; i++)
        {
            if (!EnemyList[i].activeInHierarchy)
            {
                return EnemyList[i];
            }
        }
        return null;
    }

    private void OnEnable()
    {
        // I know I hate this string parameters and I could have use my own delay library 
        // But here comes the laziness!!!!
        InvokeRepeating("InstantiateEnemy", 2, 1.25f);
        InvokeRepeating("InstantiateTurner", 4, 2);
    }

    private void InstantiateEnemy()
    {
        GameObject Enemy = GetPooledEnemy();
        if (Enemy != null)
            Enemy.gameObject.SetActive(true);
    }

    private void InstantiateCollectable()
    {
        GameObject Collectable = GetPooledCollectable();
        if(Collectable != null)
            Collectable.gameObject.SetActive(true);
    }

    private void InstantiateTurner()
    {
        GameObject Turner = GetPooledTurner();
        if(Turner != null)
        {
            Turner.gameObject.SetActive(true);

            for (int i = 0; i < CollectableList.Count; i++)
            {
                 CollectableList[i].SetActive(true);
            }
        }
    }

    private void OnDisable()
    {
        CancelInvoke();
    }
}
