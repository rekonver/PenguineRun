using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsPickUP : MonoBehaviour
{
    private Coins_Counter TextCoins;
    void Start()
    {
        TextCoins = FindObjectOfType<Coins_Counter>();
    }

    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TextCoins.SetCOINS();
            Destroy(gameObject);
        }
    }
}
