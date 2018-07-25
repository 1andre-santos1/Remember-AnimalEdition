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

    public Level[] GetLevels()
    {
        return levels.levels;
    }
    public Player GetPlayer()
    {
        return player;
    }
}