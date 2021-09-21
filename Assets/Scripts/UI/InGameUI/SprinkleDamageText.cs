using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DamageType
{
    Critical,
    Nomal
};

public enum HitActor
{
    Enemy = 0,
    Player
};

public class SprinkleDamageText : MonoBehaviour
{
    private static SprinkleDamageText _instance;
    public static SprinkleDamageText Instance
    {
        get
        {
            if (_instance != null)
                return _instance;
            _instance = FindObjectOfType<SprinkleDamageText>();
            return _instance;
        }
    }

    public GameObject nomalFont;
    public GameObject playerHitFont;
    public Transform bossTrans;
    public float aliveTime = 3;
    public float speed = 5;

    private static List<ImageText> criticalTextPool;
    private static List<FontText> nomalTextPool;
    private static int activeCount;

    // Start is called before the first frame update
    void Awake()
    {
        _instance = this;
        criticalTextPool = new List<ImageText>();
        nomalTextPool = new List<FontText>();

        for (int i = 0; i < 10; i++)
        {
            FontText font = Instantiate(nomalFont).GetComponent<FontText>();
            font.Init(aliveTime);
            font.transform.SetParent(this.transform);
            font.gameObject.SetActive(false);
            font.gameObject.hideFlags = HideFlags.HideInHierarchy;
            nomalTextPool.Add(font);
        }
    }

    // Update is called once per frame
    void Update()
    {
        int j = 0;
        activeCount = 0;
        for (int i = 0; i < nomalTextPool.Count; i++)
        {
            if (nomalTextPool[i].gameObject.activeSelf)
            {
                activeCount++;
                nomalTextPool[i].transform.Translate(Vector3.up * 0.3f);
                if (i >= 1)
                {
                    if ((nomalTextPool[i].transform.position - nomalTextPool[j].transform.position).sqrMagnitude < 30.0f * 30.0f)
                        nomalTextPool[j].transform.Translate(Vector3.up * 4.0f);
                    else
                        nomalTextPool[i].transform.Translate(Vector3.up * 0.3f);
                }
                j = i;
            }
        }
    }

    public static void OnDamageText(DamageType type, HitActor actor, Vector3 pos, int damage)
    {
        pos.y += 3.0f;
        switch (type)
        {
            case DamageType.Critical:
                for (int i = 0; i < criticalTextPool.Count; i++)
                {
                    if (!criticalTextPool[i].gameObject.activeSelf)
                    {
                        criticalTextPool[i].transform.position = Camera.main.WorldToScreenPoint(pos);
                        criticalTextPool[i].gameObject.SetActive(true);
                        criticalTextPool[i].Print(damage.ToString());
                        return;
                    }
                }
                break;

            case DamageType.Nomal:
                for (int i = 0; i < nomalTextPool.Count; i++)
                {
                    if (!nomalTextPool[i].gameObject.activeSelf)
                    {
                        nomalTextPool[i].transform.position = Camera.main.WorldToScreenPoint(pos);
                        if (activeCount > 1)
                        {
                            Instance.StartCoroutine(Instance.cheak(nomalTextPool[i]));
                        }
                        else
                            nomalTextPool[i].gameObject.SetActive(true);
                        nomalTextPool[i].Print(damage.ToString(), actor);
                        return;
                    }
                }
                break;
        }
    }

    IEnumerator cheak(FontText e)
    {
        yield return new WaitForSeconds(0.1f);
        e.gameObject.SetActive(true);
    }
}
