using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public void LoadScene(string level)
    {
        SceneManager.LoadScene(level);
    }
    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }
    public void RestartScene()
    {
        int currentScene = GetCurrentSceneIndex();
        LoadScene(currentScene);
    }
    public int GetCurrentSceneIndex()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }
    public void LoadGame(int levelIndex)
    {
        GameObject.FindObjectOfType<DataController>().levelIndex = levelIndex;
        LoadScene("Game");
    }
    public void LoadNextLevel()
    {
        DataController dataController = GameObject.FindObjectOfType<DataController>();
        int currentLevelIndex = dataController.levelIndex;
        Level[] levels = dataController.GetLevels();

        if (currentLevelIndex + 1 >= levels.Length)
            return;
        else if (levels[currentLevelIndex + 1].locked)
            return;
        else
            LoadGame(currentLevelIndex + 1);
    }
}
