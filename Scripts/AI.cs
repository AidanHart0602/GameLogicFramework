using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class AI : MonoBehaviour
{
    private enum _aiStates
    {
        Idle,
        Running,
        Hiding,
        Dead
    }
    private _aiStates _states;
    private NavMeshAgent _agent;
    [SerializeField]
    private GameObject _endWaypoint;
    private Animator _anim;
    private GameObject _startWaypoint;
    private bool _hiding = false;
    private bool _hidingCooldown = true;
    void Start()
    {

        _anim = GetComponent<Animator>();
        _endWaypoint = GameObject.FindGameObjectWithTag("Ending Waypoint");
        _startWaypoint = GameObject.FindGameObjectWithTag("Starting Waypoint");
        _states = _aiStates.Running;
        _agent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {

        switch (_states)
        {
            case _aiStates.Running:
                if (_hiding == false)
                {
                    _agent.isStopped = false;
                    _agent.destination = _endWaypoint.transform.position;
                }
                break;
            case _aiStates.Hiding:
                if(_hiding == true)
                {
                    _agent.isStopped = true;
                }
                break;
            case _aiStates.Dead:
                _agent.isStopped = true;
                _anim.SetTrigger("Death");
                this.gameObject.SetActive(false);
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Ending Waypoint")
        {
            Debug.Log("AI has been reset");
            this.transform.position = _startWaypoint.transform.position;
            this.gameObject.SetActive(false);
        }

        if(other.tag == "Hiding Spot")
        {
            if(_hidingCooldown == false)
            {
                _hidingCooldown = true;
                _hiding = true;
                _agent.destination = other.transform.position;
                StartCoroutine(Hiding());
            }
            else
            {
                StartCoroutine(HidingCooldown());
            }
        }
    }

    private IEnumerator HidingCooldown()
    {
        float Cooldown = Random.Range(4f, 20f);
        yield return new WaitForSeconds(Cooldown);
        _hidingCooldown = false;
    }

    private IEnumerator Hiding()
    {
        float randomTime = Random.Range(2.0f,5.0f);
        yield return new WaitForSeconds(1f);
        _anim.SetTrigger("Hiding");
        _states = _aiStates.Hiding;
        yield return new WaitForSeconds(randomTime);
        _hiding = false;
        _anim.ResetTrigger("Hiding");
        StartCoroutine(HidingCooldown());
        _states = _aiStates.Running;
    }
}