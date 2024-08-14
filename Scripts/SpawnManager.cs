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

    public bool _gameActive = true;

    [SerializeField]
    private int _numberOfEnemies = 0;
    [SerializeField]
    private int _enemyLimit;
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
            _numberOfEnemies++;
            UIManager.UIinstance.RemainingBots(_numberOfEnemies);
        }
        return _aiPool;
    }

    void CreateWave(int NumofAI)
    {
        for(int i = 0; i < NumofAI; i++)
        {
            UIManager.UIinstance.RemainingBots(_numberOfEnemies);
        }
    }


    private void ActivateAI()
    {
        foreach(var enemy in _aiPool)
        {
            if(enemy.activeInHierarchy == false && _numberOfEnemies > 0 && _spawnedEnemies < _enemyLimit) 
            {
                Debug.Log("spawned an enemy");
                enemy.SetActive(true);
                _spawnedEnemies++;
                return;
            }
        }

        if(_numberOfEnemies == 0) 
        {
            NextWave();
        }
    }
    public void LowerNumbers()
    {
        Debug.Log("Lowering Robot Count");
        _numberOfEnemies--;
        UIManager.UIinstance.RemainingBots(_numberOfEnemies);
    }

    public void EndSpawn()
    {
        foreach (var enemy in _aiPool)
        {
            Destroy(enemy);
        }
        _gameActive = false;
    }

    public void NextWave()
    {
        _spawnedEnemies = 0;
        _enemyLimit += 5;
        CreateWave(_enemyLimit);
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
