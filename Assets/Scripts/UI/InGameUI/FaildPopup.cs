using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum FaildType
{
    PlayerDie,
    TimeLimit
}

public class FaildPopup : MonoBehaviour
{
    public Text typeText;
    public Text faildInfo;
    public AudioClip faildSound;
    public Image imageInfo;
    public Sprite die;
    public Sprite TimeOut;

    public void OnFaildPoup(FaildType type)
    {
        this.gameObject.SetActive(true);
        Core.SoundManager.OneShot(faildSound);
       
        switch (type)
        {
            case FaildType.PlayerDie:
                imageInfo.sprite = die;
                imageInfo.SetNativeSize();
                //typeText.text = "캐릭터가 사망하였습니다.";
                break;
            case FaildType.TimeLimit:
                imageInfo.sprite = TimeOut;
                imageInfo.SetNativeSize();
                //typeText.text = "제한시간이 모두 만료되었습니다.";
                break;
        }

        faildInfo.text = $"[크라나키안 골렘] 남은 체력: {System.Math.Round((float)(GameManager.boss.curHP / (GameManager.boss.stats.HP / 100)), 1)}%";
    }

    public void ReStart()
    {
        SceneFader.LoadScene("CreateCharacter", FadeType.Loding);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
