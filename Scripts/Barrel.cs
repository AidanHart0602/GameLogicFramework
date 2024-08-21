using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    [SerializeField]
    private GameObject _explosion;
    private MeshRenderer _barrel;
    private Collider _collider; 
    public void ActivateExplosion()
    {
        _collider = GetComponent<Collider>();
        _barrel = GetComponent<MeshRenderer>();
        Debug.Log("Spark activated");
        StartCoroutine(Spark());
    }

    IEnumerator Spark()
    {
        _explosion.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        ComponentActivater(false);
        yield return new WaitForSeconds(35f);
        Debug.Log("Finished Cooldown");
        ComponentActivater(true);
    }
    private void ComponentActivater(bool trigger)
    {
        _barrel.enabled = trigger;
        _collider.enabled = trigger;
    }
}
