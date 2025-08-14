using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBonuses : MonoBehaviour
{
    public List<Transform> bonusTransforms;
    public List<GameObject> gameObjects;

    void Start()
    {
        int randomIntTransform = Random.Range(0, bonusTransforms.Count);
        int randomIntBonus = Random.Range(0, gameObjects.Count);
        Instantiate(gameObjects[randomIntBonus], bonusTransforms[randomIntTransform].position, bonusTransforms[randomIntTransform].rotation, this.transform);
    }
}
