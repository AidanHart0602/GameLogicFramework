using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class AI : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _wayPoints;
    private NavMeshAgent _agent;
    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        transform.position = _wayPoints[0].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        _agent.destination = _wayPoints[1].transform.position;
    }
}
