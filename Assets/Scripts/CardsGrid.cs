using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsGrid : MonoBehaviour
{
    public int CardsTurned;
    public int MatchedCards;
    public Sprite[] CardsSprite;

    private GameObject[] ChildCards;
    private List<int> idsTurned;

    public void Start()
    {
        ChildCards = GameObject.FindGameObjectsWithTag("Card");
        CardsTurned = 0;
        MatchedCards = 0;
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
                        MatchedCards++;
                        if (MatchedCards == ChildCards.Length)
                            Debug.Log("Won Game");
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
        MatchedCards = 0;
        foreach (var card in ChildCards)
        {
            Card c = card.GetComponent<Card>();
            if (c.isTurned && c.isInteractable)
            {
                CardsTurned++;
                idsTurned.Add(card.GetComponent<Card>().id);
            }
        }
    }
}
