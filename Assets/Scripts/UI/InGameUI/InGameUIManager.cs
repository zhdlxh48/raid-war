using UnityEngine;
using UnityEngine.UI;
using System;

public class InGameUIManager : MonoBehaviour
{
    //public Text bossName;
    public Image bossHpBar;
    public Image playerHpBar;
    public Image skillCoolBar;
    public GameObject skillIcon;

    public Text timeLimitText;
    public Image timeLimitBar;

    private float bossHPPer;
    private float playerHPPer;
    private float skillCoolPer;

    private float timePer;
    public ClearPopup clearPopup;
    public FaildPopup faildPopup;

    private void Awake()
    {
        bossHPPer = 1.0f / GameManager.boss.stats.HP;
        playerHPPer = 1.0f / GameManager.player.statusManager.Health;
        skillCoolPer = 1.0f / GameManager.player.timeManager.skillAttackTimers[0].coolTime;
        timePer = 1.0f / GameManager.timeRemaining;
        skillIcon.SetActive(false);
    }

    public void RefreshUITick()
    {
        bossHpBar.fillAmount = bossHPPer * GameManager.boss.curHP;
        playerHpBar.fillAmount = playerHPPer * GameManager.player.statusManager.Health;
        skillCoolBar.fillAmount = skillCoolPer * GameManager.player.timeManager.skillAttackTimers[0].timer;

        if (skillCoolBar.fillAmount >= 1.0f)
        {
            skillIcon.SetActive(true);
        }
        else
            skillIcon.SetActive(false);

        timeLimitBar.fillAmount = timePer * GameManager.timeRemaining;
        int a = (int)Math.Truncate(GameManager.timeRemaining % 60);
        if (a < 10)
            timeLimitText.text = $"{Math.Truncate(GameManager.timeRemaining / 60).ToString()} : 0{Math.Truncate(GameManager.timeRemaining % 60).ToString()}";
        else
            timeLimitText.text = $"{Math.Truncate(GameManager.timeRemaining / 60).ToString()} : {Math.Truncate(GameManager.timeRemaining % 60).ToString()}";
    }
}
