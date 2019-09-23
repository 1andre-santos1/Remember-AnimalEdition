﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject PanelLoseMenu;
    public GameObject StarsBar;

    public GameObject DownBanner;
    public GameObject HostTalk;
    public GameObject HostSprite;
    public GameObject LevelText;
    public GameObject LevelStarsContainer;
    private string[] HostTalkStrings = new string[] {"Good job friend!","That was very good!","Nice job!","Keep Going","Nice","Very cool!","You're learning!"};

    public GameObject PanelWinMenu;
    public GameObject NewRecordText;
    public GameObject[] PanelWin_Stars;
    public Sprite DarkStar;
    public Sprite Star;

    public GameObject PanelWinGame;
    public GameObject PauseMenu;
    public GameObject SettingsMenu;

    public GameObject LoadingScreenLevelNumber;
    public GameObject LoadingScreenHostSprite;
    public GameObject LoadingScreenNumberOfCards;
    public GameObject LoadingScreenSpeedStars;
    public GameObject LoadingScreenMemorizationStars;
    public GameObject LoadingScreenDifficultyStars;

    private float TimeBarDecreasing;
    private float AmountBarDecreasing;
    private float AmountIncrementBar;
    private float AmountDecrementBar;
    private GameManager gameManager;
    private bool isActive = false;

    private void Start()
    {
        LoadingScreenLevelNumber.GetComponent<Text>().text = "Level " + ((GameObject.FindObjectOfType<DataController>().levelIndex % 9) + 1);

        Level[] levels = GameObject.FindObjectOfType<DataController>().GetLevels();
        Level currentLevel = levels[GameObject.FindObjectOfType<DataController>().levelIndex];
        if (currentLevel.host == "monkey")
            LoadingScreenHostSprite.GetComponent<Image>().sprite = GameObject.FindObjectOfType<GameManager>().HostsSprite[0];
        else if (currentLevel.host == "cow")
            LoadingScreenHostSprite.GetComponent<Image>().sprite = GameObject.FindObjectOfType<GameManager>().HostsSprite[1];
        else if (currentLevel.host == "panda")
            LoadingScreenHostSprite.GetComponent<Image>().sprite = GameObject.FindObjectOfType<GameManager>().HostsSprite[2];
        else if (currentLevel.host == "pig")
            LoadingScreenHostSprite.GetComponent<Image>().sprite = GameObject.FindObjectOfType<GameManager>().HostsSprite[3];

        LoadingScreenNumberOfCards.GetComponent<Text>().text = "" + currentLevel.numberOfCards;

        int maxSpeed = levels[levels.Length-1].bar_AutoAmountToDecrease;
        int numSpeedStars = Mathf.CeilToInt((currentLevel.bar_AutoAmountToDecrease*3)/maxSpeed);
        for(int i = 0; i < LoadingScreenSpeedStars.transform.childCount; i++)
        {
            if(i >= numSpeedStars)
                LoadingScreenSpeedStars.transform.GetChild(i).GetComponent<Image>().sprite = DarkStar;
            else
                LoadingScreenSpeedStars.transform.GetChild(i).GetComponent<Image>().sprite = Star;
        }

        int maxProbabilitySameColor = 100;
        int numMemorizationStars = Mathf.CeilToInt((currentLevel.probability_CardsWithSameColor*3)/maxProbabilitySameColor);
        for(int i = 0; i < LoadingScreenMemorizationStars.transform.childCount; i++)
        {
            if(i >= numMemorizationStars)
                LoadingScreenMemorizationStars.transform.GetChild(i).GetComponent<Image>().sprite = DarkStar;
            else
                LoadingScreenMemorizationStars.transform.GetChild(i).GetComponent<Image>().sprite = Star;
        }

        int maxCards = levels[levels.Length-1].numberOfCards;
        int currentLevelDifficultySum = currentLevel.numberOfCards + numSpeedStars + numMemorizationStars;
        int maxDifficultySum = maxCards + maxSpeed + maxProbabilitySameColor;
        int numDifficultyStars = Mathf.CeilToInt((currentLevelDifficultySum*3)/maxDifficultySum);
        for(int i = 0; i < LoadingScreenDifficultyStars.transform.childCount; i++)
        {
            if(i >= numDifficultyStars)
                LoadingScreenDifficultyStars.transform.GetChild(i).GetComponent<Image>().sprite = DarkStar;
            else
                LoadingScreenDifficultyStars.transform.GetChild(i).GetComponent<Image>().sprite = Star;
        }
    }

    public void ShowRecord()
    {
        NewRecordText.SetActive(true);
    }

    public void StartGame()
    {
        isActive = true;
        gameManager = GameObject.FindObjectOfType<GameManager>();

        TimeBarDecreasing = gameManager.Bar_AutoTimeToDecrease;
        AmountBarDecreasing = gameManager.Bar_AutoAmountToDecrease;
        AmountIncrementBar = gameManager.Bar_MatchedCardIncrement;
        AmountDecrementBar = gameManager.Bar_FailedMatchDecrement;

        HostTalk.GetComponent<Text>().text = "";
        LevelText.GetComponent<Text>().text = "Level "+ ((GameObject.FindObjectOfType<DataController>().levelIndex % 9) + 1);

        DataController dataController = GameObject.FindObjectOfType<DataController>();
        Level currentLevel = dataController.GetLevels()[dataController.levelIndex];
        for (int i = 0; i < currentLevel.stars; i++)
            LevelStarsContainer.transform.GetChild(i).GetComponent<Image>().sprite = Star;

        if (currentLevel.host == "monkey")
            HostSprite.GetComponent<Image>().sprite = gameManager.HostsSprite[0];
        else if (currentLevel.host == "cow")
            HostSprite.GetComponent<Image>().sprite = gameManager.HostsSprite[1];
        else if (currentLevel.host == "panda")
            HostSprite.GetComponent<Image>().sprite = gameManager.HostsSprite[2];
        else if (currentLevel.host == "pig")
            HostSprite.GetComponent<Image>().sprite = gameManager.HostsSprite[3];
        else if (currentLevel.host == "rabbit")
            HostSprite.GetComponent<Image>().sprite = gameManager.HostsSprite[4];
    }

    public void BeginInitialDelayCount(float delay)
    {
        StartCoroutine("CountEvent",delay);
    }

    IEnumerator CountEvent(float delay)
    {
        Text t = HostTalk.GetComponent<Text>();
        while (delay > 0f)
        {
            t.text = "Be prepared in " + delay;
            yield return new WaitForSeconds(1f);
            delay--;
        }
        t.text = "Good Luck!";
        yield return new WaitForSeconds(2f);
        t.text = "";
    }

    public void MakeHostTalk()
    {
        Text t = HostTalk.GetComponent<Text>();
        string talk = HostTalkStrings[Random.Range(0, HostTalkStrings.Length)];

        t.text = talk;

        AnimateHostTalk();
    }

    public void AnimateHostTalk()
    {
        GameObject.Find("BackgroundHost").GetComponent<Animator>().enabled = true;
        GameObject.Find("BackgroundHost").GetComponent<Animator>().Play("Talking");
    }

    public void StartBarDecreasing()
    {
        StartCoroutine("DecreaseBar");
    }

    public void Update()
    {
        if (!isActive)
            return;
        if (!gameManager.isGameActive)
            return;
        if (StarsBar.GetComponent<RectTransform>().offsetMin.x < 47f)
            gameManager.LoseGame();
    }
    IEnumerator DecreaseBar()
    {
        if (gameManager.isGameActive)
        {
            yield return new WaitForSeconds(TimeBarDecreasing);
            StarsBar.GetComponent<RectTransform>().offsetMin = new Vector2(StarsBar.GetComponent<RectTransform>().offsetMin.x - AmountBarDecreasing, StarsBar.GetComponent<RectTransform>().offsetMin.y);

            StartCoroutine("DecreaseBar");
        }
    }

    public void ShowPanelLoseMenu()
    {
        PanelLoseMenu.SetActive(true);
    }
    public void ShowPanelWinMenu()
    {
        PanelWinMenu.SetActive(true);
    }
    public void DecrementBar()
    {
        if (!gameManager.isGameActive)
            return;

        StarsBar.GetComponent<RectTransform>().offsetMin = new Vector2(StarsBar.GetComponent<RectTransform>().offsetMin.x - AmountDecrementBar, StarsBar.GetComponent<RectTransform>().offsetMin.y);
    }
    public void IncrementBar()
    {
        if (!gameManager.isGameActive)
            return;

        StarsBar.GetComponent<RectTransform>().offsetMin = new Vector2(StarsBar.GetComponent<RectTransform>().offsetMin.x + AmountIncrementBar, StarsBar.GetComponent<RectTransform>().offsetMin.y);

        if (StarsBar.GetComponent<RectTransform>().offsetMin.x >= 598f)
            StarsBar.GetComponent<RectTransform>().offsetMin = new Vector2(598f, StarsBar.GetComponent<RectTransform>().offsetMin.y); ;
    }

    public void UpdateStars(int numberOfStars)
    {
        switch (numberOfStars)
        {
            case 1:
                PanelWin_Stars[0].GetComponent<Image>().sprite = Star;
                PanelWin_Stars[1].GetComponent<Image>().sprite = DarkStar;
                PanelWin_Stars[2].GetComponent<Image>().sprite = DarkStar;
                break;
            case 2:
                PanelWin_Stars[0].GetComponent<Image>().sprite = Star;
                PanelWin_Stars[1].GetComponent<Image>().sprite = Star;
                PanelWin_Stars[2].GetComponent<Image>().sprite = DarkStar;
                break;
            case 3:
                PanelWin_Stars[0].GetComponent<Image>().sprite = Star;
                PanelWin_Stars[1].GetComponent<Image>().sprite = Star;
                PanelWin_Stars[2].GetComponent<Image>().sprite = Star;
                break;
        }
    }

    public int GetStarsBasedOnBar()
    {
        if (StarsBar.GetComponent<RectTransform>().offsetMin.x > 453f)
            return 3;
        else if (StarsBar.GetComponent<RectTransform>().offsetMin.x > 280f)
            return 2;
        else
            return 1;
    }

    public void ShowPanelWinGame()
    {
        PanelWinGame.SetActive(true);
    }
    public void HidePanelWinGame()
    {
        PanelWinGame.SetActive(false);
    }
    public void LoadMainMenu()
    {
        GameObject.FindObjectOfType<LevelManager>().LoadScene(0);
    }
    public void RestartLevel()
    {
        GameObject.FindObjectOfType<LevelManager>().RestartScene();
    }
    public void LoadNextLevel()
    {
        GameObject.FindObjectOfType<LevelManager>().LoadNextLevel();
    }
    public void ShowPauseMenu()
    {
        PauseMenu.SetActive(true);
        DownBanner.SetActive(false);
    }
    public void HidePauseMenu()
    {
        PauseMenu.SetActive(false);
        DownBanner.SetActive(true);
    }
    public void ResumeGame()
    {
        HidePauseMenu();
        gameManager.isGameActive = true;
        StartBarDecreasing();
    }
    public void ShowSettingsMenu()
    {
        SettingsMenu.SetActive(true);
    }
    public void HideSettingsMenu()
    {
        SettingsMenu.SetActive(false);
    }
}
