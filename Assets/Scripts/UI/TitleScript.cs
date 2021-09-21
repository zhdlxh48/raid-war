using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScript : MonoBehaviour
{
    public AudioClip titleBgm;

    private void Start()
    {
        Core.SoundManager.SetBGM(titleBgm);
    }

    private void Update()
    {
        if(Input.anyKey)
            SceneFader.LoadScene("SampleScene", FadeType.Loding);
    }
}
