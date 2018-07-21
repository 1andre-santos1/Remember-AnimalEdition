using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsGrid : MonoBehaviour
{
    public int CardsTurned;
    public Sprite[] CardsSprite;

    private GameObject[] ChildCards;
    private List<int> idsTurned;

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
                {
                    if(HowManyCardsAreTurn(card.GetComponent<Card>().id) < 2)
                        card.GetComponent<Card>().ToggleTurn();
                    else
                    {
                        card.GetComponent<Card>().isInteractable = false;
                    }
                }
            }
        }
    }

    public int HowManyCardsAreTurn(int id)
    {
        int count = 0;
        foreach (var card in ChildCards)
        {
            if (card.GetComponent<Card>().isTurned && card.GetComponent<Card>().id == id)
            {
                count++;
            }
        }
        return count;
    }

    public void UpdateCardsTurned()
    {
        if (ChildCards == null)
            return;

        idsTurned = new List<int>();
        CardsTurned = 0;
        foreach (var card in ChildCards)
        {
            if (card.GetComponent<Card>().isTurned && card.GetComponent<Card>().isInteractable)
            {
                CardsTurned++;
                idsTurned.Add(card.GetComponent<Card>().id);
            }
        }
            
    }
}
