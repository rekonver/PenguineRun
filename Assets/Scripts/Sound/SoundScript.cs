using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundScript : MonoBehaviour
{
    public AudioClip audioClipCoin;
    public List<AudioClip> audioClipsObstacleDestroy;
    public List<AudioClip> audioClipsDeath;
    public AudioSource audioSource;

    private float defoultPitch = 1;
    public float bonusPitch = 2f;

    private bool isMutedSound = false; // To keep track of the mute state
    private const string MutePrefKeySound = "IsMutedSound"; // Key to store the mute state in PlayerPrefs

    private void Start()
    {
        // Load the mute state from PlayerPrefs
        if (PlayerPrefs.HasKey(MutePrefKeySound) == false)
        {
            PlayerPrefs.SetInt(MutePrefKeySound, 0);
        }
        isMutedSound = PlayerPrefs.GetInt(MutePrefKeySound, 0) == 1;
        audioSource.mute = isMutedSound;
    }

    public void bonusSound()
    {
        if (!isMutedSound)
        {
            audioSource.pitch = bonusPitch;
            audioSource.PlayOneShot(audioClipCoin);
        }
    }

    public void coinSound()
    {
        if (!isMutedSound)
        {
            audioSource.pitch = defoultPitch;
            audioSource.PlayOneShot(audioClipCoin);
        }
    }

    public void destroyObstacle()
    {
        if (!isMutedSound)
        {
            audioSource.pitch = defoultPitch;
            audioSource.PlayOneShot(audioClipsObstacleDestroy[Random.Range(0, audioClipsObstacleDestroy.Count)]);
        }
    }

    public void deathSound()
    {
        if (!isMutedSound)
        {
            audioSource.pitch = 2.0f;
            audioSource.PlayOneShot(audioClipsDeath[Random.Range(0, audioClipsDeath.Count)]);
        }
    }
}
