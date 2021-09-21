using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FontText : MonoBehaviour
{
    Text fontTex;
    Vector3 vector;
    private WaitForSeconds aliveTime;
    private Animator anim;
    public float upSpeed;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        fontTex = GetComponent<Text>();
    }

    private void OnEnable()
    {
        StartCoroutine(Alive());
    }

    public void Print(string text, HitActor actor)
    {
        anim.SetInteger("Type", (int)actor);
        fontTex.text = text;
    }

    public void Init(float aliveTime)
    {
        upSpeed = 1.0f;
       // vector = new Vector3(1.5f, 1.5f, 1.5f);
        this.aliveTime = new WaitForSeconds(aliveTime);
    }

    IEnumerator Alive()
    {
        yield return aliveTime;

        this.gameObject.SetActive(false);
    }
}
