using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource audioSource;
    public List<AudioClip> musicClips; // ������ �������� �����

    private List<AudioClip> remainingClips; // ������ �����, �� �� �� �����
    private AudioClip lastPlayedClip; // ������� ��������� ����

    private bool isMutedMusic; // ���� ���� ��� ������
    private const string MutePrefKeyMusic = "IsMutedMusic";

    private void Start()
    {
        // ������������ ����� ���� � PlayerPrefs
        if (PlayerPrefs.HasKey(MutePrefKeyMusic) == false)
        {
            PlayerPrefs.SetInt(MutePrefKeyMusic, 0);
        }
        isMutedMusic = PlayerPrefs.GetInt(MutePrefKeyMusic, 0) == 1;

        // ����������� ������ �����, �� �� �� �����
        remainingClips = new List<AudioClip>(musicClips);
        PlayRandomMusic();
    }

    private void PlayRandomMusic()
    {
        if (isMutedMusic)
        {
            audioSource.Stop();
            return;
        }

        if (remainingClips.Count == 0)
        {
            // �� ����� ��� ���� �������, ���������� ������
            remainingClips = new List<AudioClip>(musicClips);

            // �������� ������� ��������� ���� � ������
            if (lastPlayedClip != null)
            {
                remainingClips.Remove(lastPlayedClip);
            }
        }

        // ���� ����������� �����
        int randomIndex = Random.Range(0, remainingClips.Count);
        AudioClip currentClip = remainingClips[randomIndex];
        remainingClips.RemoveAt(randomIndex);

        // ������������ �� ���������� �����
        audioSource.clip = currentClip;
        audioSource.Play();

        // ������ �������� ��� ���������� ��������� �����
        StartCoroutine(WaitForMusicEnd(currentClip));
    }

    private IEnumerator WaitForMusicEnd(AudioClip currentClip)
    {
        // ���������� ���������� �����
        yield return new WaitForSeconds(currentClip.length);

        // ��������� ���������� ���������� �����
        lastPlayedClip = currentClip;

        // ����������� �� ����� ����
        PlayRandomMusic();
    }

    // ����� ��� ����������� ����� ���� ������
    public void ToggleMuteMusic()
    {
        isMutedMusic = !isMutedMusic;

        // ���������� ����� ���� � PlayerPrefs
        PlayerPrefs.SetInt(MutePrefKeyMusic, isMutedMusic ? 1 : 0);
        PlayerPrefs.Save();

        // �������� ���������� ������, ���� ���� ��������
        if (isMutedMusic)
        {
            audioSource.Stop();
        }
        else
        {
            PlayRandomMusic(); // ���������� ���������� ������, ���� ���� ��������
        }
    }
}
