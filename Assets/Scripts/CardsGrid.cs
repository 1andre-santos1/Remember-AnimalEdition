using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsGrid : MonoBehaviour
{
    public int CardsTurned;

    private GameObject[] ChildCards;

    public void Start()
    {
        ChildCards = GameObject.FindGameObjectsWithTag("Card");
        CardsTurned = 0;
    }

    public void CheckMatches()
    {
        if (CardsTurned >= 2)
        {
            foreach (var card in ChildCards)
            {
                if (card.GetComponent<Card>().isTurned)
                    card.GetComponent<Card>().ToggleTurn();
            }
        }
    }

    public void UpdateCardsTurned()
    {
        if (ChildCards == null)
            return;

        CardsTurned = 0;
        foreach (var card in ChildCards)
            if (card.GetComponent<Card>().isTurned)
               CardsTurned++;
    }
}
