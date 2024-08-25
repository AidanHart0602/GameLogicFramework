using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelExplosion : MonoBehaviour
{
    [SerializeField]
    private Collider _collider;
    private void Start()
    {
        _collider.enabled = false;
        StartCoroutine(ActiveCollider());
    }

    IEnumerator ActiveCollider()
    {
        yield return new WaitForSeconds(1.0f);
        _collider.enabled = true;
        yield return new WaitForSeconds(.5f);
        _collider.enabled = false;
        yield return new WaitForSeconds(3.5f);
        this.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 3)
        {
            AI robot = other.GetComponent<AI>();
            robot.InitiateDeath();
        }
    }
}
