using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;
    public string CurrentName { get; set; }
    public PlayerData PlayerData { get; private set; } = new PlayerData();
    public Leaderboard Leaderboard { get; private set; } = new Leaderboard();

    public int MaxCount { get; } = 5;

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
        _path = Application.persistentDataPath + "/table.json";
        LoadPlayerData();
    }
    private void LoadPlayerData()
    {
        if (File.Exists(_path))
        {
            string json = File.ReadAllText(_path);

            Leaderboard = JsonUtility.FromJson<Leaderboard>(json);
            PlayerData = Leaderboard.PlayerDatas[0];
        }
        else
            Leaderboard.PlayerDatas.Add(new PlayerData());

    }

    public void SaveScore(int value)
    {
        if (Leaderboard.PlayerDatas.Count > 0)
        {

            if (Leaderboard.PlayerDatas.Count < MaxCount)
            {
                int count = MaxCount - Leaderboard.PlayerDatas.Count;
                for (int i = 0; i < count; i++)
                {
                    Leaderboard.PlayerDatas.Add(new PlayerData());
                }
            }

            Leaderboard.PlayerDatas[MaxCount - 1].Score = value;
            Leaderboard.PlayerDatas[MaxCount - 1].Name = CurrentName;

            // sorted table

            for (int i = Leaderboard.PlayerDatas.Count - 1; i > 0; i--)
            {
                if (Leaderboard.PlayerDatas[i].Score < Leaderboard.PlayerDatas[i - 1].Score)
                    break;

                PlayerData temp = Leaderboard.PlayerDatas[i - 1];
                Leaderboard.PlayerDatas[i - 1] = Leaderboard.PlayerDatas[i];
                Leaderboard.PlayerDatas[i] = temp;
            }
        }
        else
        {
            PlayerData playerData = new PlayerData
            {
                Name = CurrentName,
                Score = value
            };

            Leaderboard.PlayerDatas.Add(playerData);
        }


        string json = JsonUtility.ToJson(Leaderboard);
        File.WriteAllText(_path, json);
    }

}

[System.Serializable]
public class PlayerData
{
    public string Name;
    public int Score;
}

[System.Serializable]
public class Leaderboard
{
    public List<PlayerData> PlayerDatas = new List<PlayerData>();
}