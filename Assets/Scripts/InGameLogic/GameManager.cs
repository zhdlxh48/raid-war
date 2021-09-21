using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        Ready,
        Intro,
        Play,
        Victory,
        Defeat
    };

    private static GameManager _instance = null;

    public static GameManager Instance
    {
        get
        {
            if (_instance != null)
                return _instance;
            _instance = FindObjectOfType<GameManager>();

            return _instance;
        }
    }

    [Tooltip("초 단위 입력")]
    public float miniute;
    public float second;

    public Camera subCam;
    public AudioClip bgm;
    public static float timeRemaining;

    public static Boss.GolemBehavior boss;
    public static PlayerFSMManager player;

    private static float originTime;
    private static GameState state;
    private static InGameUIManager inGameUI;

    public static GameState CurState { get { return state; } }

    void Awake()
    {
        if (_instance == null)
            _instance = this;

        timeRemaining = (miniute * 60) + (second);
        originTime = timeRemaining;
        state = GameState.Ready;
        boss = FindObjectOfType<Boss.GolemBehavior>();
        Debug.Log(boss);
        player = FindObjectOfType<PlayerFSMManager>();
        inGameUI = FindObjectOfType<InGameUIManager>();


        subCam.enabled = false;
    }

    private void Start()
    {
        Core.SoundManager.MuteBGM = true;
        inGameUI.RefreshUITick();
    }

    // Update is called once per frame
    void Update()
    {
        switch(state)
        {
            case GameState.Intro:
                GameIntro();
                break;
            case GameState.Play:
                GamePlay();
                break;
            case GameState.Victory:
                Victory();
                break;
            case GameState.Defeat:
                Defeat();
                break;
        }
    }

    public static void OnGameRedy()
    {
        Instance.subCam.enabled = true;
        Instance.StartCoroutine(Instance.InroCut());
    }

    public IEnumerator InroCut()
    {
        boss.BossStart();
        player.awaking = false;
        yield return new WaitForSeconds(1.4f);
        Instance.subCam.GetComponent<FollowCamera>().shake();
        yield return new WaitForSeconds(0.6f);
        Core.SoundManager.MuteBGM = false;
        Core.SoundManager.SetBGM(bgm);
        Core.SoundManager.BgmVolume = 0.3f;
        yield return new WaitForSeconds(0.8f);
        Instance.subCam.enabled = false;
        state = GameState.Intro;
    }

    void GameIntro()
    {
        player.awaking = true;
        state = GameState.Play;
    }

    void GamePlay()
    {
        timeRemaining -= Time.deltaTime;
        inGameUI.RefreshUITick();

        if (player.OnDead())
        {
            inGameUI.faildPopup.OnFaildPoup(FaildType.PlayerDie);
            state = GameState.Defeat;
        }

        else if (timeRemaining <= 0.0f)
        {
            inGameUI.faildPopup.OnFaildPoup(FaildType.TimeLimit);
            state = GameState.Defeat;
        }

        else if (boss.OnDead())
        {
            state = GameState.Victory;
            inGameUI.clearPopup.OnPopup(originTime - timeRemaining);
        }
    }

    void Victory()
    {
        Instance.subCam.enabled = true;
    }

    void Defeat()
    {
        boss.BossEnd();
        player.awaking = false;
        Instance.subCam.GetComponent<FollowCamera>().followTarget = player.transf;
        Instance.subCam.enabled = true;
    }
}
