﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int Tries = 0;

    public int NumberOfCards;
    public float Bar_AutoTimeToDecrease;
    public float Bar_AutoAmountToDecrease;
    public float Bar_MatchedCardIncrement;
    public float Bar_FailedMatchDecrement;
    public int Probability_CardsWithSameColor;

    public bool isGameActive = false;

    private UIManager uimanager;
    private DataController dataController;
    private LevelManager levelManager;

    private void Awake()
    {
        dataController = GetComponent<DataController>();
        levelManager = GameObject.FindObjectOfType<LevelManager>();

        Level level = dataController.GetLevels()[levelManager.GetCurrentSceneIndex()-1];

        NumberOfCards = level.numberOfCards;
        Bar_AutoTimeToDecrease = level.bar_AutoTimeToDecrease;
        Bar_AutoAmountToDecrease = level.bar_AutoAmountToDecrease;
        Bar_MatchedCardIncrement = level.bar_MatchedCardIncrement;
        Bar_FailedMatchDecrement = level.bar_FailedMatchDecrement;
        Probability_CardsWithSameColor = level.probability_CardsWithSameColor;
    }

    private void Start()
    {
        uimanager = GameObject.FindObjectOfType<UIManager>();
    }

    public void LoseGame()
    {
        isGameActive = false;
        uimanager.ShowPanelLoseMenu();
        GameObject.FindObjectOfType<SoundManager>().PlayLoseSound();
        return;
    }

    public void IncrementTimeLimit()
    {
        uimanager.IncrementBar();
    }
    public void DecreaseTimeLeft()
    {
        uimanager.DecrementBar();
    }
    public void IncreaseTries()
    {
        Tries++;
        uimanager.UpdateTries("" + Tries);
    }
    public void WinGame()
    {
        uimanager.UpdatePanelWinTriesText(""+Tries);
        int numberOfStars = uimanager.GetStarsBasedOnBar();
        uimanager.UpdateStars(numberOfStars);
        uimanager.ShowPanelWinMenu();
        GameObject.FindObjectOfType<SoundManager>().PlayWinSound();
    }
}
