using UnityEngine;
using UnityEngine.UI;
using System;

public class ResultPanel : MonoBehaviour
{
    public InputField nameField;
    public Text clearTime;
    public Text ranking;

    public void Init()
    {
        clearTime.text = "00 : 00";
        this.nameField.readOnly = true;
        this.gameObject.SetActive(false);
    }

    public void SetPanel(int ranking, string name, float clearTime)
    {
        this.gameObject.SetActive(true);
        this.ranking.text = ranking.ToString();
        this.nameField.text = name;

        int a = (int)Math.Truncate(clearTime % 60);
        if (a < 10)
            this.clearTime.text = $"{Math.Truncate(clearTime / 60)} : 0{Math.Truncate(clearTime % 60)}";
        else
            this.clearTime.text = $"{Math.Truncate(clearTime / 60)} : {Math.Truncate(clearTime % 60)}";
    }
}
