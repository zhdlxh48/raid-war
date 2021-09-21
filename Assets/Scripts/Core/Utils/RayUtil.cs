using System;
using System.Collections.Generic;
using UnityEngine;

public class RayUtil
{
    public static bool FireRay(ref RaycastHit hit, int hitLayer)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        return Physics.Raycast(ray, out hit, Mathf.Infinity, hitLayer);
    }

    public static bool FireRay(ref RaycastHit hit)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        return Physics.Raycast(ray, out hit, Mathf.Infinity);
    }

    public static int LayerMaskHitRay(ref RaycastHit hit)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            return 1 << hit.transform.root.gameObject.layer;
        }

        return -1;
    }
}