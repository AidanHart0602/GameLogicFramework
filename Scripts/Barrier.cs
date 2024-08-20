using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{

    public void BarrierDisabled()
    {
        StartCoroutine(Recharge());
    }

    IEnumerator Recharge()
    {
        this.gameObject.SetActive(false);
        yield return new WaitForSeconds(20.0f);
        this.gameObject.SetActive(true);
    }
}
