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
    private float _enemyLimit = 0;

    void Start()
    {
        StoreAI(10);
        StartCoroutine(InstantiateAI());
    }

    List<GameObject> StoreAI(int NumofAI) 
    {
        for(int i = 0; i < NumofAI; i++)
        {
            GameObject NewEnemy = Instantiate(_aiPrefab, _startingPoint.transform.position, Quaternion.identity);
            NewEnemy.transform.parent = _aiContainer.transform;
            NewEnemy.SetActive(false);
            _aiPool.Add(NewEnemy);
            _enemyLimit++;
        }
        return _aiPool;
    }

    private void ActivateAI()
    {
        foreach(var enemy in _aiPool)
        {
            if(enemy.activeInHierarchy == false) 
            {
                enemy.SetActive(true);
                return;
            }
        }
        if(_enemyLimit < 15)
        {
            GameObject NewEnemy = Instantiate(_aiPrefab, _startingPoint.transform.position, Quaternion.identity);
            NewEnemy.transform.parent = _aiContainer.transform;
            _enemyLimit++;
            _aiPool.Add(NewEnemy);
            return;
        }
    }

    IEnumerator InstantiateAI()
    {
        while(_gameActive == true)
        {
            ActivateAI();
            yield return new WaitForSeconds(4f);
        }
    }
}
