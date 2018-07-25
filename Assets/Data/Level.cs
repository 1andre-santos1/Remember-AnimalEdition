using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Level
{
    public string host = "";
    public int index = 0;
    public int numberOfCards = 0;
    public int bar_AutoTimeToDecrease = 0;
    public int bar_AutoAmountToDecrease = 0;
    public int bar_MatchedCardIncrement = 0;
    public int bar_FailedMatchDecrement = 0;
    public int probability_CardsWithSameColor = 0;
}