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

        foreach(var card in ChildCards)
        {
            card.GetComponent<Card>().isInteractable = false;
        }
        StartCoroutine("BeginCardDelay");
    }

    IEnumerator BeginCardDelay()
    {
        Debug.Log("Start Delay");
        yield return new WaitForSeconds(5f);
        Debug.Log("Begin Game");
        foreach (var card in ChildCards)
        {
            card.GetComponent<Animator>().enabled = true;
            card.GetComponent<Card>().ToggleTurn();
            card.GetComponent<Card>().isInteractable = true;
        }
        GameObject.FindObjectOfType<GameManager>().isGameActive = true;
    }

    public void CheckMatches()
    {
        bool ocurredMatching = false;
        if (CardsTurned >= 2)
        {
            GameObject.FindObjectOfType<GameManager>().IncreaseTries();
            foreach (var card in ChildCards)
            {
                if (card.GetComponent<Card>().isTurned)
                {
                    if(HowManyCardsAreTurn(card.GetComponent<Card>().id) < 2)
                        card.GetComponent<Card>().ToggleTurn();
                    else
                    {
                        MatchedCards++;
                        if (MatchedCards == ChildCards.Length)
                        {
                            Debug.Log("Won Game");
                            GameObject.FindObjectOfType<GameManager>().isGameActive = false;
                        }
                        if (!card.GetComponent<Card>().isInteractable)
                            continue;
                        card.GetComponent<Card>().isInteractable = false;
                        ocurredMatching = true;
                    }
                }
            }
            if (ocurredMatching)
                GameObject.FindObjectOfType<GameManager>().IncrementTimeLimit();
            else
                GameObject.FindObjectOfType<GameManager>().DecreaseTimeLeft();
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
