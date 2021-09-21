//작성자 : 임자현   작성 시작일 : 2019-04-07
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;


public enum FadeType
{
    Loding,
    BlackOut,
    GameOver
};
public class SceneFader : MonoBehaviour
{
    [SerializeField]
    public Image LodingBar; // 로딩바 이미지

    public static Image progressBar;

    public CanvasGroup lodingCanvas;//로딩 캔버스
    public CanvasGroup gameOverCanvas;//게임 오버 캔버스
    public CanvasGroup blackOutCanvas;//검은색 캔버스

    protected static SceneFader _instance;
    private static bool isLoding = false;

    public static SceneFader Instance
    {
        get
        {
            if (_instance != null)
                return _instance;
            _instance = FindObjectOfType<SceneFader>();


            if (_instance != null)
                return _instance;

            CreateInfo();

            return _instance;
        }
    }

    public static void CreateInfo()
    {
        SceneFader sceneFaderPrefep = Resources.Load<SceneFader>("SceneFader");
        _instance = Instantiate(sceneFaderPrefep);
    }

    // Start is called before the first frame update
    void Awake()
    {
        if (_instance == null)
            _instance = this;
        else if (_instance != this)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        progressBar = LodingBar;
    }

    public static void LoadScene(string sceneName = "", FadeType fadeType = FadeType.BlackOut)
    {
        if (!isLoding)
        {
            isLoding = true;
            _instance.StartCoroutine(SceneFader.FadeOut(fadeType, sceneName));
        }
    }
    protected static IEnumerator Fade(float alpha, CanvasGroup canvasGroup) // 페이드 코루틴
    {
        float fadeSpeed = Mathf.Abs(canvasGroup.alpha - alpha) / 1.0f;
        while (!Mathf.Approximately(alpha, canvasGroup.alpha))
        {
            canvasGroup.alpha = Mathf.MoveTowards(canvasGroup.alpha, alpha, fadeSpeed * Time.deltaTime);
            yield return null;
        }
        canvasGroup.alpha = alpha;
    }

    public static IEnumerator FadeIn()
    {
        CanvasGroup tempGroup;
        if (Instance.blackOutCanvas.alpha > 0.1f)
            tempGroup = Instance.blackOutCanvas;
        else if (Instance.gameOverCanvas.alpha > 0.1f)
            tempGroup = Instance.gameOverCanvas;
        else
        {
            tempGroup = Instance.lodingCanvas;
        }
        yield return Instance.StartCoroutine(Fade(0.0f, tempGroup));
        tempGroup.gameObject.SetActive(false);
        isLoding = false;
    }

    public static IEnumerator FadeOut(FadeType fadeType, string SceneName = "")
    {
        CanvasGroup tempGroup;
        switch (fadeType)
        {
            case FadeType.BlackOut:
                tempGroup = Instance.blackOutCanvas;
                tempGroup.gameObject.SetActive(true);
                yield return Instance.StartCoroutine(Fade(1.0f, tempGroup));
                break;

            case FadeType.GameOver:
                tempGroup = Instance.gameOverCanvas;
                tempGroup.gameObject.SetActive(true);
                yield return Instance.StartCoroutine(Fade(1.0f, tempGroup));
                break;

            default:
                tempGroup = Instance.lodingCanvas;
                tempGroup.gameObject.SetActive(true);
                tempGroup.alpha = 1.0f;
                yield return Instance.StartCoroutine(LoadingSecen(SceneName));
                break;
        }
    }


    private static IEnumerator LoadingSecen(string sceneName)//비동기 씬전환
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);
        asyncOperation.allowSceneActivation = false;

        float timer = 0.0f;

        while (!asyncOperation.isDone)
        {
            yield return null;
            timer += Time.deltaTime;

            if (asyncOperation.progress >= 0.9f)
            {
                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, 1f, timer);
                if (progressBar.fillAmount == 1.0f)
                {
                    asyncOperation.allowSceneActivation = true;
                }
            }

            else
            {
                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, asyncOperation.progress, timer);
                if (progressBar.fillAmount >= asyncOperation.progress)
                    timer = 0;
            }
        }
        Instance.StartCoroutine(FadeIn());
        yield return null;
    }
}
