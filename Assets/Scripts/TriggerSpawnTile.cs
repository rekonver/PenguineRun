using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using static Cinemachine.CinemachineTriggerAction.ActionSettings;

public class TriggerSpawnTile : MonoBehaviour
{
    private GameMode gameMode;

    void Start()
    {
        gameMode = FindObjectOfType<GameMode>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameMode.SpawnObject();
            Destroy(gameObject, 1f);
        }
    }
}
