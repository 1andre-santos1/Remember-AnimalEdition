using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public bool isTurned;

    private Animator anim;
    private CardsGrid cardsGrid;

	void Start () {
        anim = GetComponent<Animator>();
        anim.enabled = true;
        cardsGrid = transform.parent.GetComponent<CardsGrid>();

        isTurned = false;

        anim.SetBool("canTurn", false);
        anim.SetBool("isTurned", isTurned);
    }

    void OnMouseDown()
    {
        Debug.Log(cardsGrid.CardsTurned);
        if (isTurned || cardsGrid.CardsTurned >= 2)
            return;

        isTurned = !isTurned;
        anim.SetBool("canTurn", true);
        StartCoroutine("TurnCard");
    }

    public void ToggleTurn()
    {
        if (!isTurned)
            return;

        isTurned = !isTurned;
        anim.SetBool("canTurn", true);
        StartCoroutine("TurnCard");
    }

    IEnumerator TurnCard()
    {
        yield return new WaitForSeconds(anim.GetCurrentAnimatorClipInfo(0).Length);

        cardsGrid.UpdateCardsTurned();

        anim.SetBool("isTurned", isTurned);
        anim.SetBool("canTurn", false);

        cardsGrid.CheckMatches();
    }
}
