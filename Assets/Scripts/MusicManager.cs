using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioClip Music;

    private AudioSource audioSource;

    public static MusicManager i;

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
        audioSource.clip = Music;
        audioSource.Play();
    }
}
