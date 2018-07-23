using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip MatchedCard;
    public AudioClip FailedMatchedCard;
    public AudioClip WinSound;
    public AudioClip LoseSound;

    public void PlayMatchedCardSound()
    {
        GetComponent<AudioSource>().clip = MatchedCard;
        GetComponent<AudioSource>().Play();
    }
    public void PlayFailedMatchedCardSound()
    {
        GetComponent<AudioSource>().clip = FailedMatchedCard;
        GetComponent<AudioSource>().Play();
    }

    public void PlayWinSound()
    {
        GetComponent<AudioSource>().clip = WinSound;
        GetComponent<AudioSource>().Play();
    }

    public void PlayLoseSound()
    {
        GetComponent<AudioSource>().clip = LoseSound;
        GetComponent<AudioSource>().Play();
    }
}
