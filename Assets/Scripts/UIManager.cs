﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject TimerText;

    public void UpdateTimer(string value)
    {
        TimerText.GetComponent<Text>().text = value;
    }
	
}
