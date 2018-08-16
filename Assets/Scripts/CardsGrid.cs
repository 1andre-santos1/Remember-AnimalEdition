using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsGrid : MonoBehaviour
{
    public int CardsTurned;
    public int MatchedCards;

    public Sprite[] CardsColorWithOneCards;
    public Sprite[] CardsColorWithTwoCards;
    public Sprite[] CardsColorWithThreeCards;
    public Sprite[] CardsColorWithFourCards;
    public Sprite[] CardsColorWithNineCards;

    public Sprite[] CardsSprite;

    public GameObject CardPrefab;

    private GameObject[] ChildCards;
    private List<int> idsTurned;

    private GameManager gameManager;
    private SoundManager soundManager;
    private UIManager uiManager;
    private bool isActive = false;

    public void StartGame()
    {
        isActive = true;
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

        float cardSize = Mathf.Sqrt(5f / numberOfCards);
        float cardMargin = 0.15f;

        float posX = 0f - cardSize / 2f;
        float posY = 0f + cardSize / 2f;
        float cardWidth = -1f;
        float cardHeight = -1f;

        int cardIndex = 0;
        for (int col = 0; col < numberOfCols; col++)
        {
            posY = 0f + cardSize / 2f;
            for (int row = 0; row < numberOfRows; row++)
            {
                GameObject cardP = Instantiate(CardPrefab) as GameObject;
                cardP.transform.localPosition = new Vector3(posX, posY);
                cardP.transform.localScale = new Vector3(cardSize, cardSize, 1f);
                cardP.transform.SetParent(transform);
                if (cardWidth < 0f && cardHeight < 0f)
                {
                    cardWidth = cardP.GetComponent<BoxCollider2D>().bounds.size.x;
                    cardHeight = cardP.GetComponent<BoxCollider2D>().bounds.size.y;
                }

                posY -= cardHeight + cardMargin;
                cardIndex++;

                if (cardIndex >= numberOfCards || posY < -5f)
                    break;
            }
            if (cardIndex >= numberOfCards || posX > 3f)
                break;
            posX += cardWidth + cardMargin;
        }

        ChildCards = GameObject.FindGameObjectsWithTag("Card");
    }
    public bool ContainsAllCardsFromTheGroup(List<Sprite> ListContaining, Sprite[] List)
    {
        foreach (Sprite s in List)
        {
            if (!ListContaining.Contains(s))
                return false;
        }
        return true;
    }
    public void FillGridWithCards()
    {
        List<Sprite> spritesSelected = new List<Sprite>();

        List<Sprite[]> groups = new List<Sprite[]> {CardsColorWithOneCards, CardsColorWithTwoCards, CardsColorWithThreeCards, CardsColorWithFourCards, CardsColorWithNineCards};

        //seleciona os ids (id da carta e do indice em cards sprite)
        for (int i = 0; i < gameManager.NumberOfCards / 2; i++)
        {

            int probGroupWithOneCards = 100 - gameManager.Probability_CardsWithSameColor;
            int probGroupWithTwoCards = Mathf.FloorToInt(gameManager.Probability_CardsWithSameColor * 0.4f);
            int probGroupWithThreeCards = Mathf.FloorToInt(gameManager.Probability_CardsWithSameColor * 0.3f);
            int probGroupWithFourCards = Mathf.FloorToInt(gameManager.Probability_CardsWithSameColor * 0.2f);
            int probGroupWithNineCards = Mathf.FloorToInt(gameManager.Probability_CardsWithSameColor * 0.1f);

            int randomGroup = -1;
            int groupIndex = -1;
            do
            {
                randomGroup = Random.Range(0, 100);

                if (randomGroup < probGroupWithOneCards)
                    groupIndex = 0;
                else if (randomGroup < probGroupWithOneCards + probGroupWithTwoCards)
                    groupIndex = 1;
                else if (randomGroup < probGroupWithOneCards + probGroupWithTwoCards + probGroupWithThreeCards)
                    groupIndex = 2;
                else if (randomGroup < probGroupWithOneCards + probGroupWithTwoCards + probGroupWithThreeCards + probGroupWithFourCards)
                    groupIndex = 3;
                else if (randomGroup < probGroupWithOneCards + probGroupWithTwoCards + probGroupWithThreeCards + probGroupWithFourCards + probGroupWithNineCards)
                    groupIndex = 4;

            } while (ContainsAllCardsFromTheGroup(spritesSelected, groups[groupIndex]));

            int randomNumber = Random.Range(0, groups[groupIndex].Length);

            while (spritesSelected.Contains(groups[groupIndex][randomNumber]))
                randomNumber = Random.Range(0, groups[groupIndex].Length);

            string spriteName = groups[groupIndex][randomNumber].name;

            spritesSelected.Add(groups[groupIndex][randomNumber]);
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
        uiManager.BeginInitialDelayCount(gameManager.InitialDelay);
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
                Card c = card.GetComponent<Card>();
                if (!c.isTurned)
                {
                    if (HowManyCardsAreTurn(c.id) < 2)
                    {
                        soundManager.PlayFailedMatchedCardSound();
                        c.ToggleTurn();
                    }
                    else
                    {
                        MatchedCards++;
                        if (MatchedCards == ChildCards.Length)
                        {
                            GameObject.Find("TopBanner").GetComponent<Animator>().enabled = true;
                            GameObject.Find("TopBanner").GetComponent<Animator>().Play("Increase");
                            gameManager.IncrementTimeLimit();
                            gameManager.isGameActive = false;
                            gameManager.WinGame();
                            return;
                        }
                        if (!c.isInteractable)
                            continue;
                        soundManager.PlayMatchedCardSound();
                        c.isInteractable = false;
                        ocurredMatching = true;
                        uiManager.MakeHostTalk();
                    }
                }
            }
            if (ocurredMatching)
            {
                GameObject.Find("TopBanner").GetComponent<Animator>().enabled = true;
                GameObject.Find("TopBanner").GetComponent<Animator>().Play("Increase");
                gameManager.IncrementTimeLimit();
            }
            else
                gameManager.DecreaseTimeLeft();
        }

    }

    public int HowManyCardsAreTurn(int id)
    {
        int count = 0;
        foreach (var card in ChildCards)
        {
            if (!card.GetComponent<Card>().isTurned && card.GetComponent<Card>().id == id)
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
            if (!c.isTurned && c.isInteractable)
            {
                CardsTurned++;
                idsTurned.Add(card.GetComponent<Card>().id);
            }
        }
    }
}
