using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingGame : MonoBehaviour
{
    public GameObject BottomBanner;

    public void BeginGame()
    {
        GameObject.FindObjectOfType<GameManager>().StartGame();
        BottomBanner.GetComponent<Animator>().enabled = true;
    }
}
