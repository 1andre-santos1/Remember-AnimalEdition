using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataController : MonoBehaviour
{
    private string levelsFile = "/Data/Levels.json";
    private string playerFile = "/Data/Player.json";

    [SerializeField] private TextAsset levelsText;
    [SerializeField] private TextAsset playerText;

    private Levels levels;
    private Player player;

    // Use this for initialization
    void Awake()
    {
        ReadData();
    }

    public void ReadData()
    {
        string levelsContent = levelsText.text;
        levels = JsonUtility.FromJson<Levels>(levelsContent);

        string playerContent = playerText.text;
        player = JsonUtility.FromJson<Player>(playerContent);
    }
    public void WriteData()
    {
        //WriteLevelsData();
        WritePlayerData();
    }
    public void WriteLevelsData()
    {
        string str = "{\n\"levels\":[\n";

        for (int i = 0; i < levels.levels.Length - 1; i++)
        {
            str += JsonUtility.ToJson(levels.levels[i], true);
            str += ",\n";
        }

        str += JsonUtility.ToJson(levels.levels[levels.levels.Length - 1], true);
        str += "\n]\n}";

        string filePath = Application.dataPath + "/Data/Levels.json";
        File.WriteAllText(filePath, str);
    }
    public void WritePlayerData()
    {
        string str = JsonUtility.ToJson(player,true);

        string filePath = Application.dataPath + playerFile;
        File.WriteAllText(filePath, str);
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