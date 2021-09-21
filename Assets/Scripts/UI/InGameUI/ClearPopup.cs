using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Core;

public class ClearPopup : MonoBehaviour
{
    public ResultPanel panels;
    public Transform contents;

    public GameObject clearEx;
    public GameObject clearPopup;

    public Text resultTex;

    public ScrollRect scroll;

    private int curPanel;
    private List<ResultPanel> resultPanels;

    // Start is called before the first frame update
    void Awake()
    {
        resultPanels = new List<ResultPanel>();
    }

    public void OnPopup(float clearTime)
    {
        this.gameObject.SetActive(true);
        StartCoroutine(WaitPop(clearTime));
    }

    private WaitForSeconds wait = new WaitForSeconds(1.0f);

    IEnumerator WaitPop(float clearTime)
    {
        resultTex.text = "- 5초 후에 결과창으로 넘어갑니다 -";
        yield return wait;

        resultTex.text = "- 4초 후에 결과창으로 넘어갑니다 -";
        yield return wait;

        resultTex.text = "- 3초 후에 결과창으로 넘어갑니다 -";
        yield return wait;

        resultTex.text = "- 2초 후에 결과창으로 넘어갑니다 -";
        yield return wait;

        resultTex.text = "- 1초 후에 결과창으로 넘어갑니다 -";
        yield return wait;

        resultTex.text = "- 1초 후에 결과창으로 넘어갑니다 -";

        float sumPos = 0;
        int count = 0;

        clearEx.SetActive(false);
        clearPopup.SetActive(true);
        ClearInfomation info = new ClearInfomation("", clearTime);
        DataContainer.Datas.info.Add(info);
        DataContainer.SortData();

        for (int i = 0; i < DataContainer.Datas.info.Count; i++)
        {
            info = DataContainer.Datas.info[i];
            ResultPanel result = Instantiate(panels, contents);
            result.Init();

            if (info.playerName == "")
            {
                result.nameField.onValueChanged.AddListener(NameInputEvent);
                result.nameField.readOnly = false;
                result.SetPanel(i + 1, info.playerName, info.clearTime);
                resultPanels.Add(result);
                count = i;
                curPanel = i;
            }
            else
            {
                result.nameField.enabled = false;
                result.SetPanel(i + 1, info.playerName, info.clearTime);
                resultPanels.Add(result);
            }
            sumPos += 126.3f;
        }
        float per = 1.0f / sumPos;
        scroll.verticalNormalizedPosition = per * count;
    }

    public void ReStart()
    {
        OffPopup();
        Core.DataContainer.SaveData();
        SceneFader.LoadScene("CreateCharacter", FadeType.Loding);
    }

    public void Exit()
    {
        OffPopup();
        Core.DataContainer.SaveData();
        Application.Quit();
    }

    public void NameInputEvent(string text)
    {
        DataContainer.Datas.info[curPanel].playerName = text;
    }

    public void OffPopup()
    {
        if(DataContainer.Datas.info[curPanel].playerName == "")
            DataContainer.Datas.info[curPanel].playerName = $"Geust{Random.Range(1, 1000)}";
        this.gameObject.SetActive(false);
    }
}
