using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DancedCharecterMenu : MonoBehaviour
{
    public GameObject[] characters;

    void Start()
    {
        int selectedCharacterIndex = PlayerPrefs.GetInt("CharecterInt", 0);

        SetActiveCharacter();
    }

    public void SetActiveCharacter()
    {
        int selectedCharacterIndex = PlayerPrefs.GetInt("CharecterInt", 0);

        for (int i = 0; i < characters.Length; i++)
        {
            if (i == selectedCharacterIndex)
            {
                characters[i].SetActive(true);  // Активуємо обраного персонажа
            }
            else
            {
                characters[i].SetActive(false); // Деактивуємо інших
            }
        }
    }

    public void ChooseCharacter()
    {
        SetActiveCharacter();
    }
}
