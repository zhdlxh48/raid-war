using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusPanel : MonoBehaviour
{
    ClassIcon classIcon;
    public Pentagram pentagram;
    public GameObject SelectHiright;
    public GameObject[] skillPanel;

    private Vector3[] hirightPos = new Vector3[3];

    string[] ExplanationText =
    {
        "전사는 양손 대검을 휘두르는 캐릭터입니다.\n안정적인 체력을 기반으로 공격속도는 낮지만 한방 한방 강력한 무력화 공격을 합니다.",
        "궁수는 활을 사용하는 캐릭터 입니다.\n원거리 공격... 블라블라",
        "마법사는 마법을 사용하는 캐릭터 입니다.\n광범위 공격... 블라블라"
    };

    public Text explanationUI;

    private void Awake()
    {
        explanationUI.text = ExplanationText[0];
        pentagram.SetStetPentagram(ClassIcon.Warrior);
        classIcon = ClassIcon.Warrior;

        for (int i = 0; i < 3; i++)
        {
            hirightPos[i] = transform.GetChild(i + 2).GetComponent<RectTransform>().position;
        }
    }

    private void Start()
    {
        SkillPanelActive(classIcon);
    }

    private void SkillPanelActive(ClassIcon c)
    {
        for (int i = 0; i < skillPanel.Length; i++)
        {
            skillPanel[i].SetActive(false);
        }
        switch (c)
        {
            case ClassIcon.Warrior:
                skillPanel[0].SetActive(true);
                break;

            case ClassIcon.Archer:
                skillPanel[1].SetActive(true);
                break;

            case ClassIcon.Wizard:
                skillPanel[2].SetActive(true);
                break;
        }

        classIcon = c;
    }

    // Click Warrior Icon
    public void WarriorIcon()
    {
        explanationUI.text = ExplanationText[0];
        SelectHiright.transform.position = hirightPos[0];
        pentagram.SetStetPentagram(ClassIcon.Warrior);
        SkillPanelActive(ClassIcon.Warrior);
    }

    // Click Archer Icon
    public void ArcherIcon()
    {
        explanationUI.text = ExplanationText[1];
        SelectHiright.transform.position = hirightPos[1];
        pentagram.SetStetPentagram(ClassIcon.Archer);
        SkillPanelActive(ClassIcon.Archer);
    }

    // Click Wizard Icon
    public void WizardIcon()
    {
        explanationUI.text = ExplanationText[2];
        SelectHiright.transform.position = hirightPos[2];
        pentagram.SetStetPentagram(ClassIcon.Wizard);
        SkillPanelActive(ClassIcon.Wizard);
    }

    public ClassIcon GetCreateClass()
    {
        return classIcon;
    }
}
