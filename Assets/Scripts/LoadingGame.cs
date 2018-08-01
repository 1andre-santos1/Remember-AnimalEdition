using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingGame : MonoBehaviour
{
    public void BeginGame()
    {
        GameObject.FindObjectOfType<GameManager>().StartGame();
    }
}
