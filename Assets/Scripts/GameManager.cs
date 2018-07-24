using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int TimeLimit = 20;
    public float Timer = 0;
    public int Tries = 0;
    public int IncrementOnTimeLimit = 5;
    public int IncrementOnTimer = 5;
    public int NumberOfCards = 6;

    public bool isGameActive = false;

    private UIManager uimanager;

    private void Start()
    {
        uimanager = GameObject.FindObjectOfType<UIManager>();
        uimanager.UpdateTries("" + Tries);
    }
    public void Update()
    {
        if (!isGameActive)
            return;

        Timer += Time.deltaTime;
        uimanager.UpdateTimer(Mathf.FloorToInt(Timer) + "/" + TimeLimit);
        if (Timer >= TimeLimit)
        {
            Debug.Log("Lost Game");
            isGameActive = false;
            uimanager.ShowPanelLoseMenu();
            GameObject.FindObjectOfType<SoundManager>().PlayLoseSound();
            return;
        }
    }

    public void IncrementTimeLimit()
    {
        TimeLimit += IncrementOnTimeLimit;
    }
    public void DecreaseTimeLeft()
    {
        Timer += IncrementOnTimer;
        if(Timer >= TimeLimit)
            Timer = TimeLimit;
    }
    public void IncreaseTries()
    {
        Tries++;
        uimanager.UpdateTries("" + Tries);
    }
    public void WinGame()
    {
        uimanager.UpdatePanelWinTriesText(""+Tries);
        int numberOfStars;
        if (Tries <= NumberOfCards / 2)
            numberOfStars = 3;
        else if (Tries <= NumberOfCards)
            numberOfStars = 2;
        else
            numberOfStars = 1;
        uimanager.UpdateStars(numberOfStars);
        uimanager.ShowPanelWinMenu();
        GameObject.FindObjectOfType<SoundManager>().PlayWinSound();
    }
}
