using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int TimeLimit = 20;
    public float Timer = 0;
    public int IncrementOnTimeLimit = 5;

    public bool isGameActive = false;

    private UIManager uimanager;

    private void Start()
    {
        uimanager = GameObject.FindObjectOfType<UIManager>();
    }
    public void Update()
    {
        if (!isGameActive)
            return;

        Timer += Time.deltaTime;
        if(Timer >= TimeLimit)
        {
            Debug.Log("Lost Game");
            isGameActive = false;
            return;
        }
        uimanager.UpdateTimer(Mathf.FloorToInt(Timer)+"/"+TimeLimit);
    }

    public void IncrementTimeLimit()
    {
        TimeLimit += IncrementOnTimeLimit;
    }

}
