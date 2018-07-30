using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public Sprite[] ButtonToggle; //0 -> Off, 1 -> On

    public GameObject MusicToggle;
    public GameObject SoundToggle;

    private void Start()
    {
        InitiateMusicToggle();
        InitiateSoundToggle();
    }

    private void InitiateMusicToggle()
    {
        if (!PlayerPrefs.HasKey("Music"))
        {
            MusicToggle.GetComponent<Image>().sprite = ButtonToggle[1];
            PlayerPrefs.SetInt("Music", 1);
        }
        UpdateToggleMusic();
    }

    private void UpdateToggleMusic()
    {
        MusicToggle.GetComponent<Image>().sprite = ButtonToggle[PlayerPrefs.GetInt("Music")];
    }
    private void UpdateToggleSound()
    {
        SoundToggle.GetComponent<Image>().sprite = ButtonToggle[PlayerPrefs.GetInt("Sound")];
    }
    private void InitiateSoundToggle()
    {
        if (!PlayerPrefs.HasKey("Sound"))
        {
            SoundToggle.GetComponent<Image>().sprite = ButtonToggle[1];
            PlayerPrefs.SetInt("Sound", 1);
        }
        UpdateToggleSound();
    }

    public void ToggleMusic()
    {
        int active = PlayerPrefs.GetInt("Music") == 1 ? 0 : 1;

        GameObject.FindObjectOfType<MusicManager>().GetComponent<AudioSource>().mute = active == 0 ? true : false;

        PlayerPrefs.SetInt("Music", active);
        UpdateToggleMusic();
    }
    public void ToggleSound()
    {
        int active = PlayerPrefs.GetInt("Sound") == 1 ? 0 : 1;

        GameObject.FindObjectOfType<SoundManager>().GetComponent<AudioSource>().mute = active == 0 ? true : false;

        PlayerPrefs.SetInt("Sound", active);
        UpdateToggleSound();
    }
}
