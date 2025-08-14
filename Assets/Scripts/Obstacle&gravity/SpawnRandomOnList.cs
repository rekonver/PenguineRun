using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRandomOnList : MonoBehaviour
{
    public List<GameObject> prefabs; // ������ ������� ��� ������
    public Transform spawnPoint; // ����� ������

    void Start()
    {
        SpawnRandomPrefab();
    }

    public void SpawnRandomPrefab()
    {
        if (prefabs.Count > 0)
        {
            // �������� ���������� ������ �� ������
            int randomIndex = Random.Range(0, prefabs.Count);

            // �������� ������� ������ �� ������� ����� ������
            Instantiate(prefabs[randomIndex], spawnPoint.position, spawnPoint.rotation, this.transform);
        }
        else
        {
            Debug.LogWarning("������ ������� �������!");
        }
    }
}
