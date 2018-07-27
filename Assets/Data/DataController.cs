using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataController : MonoBehaviour
{
    private TextAsset levelsText;
    private TextAsset playerText;

    private Levels levels;
    private Player player;

    public static DataController i;

    // Use this for initialization
    void Awake()
    {
        if (!i)
        {
            i = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        Debug.Log("Load json data from files");
        levels = JsonUtility.FromJson<Levels>((Resources.Load("Levels") as TextAsset).text);
        player = JsonUtility.FromJson<Player>((Resources.Load("Player") as TextAsset).text);

        WriteData();
    }

    public void LoadData()
    {
        Debug.Log("Load Data from Player Prefs");

        string playerData = PlayerPrefs.GetString("Player");
        player = JsonUtility.FromJson<Player>(playerData);

        string levelsData = PlayerPrefs.GetString("Levels");
        levels = JsonUtility.FromJson<Levels>(levelsData);
    }

    public void WriteData()
    {
        Debug.Log("Write Data to Player Prefs");

        WriteLevelsData();
        WritePlayerData();
    }
    public void WriteLevelsData()
    {
        string jsonData = JsonUtility.ToJson(levels);
        PlayerPrefs.SetString("Levels", jsonData);
        PlayerPrefs.Save();
    }
    public void WritePlayerData()
    {
        string jsonData = JsonUtility.ToJson(player);
        PlayerPrefs.SetString("Player", jsonData);
        PlayerPrefs.Save();
    }

    public Level[] GetLevels()
    {
        return levels.levels;
    }
    public Player GetPlayer()
    {
        return player;
    }

    public void IncrementPlayerStars(int value)
    {
        player.numberOfStars += value;
    }
}