using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRandomOnList : MonoBehaviour
{
    public List<GameObject> prefabs; // Список префабів для спавну
    public Transform spawnPoint; // Точка спавну

    void Start()
    {
        SpawnRandomPrefab();
    }

    public void SpawnRandomPrefab()
    {
        if (prefabs.Count > 0)
        {
            // Вибираємо випадковий індекс із списку
            int randomIndex = Random.Range(0, prefabs.Count);

            // Спавнимо обраний префаб на вказаній точці спавну
            Instantiate(prefabs[randomIndex], spawnPoint.position, spawnPoint.rotation, this.transform);
        }
        else
        {
            Debug.LogWarning("Список префабів порожній!");
        }
    }
}
