using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCanvas : MonoBehaviour
{
    public void StartMenuMusic()
    {
        GameObject.FindObjectOfType<MusicManager>().PlayMenuMusic();
    }
}
