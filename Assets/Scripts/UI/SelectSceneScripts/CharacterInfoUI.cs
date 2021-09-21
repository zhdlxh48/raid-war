using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterInfoUI : MonoBehaviour
{
    private Image icon;
    private Text[] texts = new Text[4];
    public Sprite[] iconSprites;

    // Start is called before the first frame update

    void OnEnable()
    {
        icon = transform.GetChild(0).GetComponent<Image>();

        for (int i = 0; i < 4; i++)
        {
            texts[i] = transform.GetChild(i + 1).GetComponent<Text>();
        }
    }

    public void SetInfoUI(CharacterInfo info)
    {
        Debug.Log(info.Icon);
        icon.sprite = SelectIcon(info.Icon);
        texts[0].text = info.Name;
        texts[1].text = SelectClassName(info.Icon);
        texts[2].text = $"Lv.{info.Level.ToString()}";
        texts[3].text = info.PrimaryDeco;
    }

    public Sprite SelectIcon(ClassIcon icon)
    {
        switch (icon)
        {
            case ClassIcon.Warrior:
                return iconSprites[0];
            case ClassIcon.Archer:
                return iconSprites[1];
            // case ClassIcon.Wizard:
            default:
                return iconSprites[2];
        }
    }

    public string SelectClassName(ClassIcon icon)
    {
        switch (icon)
        {
            case ClassIcon.Warrior:
                return "전사";
            case ClassIcon.Archer:
                return "아처";
            // case ClassIcon.Wizard:
            default:
                return "위자드";
        }
    }
}
