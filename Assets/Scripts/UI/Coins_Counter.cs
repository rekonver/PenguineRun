using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Coins_Counter : MonoBehaviour
{
    public int Coins = 0;
    public TextMeshProUGUI COINSCounterText;
    void Start()
    {
        COINSCounterText.text = "Coins: " + Coins.ToString();
    }
    public void SetCOINS()
    {
        Coins++;
        COINSCounterText.text = "Coins: " + Coins.ToString();
    }
}
