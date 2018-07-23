using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject TimerText;
    public GameObject PanelLoseMenu;
    public GameObject TriesText;

    public GameObject PanelWinMenu;
    public GameObject PanelWin_TriesText;
    public GameObject[] PanelWin_Stars;
    public Sprite DarkStar;
    public Sprite Star;

    public void UpdateTimer(string value)
    {
        TimerText.GetComponent<Text>().text = value;
    }
	public void ShowPanelLoseMenu()
    {
        PanelLoseMenu.SetActive(true);
    }
    public void ShowPanelWinMenu()
    {
        PanelWinMenu.SetActive(true);
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
        switch(numberOfStars)
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
}
