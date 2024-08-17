using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    [SerializeField]
    private GameObject _explosion;
    [SerializeField]
    public MeshRenderer _barrel;

    public void ActivateExplosion()
    {
        Debug.Log("Spark activated");
        StartCoroutine(Spark());
    }

    IEnumerator Spark()
    {
        _explosion.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        _barrel.enabled = false;
        yield return new WaitForSeconds(5);
        Destroy(this);
    }
}
