using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource audioSource;
    public List<AudioClip> musicClips; // Список музичних треків

    private List<AudioClip> remainingClips; // Музичні треки, які ще не грали
    private AudioClip lastPlayedClip; // Останній програний трек

    private bool isMutedMusic; // Стан муту для музики
    private const string MutePrefKeyMusic = "IsMutedMusic";

    private void Start()
    {
        // Завантаження стану муту з PlayerPrefs
        if (PlayerPrefs.HasKey(MutePrefKeyMusic) == false)
        {
            PlayerPrefs.SetInt(MutePrefKeyMusic, 0);
        }
        isMutedMusic = PlayerPrefs.GetInt(MutePrefKeyMusic, 0) == 1;

        // Ініціалізація списку треків, які ще не грали
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
            // Всі треки вже були програні, відновлюємо список
            remainingClips = new List<AudioClip>(musicClips);

            // Видалити останній програний трек зі списку
            if (lastPlayedClip != null)
            {
                remainingClips.Remove(lastPlayedClip);
            }
        }

        // Вибір випадкового треку
        int randomIndex = Random.Range(0, remainingClips.Count);
        AudioClip currentClip = remainingClips[randomIndex];
        remainingClips.RemoveAt(randomIndex);

        // Встановлення та відтворення треку
        audioSource.clip = currentClip;
        audioSource.Play();

        // Запуск корутини для очікування тривалості треку
        StartCoroutine(WaitForMusicEnd(currentClip));
    }

    private IEnumerator WaitForMusicEnd(AudioClip currentClip)
    {
        // Очікування завершення треку
        yield return new WaitForSeconds(currentClip.length);

        // Оновлення останнього програного треку
        lastPlayedClip = currentClip;

        // Перемикання на новий трек
        PlayRandomMusic();
    }

    // Метод для перемикання стану муту музики
    public void ToggleMuteMusic()
    {
        isMutedMusic = !isMutedMusic;

        // Збереження стану муту в PlayerPrefs
        PlayerPrefs.SetInt(MutePrefKeyMusic, isMutedMusic ? 1 : 0);
        PlayerPrefs.Save();

        // Зупинити відтворення музики, якщо вона вимкнена
        if (isMutedMusic)
        {
            audioSource.Stop();
        }
        else
        {
            PlayRandomMusic(); // Продовжити відтворення музики, якщо вона увімкнена
        }
    }
}
