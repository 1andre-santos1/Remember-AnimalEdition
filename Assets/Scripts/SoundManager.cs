﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip MatchedCard;
    public AudioClip FailedMatchedCard;
    public AudioClip WinSound;
    public AudioClip LoseSound;
    public AudioClip UnlockSound;

    private AudioSource audioSource;

    public static SoundManager i;

    // Use this for initialization
    void Awake()
    {
        if (!i)
        {
            i = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

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

    public void PlayUnlockSound()
    {
        audioSource.clip = UnlockSound;
        audioSource.Play();
    }
}
