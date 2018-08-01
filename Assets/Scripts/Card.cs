using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public bool isTurned;
    public int id;
    public bool isInteractable;
    public AudioClip CardTurning;

    private Animator anim;
    private CardsGrid cardsGrid;
    private GameManager gameManager;
    private AudioSource audioSource;

	void Start () {
        anim = GetComponent<Animator>();
        cardsGrid = transform.parent.GetComponent<CardsGrid>();
        gameManager = GameObject.FindObjectOfType<GameManager>();
        audioSource = GetComponent<AudioSource>();

        isTurned = false;
        isInteractable = true;

        anim.SetBool("canTurn", false);
        anim.SetBool("isTurned", isTurned);
    }

    void OnMouseDown()
    {
        if (!isInteractable || isTurned || !gameManager.isGameActive)
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
        if(!GameManager.FindObjectOfType<SoundManager>().gameObject.GetComponent<AudioSource>().mute)
        {
            audioSource.clip = CardTurning;
            audioSource.Play();
        }

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
