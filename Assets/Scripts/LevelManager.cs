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
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        LoadScene(currentScene);
    }
}
