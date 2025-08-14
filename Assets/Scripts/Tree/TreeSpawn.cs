using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeSpawn : MonoBehaviour
{
    public List<GameObject> spawnList = new List<GameObject>();
    public Transform spawnPoint;
    public GameObject spawnedTree;

    void Start()
    {
        SpawnRandomElement();
    }

    void SpawnRandomElement()
    {
        if (spawnList.Count > 0)
        {
            int randomIndex = Random.Range(0, spawnList.Count);

            Instantiate(spawnList[randomIndex], spawnPoint.position, spawnPoint.rotation, this.transform);
        }
        else
        {
            Debug.LogWarning("Список для спавну порожній!");
        }
    }
}
