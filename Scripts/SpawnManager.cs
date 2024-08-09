using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private Transform _startingPoint;
    [SerializeField]
    public Transform _endingPoint;

    [SerializeField]
    private List<GameObject> _aiPool;
    [SerializeField]
    private GameObject _aiPrefab;
    [SerializeField]
    private GameObject _aiContainer;

    private bool _gameActive = true;

    [SerializeField]
    private int _numberofEnemies = 0;
    [SerializeField]
    private int _enemyLimit = 15;
    [SerializeField]
    private int _spawnedEnemies = 0;

    void Start()
    {
        StoreAI(_enemyLimit);
        StartCoroutine(InstantiateAI());
    }

    List<GameObject> StoreAI(int NumofAI)
    {
        for (int i = 0; i < NumofAI; i++)
        {
            GameObject NewEnemy = Instantiate(_aiPrefab, _startingPoint.transform.position, Quaternion.identity);
            NewEnemy.transform.parent = _aiContainer.transform;
            NewEnemy.SetActive(false);
            _aiPool.Add(NewEnemy);
            _numberofEnemies++;
            UIManager.UIinstance.RemainingBots(_numberofEnemies);
        }
        return _aiPool;
    }



    private void ActivateAI()
    {
        foreach(var enemy in _aiPool)
        {
            if(enemy.activeInHierarchy == false && _numberofEnemies > 0 && _spawnedEnemies != _enemyLimit) 
            {
                enemy.SetActive(true);
                _spawnedEnemies++;
                return;
            }
        }

        if(_numberofEnemies == 0) 
        {
            NextWave();
        }
    }

    public void LowerNumbers()
    {
        _numberofEnemies--;
        UIManager.UIinstance.RemainingBots(_numberofEnemies);
    }

    public void NextWave()
    {
        _spawnedEnemies = 0;
        _enemyLimit += 5;
        StoreAI(_enemyLimit);
        StartCoroutine(InstantiateAI());
    }

    IEnumerator InstantiateAI()
    {
        while(_gameActive == true)
        {
            ActivateAI();
            yield return new WaitForSeconds(3f);
        }
    }
}
