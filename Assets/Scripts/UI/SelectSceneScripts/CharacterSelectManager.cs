using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ClassIcon
{
    Warrior = 0,
    Archer,
    Wizard,
    Empty
};

public struct CharacterInfo
{
    public ClassIcon Icon; // 캐릭터 클래스 엠블렘
    public string Name; // 캐릭터 이름
    public byte Level; // 레벨
    public string PrimaryDeco; // 칭호
}

public class CharacterSelectManager : MonoBehaviour
{
    public Button[] CreateChaButton = new Button[4]; // 캐릭터 생성 버튼
    public GameObject[] ChaInfoPanel = new GameObject[4]; // 캐릭터 패널
    public CharacterInfo[] chaInfos = new CharacterInfo[4];

    private void Awake()
    {
        for (int i = 0; i < 4; i++)
        {
            chaInfos[i].Icon = ClassIcon.Empty;
        }

        // Editer
        //TestDataContainer.GetCharacterInfo(ref chaInfos);
        // //////
    }

    private void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            CreateChaButton[i].gameObject.SetActive(true);
            CreateChaButton[i].onClick.AddListener(()=> { CreateSceneLoad(); });
            ChaInfoPanel[i].SetActive(false);
        }

        for (int i = 0; i < 4; i++)
        {
            if (chaInfos[i].Icon != ClassIcon.Empty)
            {
                CreateChaButton[i].gameObject.SetActive(false);
                ChaInfoPanel[i].SetActive(true);
                ChaInfoPanel[i].GetComponent<CharacterInfoUI>().SetInfoUI(chaInfos[i]);
            }
        }
    }

    public void CreateSceneLoad()
    {
        SceneFader.LoadScene("CreateCharacter", FadeType.Loding);
    }

    public void StartGame()
    {
        SceneFader.LoadScene("SampleScene", FadeType.Loding);
    }
}
