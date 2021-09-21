using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEffectItemBase
{
    void OnTriggerEnter(Collider other);

    void ItemEffects();
    IEnumerator ReleaseEffects();
}
