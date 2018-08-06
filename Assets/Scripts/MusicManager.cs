using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioClip[] MenuMusic;
    public AudioClip[] WorldMusic;

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

    public void PlayMenuMusic()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = MenuMusic[Random.Range(0, MenuMusic.Length)];
        audioSource.Play();
    }

    public void PlayWorldMusic(int world)
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = WorldMusic[world];
        audioSource.Play();
    }
}
