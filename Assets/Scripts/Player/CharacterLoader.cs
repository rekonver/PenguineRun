using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterLoader : MonoBehaviour
{
    public GameObject[] characters;
    public Transform parentTransform;  // Об'єкт, який буде батьківським для персонажа

    void Start()
    {
        // Завантажуємо індекс вибраного персонажа
        int CharecterInt = PlayerPrefs.GetInt("CharecterInt");

        // Створюємо персонажа
        GameObject character = Instantiate(characters[CharecterInt], parentTransform.position, Quaternion.identity);

        // Встановлюємо персонажа дочірнім до parentTransform
        character.transform.SetParent(parentTransform, false);

        Debug.Log("Завантажено персонажа " + CharecterInt + " як дочірній об'єкт");
    }
}
