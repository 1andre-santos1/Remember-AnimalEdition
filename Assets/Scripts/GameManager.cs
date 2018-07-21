using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float Timer = 10f;
    public bool isGameActive = true;

    private UIManager uimanager;

    private void Start()
    {
        uimanager = GameObject.FindObjectOfType<UIManager>();
    }
    public void Update()
    {
        if (!isGameActive)
            return;

        Timer -= Time.deltaTime;
        if(Timer <= 0f)
        {
            Debug.Log("Lost Game");
            isGameActive = false;
            return;
        }
        uimanager.UpdateTimer(Mathf.FloorToInt(Timer));
    }

}
