using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class SpawnObstacle : MonoBehaviour
{
    public Transform[] transformsArray;
    public GameObject[] obstacleArray;

    private List<int> freePositions = new List<int>();
    private List<int> wallPositions = new List<int>();
    private List<int> jumpPositions = new List<int>();
    private List<int> slidePositions = new List<int>();

    public SpawnCoins spawnCoinsScript;

    public int currentDifficultyLevel = 1;
    void Start()
    {
        //SpawnObstaclesBasedOnChance();
        SpawnObstaclesBasedOnDifficulty();

        if (spawnCoinsScript != null) 
        {
            spawnCoinsScript.GetInfoObstacle(freePositions, jumpPositions, slidePositions);
        }
    }

    public void UpdateDifficulty(int newDifficulty)
    {
        currentDifficultyLevel = newDifficulty;
        Debug.Log($"Updated difficulty level to: {currentDifficultyLevel}");
    }

    void SpawnObstaclesBasedOnDifficulty()
    {
        int obstacleCount = 1;

        switch (currentDifficultyLevel)
        {
            case 1:
                // Рівень 1: Спавниться 1 або 2 перешкоди
                obstacleCount = Random.value < 0.5f ? 1 : 2;
                break;

            case 2:
                // Рівень 2: 25% шанс на 1 перешкоду, 75% на 2
                obstacleCount = Random.value < 0.25f ? 1 : 2;
                break;

            case 3:
                // Рівень 3: 75% шанс на 2 перешкоди, 25% на 3
                obstacleCount = Random.value < 0.75f ? 2 : 3;
                break;

            case 4:
                // Рівень 4: 50% шанс на 2 або 3 перешкоди
                obstacleCount = Random.value < 0.5f ? 2 : 3;
                break;

            case 5:
                // Рівень 5: 90% шанс на 3 перешкоди, 10% на 1 або 2
                obstacleCount = Random.value < 0.1f ? Random.Range(1, 3) : 3;
                break;

            default:
                // За замовчуванням: спавниться 1 перешкода
                obstacleCount = 1;
                break;
        }

        // Виклик функції для спавну перешкод
        SpawnObstaclesCount(obstacleCount);
    }

    void SpawnObstaclesBasedOnChance()
    {
        float chance = Random.value;
        if (chance < 0.3f)
        {
            SpawnObstaclesCount(1);
        }
        else if (chance < 0.6f)
        {
            SpawnObstaclesCount(2);
        }
        else
        {
            SpawnObstaclesCount(3);
        }
    }


    void SpawnObstaclesCount(int Count)
    {
        List<int> CanObstaclePosition = SpawnObstaclePositionList(transformsArray.Length);

        for (int i = 0; i < Count; i++)
        {
            int ObstacleIndex;

            if (wallPositions.Count < 2)
            {
                ObstacleIndex = Random.Range(0, obstacleArray.Length);
            }
            else
            {
                ObstacleIndex = Random.Range(1, obstacleArray.Length);
            }
            
            int RandomPosition = Random.Range(0, CanObstaclePosition.Count);

            Transform TransformObstacle = transformsArray[CanObstaclePosition[RandomPosition]];

            Instantiate(obstacleArray[ObstacleIndex], TransformObstacle.position, TransformObstacle.rotation, this.transform);

            switch (ObstacleIndex)
            {
                case 0:
                    wallPositions.Add(CanObstaclePosition[RandomPosition]);
                    break;
                case 1:
                    jumpPositions.Add(CanObstaclePosition[RandomPosition]);
                    break;
                case 2:
                    slidePositions.Add(CanObstaclePosition[RandomPosition]);
                    break;
                default:
                    break;
            }
            CanObstaclePosition.RemoveAt(RandomPosition);
        }

        freePositions = CanObstaclePosition;
    }

    List<int> SpawnObstaclePositionList(int length)
    {
        List<int> FuncList = new List<int>(length);
        for (int i = 0; i < length; i++)
        {
            FuncList.Add(i);
        }
        return FuncList;
    }
}
