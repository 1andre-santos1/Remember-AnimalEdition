using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int Tries = 0;
    public int NumberOfCards = 6;

    public bool isGameActive = false;

    private UIManager uimanager;

    private void Start()
    {
        uimanager = GameObject.FindObjectOfType<UIManager>();
    }

    public void LoseGame()
    {
        Debug.Log("Lost Game");
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
