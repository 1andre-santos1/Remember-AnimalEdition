using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip MatchedCard;
    public AudioClip FailedMatchedCard;
    public AudioClip WinSound;
    public AudioClip LoseSound;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayMatchedCardSound()
    {
        audioSource.clip = MatchedCard;
        audioSource.Play();
    }
    public void PlayFailedMatchedCardSound()
    {
        audioSource.clip = FailedMatchedCard;
        audioSource.Play();
    }

    public void PlayWinSound()
    {
        audioSource.clip = WinSound;
        audioSource.Play();
    }

    public void PlayLoseSound()
    {
        audioSource.clip = LoseSound;
        audioSource.Play();
    }
}
