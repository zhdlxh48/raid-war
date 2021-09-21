using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffect : MonoBehaviour
{
    WaitForSeconds waitTime = new WaitForSeconds(0.3f);
    private void OnEnable()
    {
        StartCoroutine(Active());
    }
    IEnumerator Active()
    {
        yield return waitTime;
        this.gameObject.SetActive(false);
    }
}
