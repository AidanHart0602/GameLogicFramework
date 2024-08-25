using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _aiPool;
    [SerializeField]
    private GameObject _aiPrefab;
    [SerializeField]
    private GameObject _aiContainer;
    [SerializeField]
    private Transform _startingPoint;
    private int _enemyLimit = 90;
    [SerializeField]
    private int _spawnedEnemies = 0;
    [SerializeField]
    private int _numberOfEnemies;
    private bool _gameActive = true;
    [SerializeField]
    private GameObject _barrelPrefab;
    [SerializeField]
    private GameObject[] _barrelSpawns;

    void Start()
    {
        _storeAI(20);
        _numberOfEnemies = _enemyLimit;
        foreach (var BarrelSpawn in _barrelSpawns)
        {
            Instantiate(_barrelPrefab, BarrelSpawn.transform);
        }
        StartCoroutine(EnableAI());
    }
    List<GameObject> _storeAI(int NumOfAI)
    {
        for(int i = 0; i < NumOfAI; i++)
        {
            GameObject enemy = Instantiate(_aiPrefab, _startingPoint.transform.position, Quaternion.identity);
            enemy.transform.parent = _aiContainer.transform;
            enemy.SetActive(false);
            _aiPool.Add(enemy);

        }
        return _aiPool;
    }
    public void LowerNumber()
    {
        _numberOfEnemies--;
        UIManager.UIinstance.RemainingBots(_numberOfEnemies);
    }

    public void StopSpawning()
    {
        _gameActive = false;
        if(_gameActive == false)
        {
            foreach(var enemy in _aiPool)
            {
                Destroy(enemy);
            }
        }
    }
  
    IEnumerator EnableAI()
    {
        while(_spawnedEnemies < _enemyLimit && _gameActive == true)
        {
            foreach (var enemy in _aiPool)
            {
                if (enemy.activeInHierarchy == false)
                {
                    Debug.Log("Spawned Enemy");
                    _spawnedEnemies = _spawnedEnemies + 1;
                    enemy.SetActive(true);
                    break;
                }
            }
            yield return new WaitForSeconds(3.0f);
        }

        while(_gameActive == true)
        {
            if (_spawnedEnemies == _enemyLimit && _numberOfEnemies == 0)
            {
                UIManager.UIinstance.EndScene();
                StopSpawning();
            }
            yield return null;
        }
    }
}
