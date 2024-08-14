using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class AI : MonoBehaviour
{
    private enum _aiStates
    {
        Running,
        Hiding,
        Dead
    }
    private AudioSource _deathSound;
    private bool _hiding = false;
    private bool _hidingCooldown = true;
    private bool _deathTrigger = false;
    private _aiStates _states;
    private NavMeshAgent _agent;
    [SerializeField]
    private GameObject _endWaypoint;
    private Animator _anim;
    private GameObject _startWaypoint;
    private SpawnManager _spawnManager;

    void Start()
    {
        _deathSound = GetComponent<AudioSource>();
        if(_deathSound == null) 
        {
            Debug.Log("Audio Source is null");
        }

        _spawnManager = FindObjectOfType<SpawnManager>();
        _anim = GetComponent<Animator>();
        _endWaypoint = GameObject .FindGameObjectWithTag("Ending Waypoint");
        _startWaypoint = GameObject.FindGameObjectWithTag("Starting Waypoint");
        _states = _aiStates.Running;
        _agent = GetComponent<NavMeshAgent>();
        StartCoroutine(HidingCooldown());
    }
    private void Update()
    {

        switch (_states)
        {
            case _aiStates.Running:
                    _agent.isStopped = false;
                    _agent.destination = _endWaypoint.transform.position;
                break;
            case _aiStates.Hiding:
                if(_hiding == true)
                {
                    _agent.isStopped = true;
                }
                break;
            case _aiStates.Dead:
                if(_deathTrigger == true)
                {
                    _deathSound.Play();
                    _agent.isStopped = true;
                    StartCoroutine(Death());
                    _deathTrigger = false;
                }
                break;
        }
    }

    public void InitiateDeath()
    {
        _spawnManager.LowerNumbers();
        _deathTrigger = true;
        _states = _aiStates.Dead;
    }

    private IEnumerator Death()
    {
        _anim.SetTrigger("Death");
        yield return new WaitForSeconds(3.21f);
        this.transform.position = _endWaypoint.transform.position;
        this.gameObject.SetActive(false);
        _states = _aiStates.Running;

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Ending Waypoint")
        {
            this.transform.position = _startWaypoint.transform.position;
            this.gameObject.SetActive(false);
        }

        if(other.tag == "Hiding Spot")
        {
            gameObject.layer = 7;
            if (_hidingCooldown == false)
            {
                _hidingCooldown = true;
                _hiding = true;
                StartCoroutine(Hiding());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        gameObject.layer = 3;
    }
    private IEnumerator HidingCooldown()
    {
        int Cooldown = Random.Range(2, 20);
        yield return new WaitForSeconds(Cooldown);
        _hidingCooldown = false;
    }

    private IEnumerator Hiding()
    {
        float randomTime = Random.Range(2.0f,3.0f);
        yield return new WaitForSeconds(0.1f);
        _anim.SetTrigger("Hiding");
        _states = _aiStates.Hiding;
        yield return new WaitForSeconds(randomTime);
        _hiding = false;
        _anim.ResetTrigger("Hiding");
        _states = _aiStates.Running;
        StartCoroutine(HidingCooldown());  
    }
}