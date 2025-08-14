using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMode : MonoBehaviour
{
    public GameObject prefabBonus;
    public GameObject prefabStart;

    public GameObject[] prefabTileArray;
    public GameObject[] prefabContArray;
    public GameObject[] prefabBoostArray;

    public float delay = 1f;

    public int maxDifficultyLevel;
    private int currentDifficultyLevel;

    public Transform nextTransform;

    private void Start()
    {
        Application.targetFrameRate = 1000;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        StartCoroutine(IncreaseDifficultyCoroutine());

        SpawnStartTiles();
        SpawnTiles();
    }

    private IEnumerator IncreaseDifficultyCoroutine()
    {
        while (currentDifficultyLevel < maxDifficultyLevel) // Виконувати, поки рівень складності менший за максимальний
        {
            IncreaseDifficulty(); // Викликаємо функцію для збільшення складності
            Debug.Log($"Current Difficulty Level: {currentDifficultyLevel}"); // Лог для перевірки
            yield return new WaitForSeconds(20f); // Чекаємо N секунд перед наступною перевіркою
        }
    }

    public void IncreaseDifficulty()
    {
        if (currentDifficultyLevel < maxDifficultyLevel)
        {
            currentDifficultyLevel++;
        }
    }

    void SpawnStartTiles()
    {
        for (int i = 0; i < 2; i++)
        {
            SpawnOnNextPos(prefabStart);
        }
    }

    void SpawnTiles()
    {
        for (int i = 0; i < 4; i++)
        {
            SpawnObject();
        }
    }

    public void SpawnObject()
    {
        float chanceBonus = Random.value;
        if (chanceBonus < 0.90)
        {
            int randomValue = Random.Range(0, 100);

            if (randomValue < 10)
            {
                SpawnOnNextPos(prefabTileArray[1]);
            }
            else if (randomValue < 30)
            {
                SpawnOnNextPos(prefabBoostArray[Random.Range(0, prefabBoostArray.Length)]);
            }
            else
            {
                SpawnOnNextPos(prefabTileArray[0]);
            }
        }
        else
        {
            SpawnOnNextPos(prefabBonus);
        }
        SpawnOnNextPos(prefabContArray[Random.Range(0,prefabContArray.Length)]);
    }
    private void SpawnOnNextPos(GameObject obj)
    {
        GameObject newObject = Instantiate(obj, nextTransform.position, nextTransform.rotation);
        // Перевірка наявності компонента SpawnObstacle
        SpawnObstacle spawnObstacle = newObject.GetComponent<SpawnObstacle>();
        if (spawnObstacle != null)
        {
            spawnObstacle.UpdateDifficulty(currentDifficultyLevel); // Передача складності
            Debug.Log($"Spawned {newObject.name} with difficulty level: {currentDifficultyLevel}");
        }
        else
        {
            Debug.LogWarning("SpawnObstacle component not found on the instantiated object!");
        }

        Transform nextPosInNewObject = newObject.transform.Find("NextPos");
        nextTransform = nextPosInNewObject.transform;
    }
}
