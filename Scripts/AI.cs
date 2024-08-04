using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class AI : MonoBehaviour
{
    private NavMeshAgent _agent;
    [SerializeField]
    private GameObject _endWaypoint;
    private GameObject _startWaypoint;
    void Start()
    {
        _endWaypoint = GameObject.FindGameObjectWithTag("Ending Waypoint");
        _startWaypoint = GameObject.FindGameObjectWithTag("Starting Waypoint");

        _agent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        _agent.destination = _endWaypoint.transform.position;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Ending Waypoint")
        {
            Debug.Log("Sphere has been reset");
            this.transform.position = _startWaypoint.transform.position;
            this.gameObject.SetActive(false);
        }
    }
}