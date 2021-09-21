using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Boss;

public class BossUtil
{
    public static Transform[] GetBossLocations(BossMonsterBase[] bossArray)
    {
        Transform[] tempBossLocats = new Transform[bossArray.Length];
        int len = bossArray.Length;

      //  Debug.Log(len);

        for (int i = 0; i < len; i++)
        {
           // Debug.Log("GetBossLocations DO");
            tempBossLocats[i] = bossArray[i].transform;
        }

        return tempBossLocats;
    }

    public static BossMonsterBase[] GetBossComponents(Transform[] bossList)
    {
        BossMonsterBase[] tempBossComps = new BossMonsterBase[bossList.Length];
        int len = bossList.Length;

      //  Debug.Log(len);

        for (int i = 0; i < len; i++)
        {
           // Debug.Log("GetBossComponents DO");
            tempBossComps[i] = bossList[i].root.gameObject.GetComponent<BossMonsterBase>();
        }

        return tempBossComps;
    }
}
