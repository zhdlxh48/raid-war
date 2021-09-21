using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedItem : MonoBehaviour, IEffectItemBase
{
   // private PlayerManager target;
    private PlayerStatusData targetData;

    private Renderer itemRender;
    private Collider[] itemColliders;

    public float speedMultiple;
    public float remainSecond;

    public void Awake()
    {
        itemRender = GetComponentInChildren<Renderer>();
        itemColliders = GetComponents<Collider>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            //target = other.transform.root.GetComponent<PlayerManager>();
            //targetData = target.data;

            ItemEffects();
        }
    }

    public void ItemEffects()
    {
        targetData.MoveSpeed *= speedMultiple;

        StartCoroutine("ReleaseEffects");
    }

    public IEnumerator ReleaseEffects()
    {
        itemRender.enabled = false;
        foreach (var item in itemColliders)
            item.enabled = false;

        yield return new WaitForSeconds(remainSecond);

        targetData.MoveSpeed /= speedMultiple;

        Destroy(gameObject);
    }
}
