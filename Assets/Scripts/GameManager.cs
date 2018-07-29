using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int Tries = 0;
    public float Bar_AutoTimeToDecrease = 0.2f;

    public int NumberOfCards;
    public float Bar_AutoAmountToDecrease;
    public float Bar_MatchedCardIncrement;
    public float Bar_FailedMatchDecrement;
    public int Probability_CardsWithSameColor;
    public float InitialDelay;

    public bool isGameActive = false;

    private UIManager uimanager;
    private DataController dataController;
    private LevelManager levelManager;

    private void Start()
    {
        uimanager = GameObject.FindObjectOfType<UIManager>();
        dataController = GameObject.FindObjectOfType<DataController>();
        levelManager = GameObject.FindObjectOfType<LevelManager>();

        Level level = dataController.GetLevels()[levelManager.GetCurrentSceneIndex() - 1];

        NumberOfCards = level.numberOfCards;
        Bar_AutoAmountToDecrease = level.bar_AutoAmountToDecrease;
        Bar_MatchedCardIncrement = level.bar_MatchedCardIncrement;
        Bar_FailedMatchDecrement = level.bar_FailedMatchDecrement;
        Probability_CardsWithSameColor = level.probability_CardsWithSameColor;
        InitialDelay = level.initialDelay;
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
    }
    public void WinGame()
    {
        dataController.LoadData();

        uimanager.UpdatePanelWinTriesText(""+Tries);
        int numberOfStars = uimanager.GetStarsBasedOnBar();
        uimanager.UpdateStars(numberOfStars);

        //get stars number for this level
        int previousLevelStars = dataController.GetLevels()[levelManager.GetCurrentSceneIndex() - 1].stars;
        int starsDifference = numberOfStars - previousLevelStars;

        GameObject.FindObjectOfType<SoundManager>().PlayWinSound();

        uimanager.ShowPanelWinMenu();

        if (previousLevelStars >= numberOfStars)
            return;

        //escreve estrelas para nivel
        dataController.GetLevels()[levelManager.GetCurrentSceneIndex() - 1].stars += starsDifference;

        //escreve estrelas para player
        dataController.IncrementPlayerStars(starsDifference);

        dataController.WriteData();
    }
}
