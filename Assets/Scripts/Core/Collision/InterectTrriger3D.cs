using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class InterectTrriger3D : MonoBehaviour
{
    public string trrTag;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(trrTag))
            OnEnter(other.gameObject);
    }

    public virtual void OnEnter(GameObject go) { }
}
