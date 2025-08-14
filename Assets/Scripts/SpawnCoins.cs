using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;
using Unity.VisualScripting;
using System.Linq;


public class SpawnCoins : MonoBehaviour
{
    private List<int> freePosList;
    private List<int> jumpPosList;
    private List<int> slidePosList;
    

    public GameObject[] SplineArray;
    public Transform[] transformsCoinsArray;
    public bool RandomSpawn = false;

    private void Start()
    {
        if (RandomSpawn)
        {
            SpawnPos(Random.Range(0, SplineArray.Length));
        }
    }

    public void GetInfoObstacle(List<int> freeSended, List<int> jumpSended, List<int> slideSended)
    {
        if (!RandomSpawn)
        {
            freePosList = freeSended;
            jumpPosList = jumpSended;
            slidePosList = slideSended;

            if (freePosList.Count > 0)
            {
                int random = Random.Range(0, freePosList.Count);
                SpawnPos(freePosList[random]);
            }
            else if (jumpPosList.Count > 0)
            {
                int random = Random.Range(0, jumpSended.Count);
                SpawnPos(jumpPosList[random]);
            }
            else if (slideSended.Count > 0) 
            {
                int random = Random.Range(0, slidePosList.Count);
                SpawnPos(slidePosList[random]);
            }
        }
    }

    void SpawnPos(int i)
    {
        int SplineInt;
        switch (i) 
        { 
            case 0:
                SplineInt = Random.Range(0,2);
                SpawnInPosition(SplineInt, transformsCoinsArray[i]);
                break;
            case 1:
                SplineInt = Random.Range(0, 3);
                SpawnInPosition(SplineInt, transformsCoinsArray[i]);
                break;
            case 2:
                SplineInt = Random.Range(1, 3);
                SpawnInPosition(SplineInt, transformsCoinsArray[i]);
                break;
            default:

                break;
        }

    }
    void SpawnInPosition(int i, Transform TransformObstacle)
    {
        Instantiate(SplineArray[i], TransformObstacle.position, TransformObstacle.rotation, this.transform);
    }
}
