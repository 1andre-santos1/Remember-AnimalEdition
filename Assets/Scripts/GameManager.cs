using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int Tries = 0;
    public float Bar_AutoTimeToDecrease = 0.2f;

    public int NumberOfCards;
    public float Bar_AutoAmountToDecrease;
    public float Bar_MatchedCardIncrement;
    public float Bar_FailedMatchDecrement;
    public int Probability_CardsWithSameColor;
    public int InitialDelay;

    public bool isGameActive = false;

    public GameObject Canvas;
    public GameObject UnlockedLevel;
    public Sprite[] HostsSprite;
    public GameObject ParticleConfetti;

    public GameObject Background;
    public Sprite[] BackgroundImages;

    private UIManager uimanager;
    private DataController dataController;
    private LevelManager levelManager;

    public void Start()
    {
        dataController = GameObject.FindObjectOfType<DataController>();
        levelManager = GameObject.FindObjectOfType<LevelManager>();
        uimanager = GameObject.FindObjectOfType<UIManager>();
    }
    public void StartGame()
    {
        Level level = dataController.GetLevels()[dataController.levelIndex];

        NumberOfCards = level.numberOfCards;
        Bar_AutoAmountToDecrease = level.bar_AutoAmountToDecrease;
        Bar_MatchedCardIncrement = level.bar_MatchedCardIncrement;
        Bar_FailedMatchDecrement = level.bar_FailedMatchDecrement;
        Probability_CardsWithSameColor = level.probability_CardsWithSameColor;
        InitialDelay = level.initialDelay;

        if (level.host == "monkey")
            Background.GetComponent<SpriteRenderer>().sprite = BackgroundImages[0];
        else if(level.host == "cow")
            Background.GetComponent<SpriteRenderer>().sprite = BackgroundImages[1];
        else if(level.host == "panda")
            Background.GetComponent<SpriteRenderer>().sprite = BackgroundImages[2];

        uimanager.StartGame();
        GameObject.FindObjectOfType<CardsGrid>().StartGame();
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (uimanager == null)
                return;
            isGameActive = false;
            uimanager.ShowPauseMenu();
        }
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
        Instantiate(ParticleConfetti);

        dataController.LoadData();

        uimanager.UpdatePanelWinTriesText(""+Tries);
        int numberOfStars = uimanager.GetStarsBasedOnBar();
        uimanager.UpdateStars(numberOfStars);

        //get stars number for this level
        int previousLevelStars = dataController.GetLevels()[dataController.levelIndex].stars;
        int starsDifference = numberOfStars - previousLevelStars;

        if (previousLevelStars >= numberOfStars)
        {
            StartCoroutine("WinEvent");

            return;
        }

        //get all the levels previously unlocked
        List<Level> levelsPreviouslyUnlocked = new List<Level>();
        foreach (var l in dataController.GetLevels())
            if (!l.locked)
                levelsPreviouslyUnlocked.Add(l);

        //escreve estrelas para nivel
        dataController.GetLevels()[dataController.levelIndex].stars += starsDifference;

        //escreve estrelas para player
        dataController.IncrementPlayerStars(starsDifference);

        dataController.WriteData();

        //get all the new unlocked levels
        List<Level> LevelsUnlocked = new List<Level>();
        foreach (var l in dataController.GetLevels())
            if (!l.locked && !levelsPreviouslyUnlocked.Contains(l))
                LevelsUnlocked.Add(l);

        StartCoroutine("ShowUnlocked", LevelsUnlocked);
    }

    IEnumerator ShowUnlocked(List<Level> LevelsUnlocked)
    {
        GameObject.Find("Grid").SetActive(false);

        uimanager.ShowPanelWinGame();

        GameObject.FindObjectOfType<SoundManager>().PlayWinSound();

        yield return new WaitForSeconds(3f);

        uimanager.HidePanelWinGame();
        uimanager.HostTalk.transform.parent.transform.parent.gameObject.SetActive(false);

        foreach (var level in LevelsUnlocked)
        {
            GameObject.FindObjectOfType<SoundManager>().PlayUnlockSound();

            GameObject levelP = Instantiate(UnlockedLevel) as GameObject;
            levelP.transform.SetParent(Canvas.transform, false);

            GameObject LevelInfo = levelP.transform.Find("UnlockedLevel").gameObject;

            LevelInfo.transform.Find("Number").GetComponent<Text>().text = "" + ((level.index % 9) + 1);
            switch (level.host)
            {
                case "monkey":
                    LevelInfo.transform.Find("Host").GetComponent<Image>().sprite = HostsSprite[0];
                    break;
                case "cow":
                    LevelInfo.transform.Find("Host").GetComponent<Image>().sprite = HostsSprite[1];
                    break;
                case "panda":
                    LevelInfo.transform.Find("Host").GetComponent<Image>().sprite = HostsSprite[2];
                    break;
            }

            yield return new WaitForSeconds(5f);
            Destroy(levelP);
        }
        uimanager.ShowPanelWinMenu();
    }
    IEnumerator WinEvent()
    {
        GameObject.Find("Grid").SetActive(false);

        uimanager.ShowPanelWinGame();

        GameObject.FindObjectOfType<SoundManager>().PlayWinSound();

        yield return new WaitForSeconds(3f);

        uimanager.HidePanelWinGame();

        uimanager.HostTalk.transform.parent.transform.parent.gameObject.SetActive(false);

        uimanager.ShowPanelWinMenu();
    }
}
