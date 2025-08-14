using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnStart : MonoBehaviour
{
    public GameObject[] objectsToDestroy; // ����� ��'���� ��� ���������

    void Start()
    {
        foreach (GameObject obj in objectsToDestroy)
        {
            if (obj != null)
            {
                Destroy(obj);
            }
        }
    }
}
