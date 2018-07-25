using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataController : MonoBehaviour
{
    private string levelsFile = "/Data/Levels.json";

    [SerializeField] private TextAsset levelsText;

    private Levels levels;

    // Use this for initialization
    void Awake()
    {
        ReadData();
    }

    public void ReadData()
    {
        string levelsContent = levelsText.text;
        levels = JsonUtility.FromJson<Levels>(levelsContent);
    }

    public Level[] GetLevels()
    {
        return levels.levels;
    }
}