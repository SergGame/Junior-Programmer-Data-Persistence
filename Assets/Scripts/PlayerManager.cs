using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;
    public string CurrentName { get; set; }
    public PlayerData PlayerData { get; private set; } = new PlayerData();

    private string _path;


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        _path = Application.persistentDataPath + "/player.json";
        LoadPlayerData();
    }
    private void LoadPlayerData()
    {
        if (File.Exists(_path))
        {
            string json = File.ReadAllText(_path);
            PlayerData = JsonUtility.FromJson<PlayerData>(json);
        }
    }

    public void SaveScore(int value)
    {
        if(value > PlayerData.Score)
        {
            PlayerData.Score = value;
            PlayerData.Name = CurrentName;

            string json = JsonUtility.ToJson(PlayerData);

            File.WriteAllText(_path, json);
        }
    }

}

[System.Serializable]
public class PlayerData
{
    public string Name;
    public int Score;
}
