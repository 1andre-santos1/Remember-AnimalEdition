using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsGrid : MonoBehaviour
{
    private List<Card> ChildCards;

    public void Start()
    {
        ChildCards = new List<Card>();
        int childrenCount = transform.childCount;
        for (int i = 0; i < childrenCount; i++)
            ChildCards.Add(transform.GetChild(i).GetComponent<Card>());
    }

    private void Update()
    {
        Debug.Log("Cards Turned: "+GetCardsTurned());
    }

    private int GetCardsTurned()
    {
        if (ChildCards == null)
            return 0;

        int count = 0;
        foreach (var card in ChildCards)
            if (card.isTurned)
                count++;
        return count;
    }
}
