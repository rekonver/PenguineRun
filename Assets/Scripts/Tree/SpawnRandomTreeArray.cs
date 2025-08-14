using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRandomTreeArray : MonoBehaviour
{
    public List<GameObject> treesTypes;
    public Transform spawnPoint;
    public GameObject previewObject;
    void Start()
    {
        Destroy(previewObject); 
        int randomIndex = Random.Range(0, treesTypes.Count);

        Instantiate(treesTypes[randomIndex], spawnPoint.position, spawnPoint.rotation, this.transform);
    }
}
