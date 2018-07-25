using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject PanelLoseMenu;
    public GameObject TriesText;
    public GameObject StarsBar;

    public GameObject PanelWinMenu;
    public GameObject PanelWin_TriesText;
    public GameObject[] PanelWin_Stars;
    public Sprite DarkStar;
    public Sprite Star;

    private float TimeBarDecreasing;
    private float AmountBarDecreasing;
    private float AmountIncrementBar;
    private float AmountDecrementBar;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();

        TimeBarDecreasing = gameManager.Bar_AutoTimeToDecrease;
        AmountBarDecreasing = gameManager.Bar_AutoAmountToDecrease;
        AmountIncrementBar = gameManager.Bar_MatchedCardIncrement;
        AmountDecrementBar = gameManager.Bar_FailedMatchDecrement;
    }

    public void StartBarDecreasing()
    {
        StartCoroutine("DecreaseBar");
    }

    public void Update()
    {
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
    }
    public void UpdateTries(string value)
    {
        TriesText.GetComponent<Text>().text = value;
    }
    public void UpdatePanelWinTriesText(string value)
    {
        PanelWin_TriesText.GetComponent<Text>().text = value;
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
        else if (StarsBar.GetComponent<RectTransform>().offsetMin.x > 283f)
            return 2;
        else
            return 1;
    }
}
