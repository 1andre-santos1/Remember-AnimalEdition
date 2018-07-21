﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public bool isTurned;
    public int id;
    public bool isInteractable;

    private Animator anim;
    private CardsGrid cardsGrid;

	void Start () {
        anim = GetComponent<Animator>();
        anim.enabled = true;
        cardsGrid = transform.parent.GetComponent<CardsGrid>();

        isTurned = false;
        isInteractable = true;

        anim.SetBool("canTurn", false);
        anim.SetBool("isTurned", isTurned);
    }

    void OnMouseDown()
    {
        if (!isInteractable)
            return;
        if (isTurned)
            return;

        isTurned = !isTurned;
        anim.SetBool("canTurn", true);
        StartCoroutine("TurnCard");
    }

    public void ToggleTurn()
    {
        if (!isInteractable)
            return;
        if (!isTurned)
            return;

        isTurned = !isTurned;
        anim.SetBool("canTurn", true);
        StartCoroutine("TurnCard");
    }

    IEnumerator TurnCard()
    {
        yield return new WaitForSeconds(anim.GetCurrentAnimatorClipInfo(0).Length);

        if (isInteractable)
        {
            cardsGrid.UpdateCardsTurned();

            anim.SetBool("isTurned", isTurned);
            anim.SetBool("canTurn", false);

            cardsGrid.CheckMatches();
        }
    }
}
