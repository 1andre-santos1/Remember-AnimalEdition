using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public float TimeBarDecreasing = 1f;
    public float AmountBarDecreasing = 10f;
    public float AmountIncrementBar = 100f;
    public float AmountDecrementBar = 50f;
    public GameObject PanelLoseMenu;
    public GameObject TriesText;
    public GameObject StarsBar;

    public GameObject PanelWinMenu;
    public GameObject PanelWin_TriesText;
    public GameObject[] PanelWin_Stars;
    public Sprite DarkStar;
    public Sprite Star;

    public void StartBarDecreasing()
    {
        StartCoroutine("DecreaseBar");
    }

    public void Update()
    {
        if (!GameObject.FindObjectOfType<GameManager>().isGameActive)
            return;
        if (StarsBar.GetComponent<RectTransform>().offsetMin.x < 47f)
            GameObject.FindObjectOfType<GameManager>().LoseGame();
    }
    IEnumerator DecreaseBar()
    {
        if (GameObject.FindObjectOfType<GameManager>().isGameActive)
        {
            yield return new WaitForSeconds(TimeBarDecreasing);
            StarsBar.GetComponent<RectTransform>().offsetMin = new Vector2(StarsBar.GetComponent<RectTransform>().offsetMin.x - AmountBarDecreasing, StarsBar.GetComponent<RectTransform>().offsetMin.y);
            Debug.Log(GetStarsBasedOnBar());

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
        if (!GameObject.FindObjectOfType<GameManager>().isGameActive)
            return;
        StarsBar.GetComponent<RectTransform>().offsetMin = new Vector2(StarsBar.GetComponent<RectTransform>().offsetMin.x - AmountDecrementBar, StarsBar.GetComponent<RectTransform>().offsetMin.y);
        Debug.Log(GetStarsBasedOnBar());
    }
    public void IncrementBar()
    {
        if (!GameObject.FindObjectOfType<GameManager>().isGameActive)
            return;
        StarsBar.GetComponent<RectTransform>().offsetMin = new Vector2(StarsBar.GetComponent<RectTransform>().offsetMin.x + AmountIncrementBar, StarsBar.GetComponent<RectTransform>().offsetMin.y);
        Debug.Log(GetStarsBasedOnBar());

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
