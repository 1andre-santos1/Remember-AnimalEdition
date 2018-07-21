using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public bool isTurned;

    private Animator anim;

	void Start () {
        anim = GetComponent<Animator>();
        isTurned = false;
	}

    void OnMouseDown()
    {
        anim.enabled = true;
        isTurned = true;
    }
}
