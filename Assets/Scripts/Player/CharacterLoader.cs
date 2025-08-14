using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterLoader : MonoBehaviour
{
    public GameObject[] characters;
    public Transform parentTransform;  // ��'���, ���� ���� ����������� ��� ���������

    void Start()
    {
        // ����������� ������ ��������� ���������
        int CharecterInt = PlayerPrefs.GetInt("CharecterInt");

        // ��������� ���������
        GameObject character = Instantiate(characters[CharecterInt], parentTransform.position, Quaternion.identity);

        // ������������ ��������� ������� �� parentTransform
        character.transform.SetParent(parentTransform, false);

        Debug.Log("����������� ��������� " + CharecterInt + " �� ������� ��'���");
    }
}
