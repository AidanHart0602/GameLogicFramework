using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingWaypoint : MonoBehaviour
{
    private AudioSource _buzzer;
    private int _escapeCounter;
    private SpawnManager _spawnManager;
    private void Start()
    {
        _spawnManager = FindObjectOfType<SpawnManager>();
        _buzzer = GetComponent<AudioSource>();
        if(_buzzer == null)
        {
            Debug.LogError("Buzzer audio source is null");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            Debug.Log("Buzzer Played");
            _buzzer.Play();
            _escapeCounter++;
        }

        if(_escapeCounter == 90)
        {
            _spawnManager.LowerNumber();
            UIManager.UIinstance.EndScene();
        }
    }
}
