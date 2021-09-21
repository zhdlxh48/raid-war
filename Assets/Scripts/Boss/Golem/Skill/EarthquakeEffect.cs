using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthquakeEffect : MonoBehaviour
{
    public Transform[] effects;

    private WaitForSeconds wait = new WaitForSeconds(0.12f);

    private void Awake()
    {
        //effects = GetComponentsInChildren<Transform>();

        for (int i = 0; i < effects.Length; i++)
        {
            effects[i].gameObject.SetActive(false);
        }
    }

    public IEnumerator efstart(int damage)
    {
        for (int i = 0; i < effects.Length; i++)
        {
            effects[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < effects.Length; i++)
        {
            effects[i].gameObject.SetActive(true);
            if ((effects[i].position - GameManager.player.transf.position).sqrMagnitude < 3.0f * 3.0f)
                GameManager.player.OnDamage(damage);

            yield return wait;
        }
        yield return null;
    }
}
