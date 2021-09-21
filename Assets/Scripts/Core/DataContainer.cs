using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Core
{
    [Serializable]
    public class ClearInfomation
    {
        public string playerName;
        public float clearTime;
        public ClearInfomation(string name, float time)
        {
            this.playerName = name;
            this.clearTime = time;
        }
        public void SetName(string text)
        {
            Debug.Log(text);
            playerName = text;
            Debug.Log(playerName);
        }
    }
    public class DataContainer : MonoBehaviour
    {
    
        [Serializable]
        public struct SaveDatas
        {
            public List<ClearInfomation> info;
        }
        private static SaveDatas datas;
        private static string filePath;

        public static SaveDatas Datas
        {
            get
            {
                if (datas.info != null)
                    return datas;

                LoadData();
                return datas;
            }
        }

        public static string FilePath { get { return filePath; } }

        protected static DataContainer _instance;

        public static DataContainer Instance
        {
            get
            {
                if (_instance != null)
                    return _instance;
                _instance = FindObjectOfType<DataContainer>();


                if (_instance != null)
                    return _instance;

                CreateInfo();

                return _instance;
            }
        }

        public static void CreateInfo()
        {
            DataContainer dataContainerPrefep = Resources.Load<DataContainer>("DataContainer");
            _instance = Instantiate(dataContainerPrefep);
        }


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
            datas.info = new List<ClearInfomation>();
        }

        private void Start()
        {
            Debug.Log(Application.persistentDataPath);
            filePath = Application.persistentDataPath + "/RaidWar.bin";
            if (File.Exists(filePath))
                LoadData();
        }

        public static void SortData()
        {
            if (datas.info.Count <= 1)
                return;

            datas.info.Sort(delegate (ClearInfomation A, ClearInfomation B)
            {
                if (A.clearTime > B.clearTime) return 1;
                else if (A.clearTime < B.clearTime) return -1;
                return 0;
            });
        }

        // 데이터 저장 메서드
        public static void SaveData()
        {
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                formatter.Serialize(stream, datas);
                stream.Close();
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
            }
        }

        // 데이터 로드 메서드
        public static void LoadData()
        {
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Read);
                SaveDatas temp = (SaveDatas)formatter.Deserialize(stream);
                stream.Close();
                datas = temp;
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
            }
        }
       
    }
}
