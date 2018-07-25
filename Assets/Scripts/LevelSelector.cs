using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelector : MonoBehaviour
{
    public GameObject Worlds;
    
    public void NextWorld()
    {
        Worlds.GetComponent<RectTransform>().anchoredPosition = new Vector2(Worlds.GetComponent<RectTransform>().anchoredPosition.x-550f, Worlds.GetComponent<RectTransform>().anchoredPosition.y);
    }
    public void PreviewWorld()
    {
        Worlds.GetComponent<RectTransform>().anchoredPosition = new Vector2(Worlds.GetComponent<RectTransform>().anchoredPosition.x + 550f, Worlds.GetComponent<RectTransform>().anchoredPosition.y);
    }
}
