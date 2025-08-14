using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeIconMusSon : MonoBehaviour
{
    [Header("Sound")]
    public Image buttonIconSound;
    private bool isMutedSound;
    public Sprite muteIconSound;
    public Sprite unmuteIconSound;
    private const string MutePrefKeySound = "IsMutedSound";

    [Header("Music")]
    public Image buttonIconMusic;
    private bool isMutedMusic;
    public Sprite muteIconMusic;
    public Sprite unmuteIconMusic;
    private const string MutePrefKeyMusic = "IsMutedMusic";

    void Start()
    {
        // Load the mute state for sound
        if (PlayerPrefs.HasKey(MutePrefKeySound) == false)
        {
            PlayerPrefs.SetInt(MutePrefKeySound, 0);
        }
        isMutedSound = PlayerPrefs.GetInt(MutePrefKeySound, 0) == 1;

        // Load the mute state for music
        if (PlayerPrefs.HasKey(MutePrefKeyMusic) == false)
        {
            PlayerPrefs.SetInt(MutePrefKeyMusic, 0);
        }
        isMutedMusic = PlayerPrefs.GetInt(MutePrefKeyMusic, 0) == 1;

        // Initial update of the button icons
        UpdateButtonIconSound();
        UpdateButtonIconMusic();
    }

    // Method to toggle the sound mute state
    public void ToggleMuteSound()
    {
        isMutedSound = !isMutedSound;

        PlayerPrefs.SetInt(MutePrefKeySound, isMutedSound ? 1 : 0);
        PlayerPrefs.Save();

        // Update the icon immediately after toggling
        UpdateButtonIconSound();

        Debug.Log("ToggleMuteSound called. Muted: " + isMutedSound);
    }

    // Method to toggle the music mute state
    public void ToggleMuteMusic()
    {
        isMutedMusic = !isMutedMusic;

        PlayerPrefs.SetInt(MutePrefKeyMusic, isMutedMusic ? 1 : 0);
        PlayerPrefs.Save();

        // Update the icon immediately after toggling
        UpdateButtonIconMusic();

        Debug.Log("ToggleMuteMusic called. Muted: " + isMutedMusic);
    }

    // Method to update the sound button icon
    private void UpdateButtonIconSound()
    {
        if (buttonIconSound != null)
        {
            buttonIconSound.sprite = isMutedSound ? muteIconSound : unmuteIconSound;
        }
    }

    // Method to update the music button icon
    private void UpdateButtonIconMusic()
    {
        if (buttonIconMusic != null)
        {
            buttonIconMusic.sprite = isMutedMusic ? muteIconMusic : unmuteIconMusic;
        }
    }
}
