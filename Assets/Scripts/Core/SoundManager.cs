using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Core
{
    public class SoundManager : MonoBehaviour
    {
        public AudioSource bgmSource;
        public AudioSource effectSource;
        private static SoundManager instance = null;

        public static SoundManager Instance => instance;
        // Start is called before the first frame update

        public static bool MuteBGM
        {
            get { return Instance.bgmSource.mute; }
            set { Instance.bgmSource.mute = value; }
        }

        public static bool MuteEffect
        {
            get { return Instance.effectSource.mute; }
            set { Instance.effectSource.mute = value; }
        }

        public static float BgmVolume
        {
            get { return Instance.bgmSource.volume; }
            set { Instance.bgmSource.volume = value; }
        }

        public static float EffectVolume
        {
            get { return Instance.effectSource.volume; }
            set { Instance.effectSource.volume = value; }
        }

        void Awake()
        {
            if (instance == null)
                instance = this;

            else if (instance != this)
                Destroy(gameObject);

            DontDestroyOnLoad(gameObject);
        }

        public static void OneShot(AudioClip[] audioClip)// 한번만 재생, 렌덤
        {
            int r = Random.Range(0, audioClip.Length - 1);
            Instance.effectSource.PlayOneShot(audioClip[r]);
        }

        public static void OneShot(AudioClip audioClip)// 한번만 재생
        {
            Instance.effectSource.PlayOneShot(audioClip);
        }

        public static void OneShotVib(AudioClip audioClip)// 한번만 재생 + 약 1초 진동
        {
            Instance.effectSource.PlayOneShot(audioClip);
            //Handheld.Vibrate();
        }

        public static void OneVibrater()//한번진동
        {
           // Handheld.Vibrate();
        }

        public static void SetBGM(AudioClip audioClip)// 루프 재생
        {
            if (Instance.bgmSource.clip == audioClip)
                return;
            Instance.bgmSource.clip = audioClip;
            Instance.bgmSource.loop = true;
            Instance.bgmSource.Play();
        }
    }
}
