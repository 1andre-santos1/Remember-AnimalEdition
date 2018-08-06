using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager i;

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
        Animator anim = GameObject.Find("Canvas").GetComponent<Animator>();
        anim.enabled = true;
        anim.Play("SplashScreen");
    }

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

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 0)
            GameObject.FindObjectOfType<MusicManager>().PlayMenuMusic();
        else
        {
            DataController dc = GameObject.FindObjectOfType<DataController>();
            Level l = dc.GetLevels()[dc.levelIndex];
            if (l.host == "monkey")
                GameObject.FindObjectOfType<MusicManager>().PlayWorldMusic(0);
            else if (l.host == "cow")
                GameObject.FindObjectOfType<MusicManager>().PlayWorldMusic(1);
            else if (l.host == "cow")
                GameObject.FindObjectOfType<MusicManager>().PlayWorldMusic(2);
        }
    }
}
