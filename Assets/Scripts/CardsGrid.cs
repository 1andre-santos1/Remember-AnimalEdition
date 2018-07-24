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
    private GameManager gameManager;

    public void Start()
    {
        ChildCards = GameObject.FindGameObjectsWithTag("Card");
        gameManager = GameObject.FindObjectOfType<GameManager>();

        CardsTurned = 0;
        MatchedCards = 0;

        FillGridWithCards();
        
        StartCoroutine("BeginCardDelay");
    }

    public void FillGridWithCards()
    {
        List<Sprite> spritesSelected = new List<Sprite>();

        //seleciona os ids (id da carta e do indice em cards sprite)
        for (int i = 0; i < gameManager.NumberOfCards / 2; i++)
        {
            int randomNumber = Random.Range(0, CardsSprite.Length);
            while (spritesSelected.Contains(CardsSprite[randomNumber]))
                randomNumber = Random.Range(0, CardsSprite.Length);

            spritesSelected.Add(CardsSprite[randomNumber]);
        }

        List<int> emptyCards = new List<int>();
        int count = 0;
        foreach (var card in ChildCards)
        {
            card.GetComponent<Card>().isInteractable = false;
            int aux = count;
            emptyCards.Add(aux);
            count++;
        }

        foreach (var sprite in spritesSelected)
        {
            int n = Random.Range(0, emptyCards.Count);
            int card1 = emptyCards[n];
            emptyCards.Remove(emptyCards[n]);

            n = Random.Range(0, emptyCards.Count);
            int card2 = emptyCards[n];
            emptyCards.Remove(emptyCards[n]);

            ChildCards[card1].transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = sprite;
            ChildCards[card1].GetComponent<Card>().id = spritesSelected.IndexOf(sprite);

            ChildCards[card2].transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = sprite;
            ChildCards[card2].GetComponent<Card>().id = spritesSelected.IndexOf(sprite);
        }
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
        gameManager.isGameActive = true;
    }

    public void CheckMatches()
    {
        bool ocurredMatching = false;
        if (CardsTurned >= 2)
        {
            gameManager.IncreaseTries();
            foreach (var card in ChildCards)
            {
                if (card.GetComponent<Card>().isTurned)
                {
                    if(HowManyCardsAreTurn(card.GetComponent<Card>().id) < 2)
                    {
                        GameObject.FindObjectOfType<SoundManager>().PlayFailedMatchedCardSound();
                        card.GetComponent<Card>().ToggleTurn();
                    }
                    else
                    {
                        MatchedCards++;
                        if (MatchedCards == ChildCards.Length)
                        {
                            Debug.Log("Won Game");
                            gameManager.isGameActive = false;
                            gameManager.WinGame();
                        }
                        if (!card.GetComponent<Card>().isInteractable)
                            continue;
                        GameObject.FindObjectOfType<SoundManager>().PlayMatchedCardSound();
                        card.GetComponent<Card>().isInteractable = false;
                        ocurredMatching = true;
                    }
                }
            }
            if (ocurredMatching)
                gameManager.IncrementTimeLimit();
            else
                gameManager.DecreaseTimeLeft();
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
