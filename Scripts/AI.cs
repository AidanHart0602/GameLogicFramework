using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class AI : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _wayPoints;
    private NavMeshAgent _agent;

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        transform.position = _wayPoints[0].transform.position;  
    }

    void Update()
    {
        _agent.destination = _wayPoints[1].transform.position;
    }
}
