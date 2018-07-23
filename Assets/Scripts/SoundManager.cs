using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip MatchedCard;
    public AudioClip FailedMatchedCard;


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
}
