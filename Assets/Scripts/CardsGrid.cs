using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsGrid : MonoBehaviour
{
    public int CardsTurned;
    public int MatchedCards;
    public Sprite[] CardsSprite;
    public GameObject CardPrefab;

    private GameObject[] ChildCards;
    private List<int> idsTurned;

    private GameManager gameManager;
    private SoundManager soundManager;
    private UIManager uiManager;

    public void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        soundManager = GameObject.FindObjectOfType<SoundManager>();
        uiManager = GameObject.FindObjectOfType<UIManager>();

        InstantiateCards();

        CardsTurned = 0;
        MatchedCards = 0;

        FillGridWithCards();

        StartCoroutine("BeginCardDelay");
    }

    public void InstantiateCards()
    {
        int numberOfCards = gameManager.NumberOfCards;

        int numberOfRows = numberOfCards / 2;
        int numberOfCols = Mathf.FloorToInt(numberOfCards / 3);

        float cardSize = Mathf.Sqrt(5f/numberOfCards);
        float cardMargin = 0.15f;

        float posX = 0f - cardSize/2f;
        float posY = 0f + cardSize/2f;
        float cardWidth = -1f;
        float cardHeight = -1f;

        int cardIndex = 0;
        for (int col = 0; col < numberOfCols; col++)
        {
            posY = 0f + cardSize / 2f;
            for(int row = 0; row < numberOfRows; row++)
            {
                GameObject cardP = Instantiate(CardPrefab) as GameObject;
                cardP.transform.localPosition = new Vector3(posX, posY);
                cardP.transform.localScale = new Vector3(cardSize, cardSize,1f);
                cardP.transform.SetParent(transform);
                if(cardWidth < 0f && cardHeight < 0f)
                {
                    cardWidth = cardP.GetComponent<BoxCollider2D>().bounds.size.x;
                    cardHeight = cardP.GetComponent<BoxCollider2D>().bounds.size.y;
                }

                posY -= cardHeight + cardMargin;
                cardIndex++;

                if (cardIndex >= numberOfCards || posY < -6.4f)
                    break;
            }
            if (cardIndex >= numberOfCards || posX > 3f)
                break;
            posX += cardWidth + cardMargin;
        }

        ChildCards = GameObject.FindGameObjectsWithTag("Card");
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
        yield return new WaitForSeconds(gameManager.InitialDelay);
        foreach (var card in ChildCards)
        {
            card.GetComponent<Animator>().enabled = true;
            card.GetComponent<Card>().ToggleTurn();
            card.GetComponent<Card>().isInteractable = true;
        }
        gameManager.isGameActive = true;
        uiManager.StartBarDecreasing();
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
                    if (HowManyCardsAreTurn(card.GetComponent<Card>().id) < 2)
                    {
                        soundManager.PlayFailedMatchedCardSound();
                        card.GetComponent<Card>().ToggleTurn();
                    }
                    else
                    {
                        MatchedCards++;
                        if (MatchedCards == ChildCards.Length)
                        {
                            gameManager.IncrementTimeLimit();
                            gameManager.isGameActive = false;
                            gameManager.WinGame();
                            return;
                        }
                        if (!card.GetComponent<Card>().isInteractable)
                            continue;
                        soundManager.PlayMatchedCardSound();
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
