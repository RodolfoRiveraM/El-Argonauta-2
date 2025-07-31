using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;
using System.Xml;
using System.Xml.Serialization;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;

    public SaveData activeSave;

    private SaveData defaultInfo;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            defaultInfo = activeSave;

            LoadInfo();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(Application.persistentDataPath);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadInfo()
    {
        string dataPath = Application.persistentDataPath;

        if (File.Exists(dataPath + "/save.data"))
        {
            var serializer = new XmlSerializer(typeof(SaveData));
            var stream = new FileStream(dataPath + "/save.data", FileMode.Open);
            activeSave = serializer.Deserialize(stream) as SaveData;
            stream.Close();

            Debug.Log("data loaded");
        }
    }

    public void SaveInfo()
    {
        string dataPath = Application.persistentDataPath;

        var serializer = new XmlSerializer(typeof(SaveData));
        var stream = new FileStream(dataPath + "/save.data", FileMode.Create);
        serializer.Serialize(stream, activeSave);
        stream.Close();

        Debug.Log("Data saved");
    }

    private void OnApplicationQuit()
    {
        SaveInfo();
    }

    public void ResetSave()
    {
        activeSave = defaultInfo;
    }

    public void MarkProgress(string progressToMark)
    {
        bool found = false;

        for(int i = 0; i < activeSave.progress.Count; i++)
        {
            if (activeSave.progress[i].name == progressToMark)
            {
                found = true;

                activeSave.progress[i].isMarked = true;

                i = activeSave.progress.Count;
            }
        }

        if (!found)
        {
            Debug.LogError("no se que dice" + progressToMark);
        }
    }

    public bool CheckProgress(string progressToCheck)
    {
        bool isMarked = false;
        for(int i = 0; i < activeSave.progress.Count; i++)
        {
            if (activeSave.progress[i].name == progressToCheck)
            {
                isMarked = activeSave.progress[i].isMarked;
                i = activeSave.progress.Count;
            }
        }
        return isMarked;
    }
}

[System.Serializable]
public class SaveData
{
    public bool hasBegun;
    public Vector3 sceneStartPosition;
    public string currentScene;
    public int maxHealth, currentSword, swordDamage, currentCoins;
    public float maxStamina;

    public List<ProgressItem> progress = new List<ProgressItem>();
}

[System.Serializable]
public class ProgressItem
{
    public string name;
    public bool isMarked;
}
