using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDEAD : PlayerFSMState
{
    private void OnEnable()
    {
        StartCoroutine(DeadAnim());
    }

    private IEnumerator DeadAnim()
    {
        manager.visualManager.PlayDeadAnim();

        Debug.Log("AnimationWaiting");
        Debug.Log("isAnimating : " + manager.visualManager.isAnimating);
        yield return new WaitUntil(() => manager.visualManager.isAnimating);

        manager.isDamageable = false;
        Debug.Log("isDamageable : " + manager.isDamageable);

        Debug.Log("EndWaiting");
        Debug.Log("isAnimating : " + manager.visualManager.isAnimating);
        yield return new WaitWhile(() => manager.visualManager.isAnimating);
        
        Debug.Log("EndAnimation");
        Debug.Log("isAnimating : " + manager.visualManager.isAnimating);
        
        //주금
    }
}
