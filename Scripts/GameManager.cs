using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get 
        {
            if(_instance == null)
            {
                Debug.LogError("Game manager is NULL");
            }
                return _instance;    
        }
    }

    [SerializeField]
    private List<GameObject>_aiPool;
    [SerializeField]
    private GameObject _aiPrefab;
    [SerializeField]
    private GameObject _aiContainer;

    private void Awake()
    {
        _instance = this;
        _aiPool = StoreAI(10);
    }

    List<GameObject> StoreAI(int NumOfAI)
    {
        for (int i = 0; i < NumOfAI; i++)
        {
            GameObject AI = Instantiate(_aiPrefab);
            AI.transform.parent = _aiContainer.transform;
            AI.SetActive(false);
            _aiPool.Add(AI);
        }
        return _aiPool;
    }
}
