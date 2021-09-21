using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateCharacter : MonoBehaviour
{
    public StatusPanel statusPanel;
    public InputField nickNameField;
    public GameObject exitPanel;

    private CharacterInfo info;

    private void Awake()
    {
        statusPanel = this.GetComponentInChildren<StatusPanel>();
        nickNameField = this.GetComponentInChildren<InputField>();

        Debug.Log(this.name);
    }

    private void Start()
    {
        //exitPanel.SetActive(false);
    }

    private void Update()
    {
        
    }

    public void CreateCharacterButton()
    {
        if (nickNameField.text == null)
            return;

        info.Icon = statusPanel.GetCreateClass();
        info.Name = nickNameField.text;
        info.Level = 1;
        info.PrimaryDeco = null;
        
        // Editor
       // TestDataContainer.AddCharacterInfo(info);
        // //////

        //SceneFader.LoadScene("SampleScene", FadeType.Loding);
    }

    public void BackButton()
    {
        Application.Quit();
        //exitPanel.SetActive(true);
    }
}
