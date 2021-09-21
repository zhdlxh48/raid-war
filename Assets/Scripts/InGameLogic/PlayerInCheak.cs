using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInCheak : InterectTrriger3D
{
    public override void OnEnter(GameObject go)
    {
        GameManager.OnGameRedy();
        Destroy(this.gameObject);
    }
}
