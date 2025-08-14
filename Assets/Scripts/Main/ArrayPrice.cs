using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ArrayPrice : MonoBehaviour
{
    public int[] prices = { 0, 999, 1999 };

    private void Start()
    {
        for (int i = 0; i < prices.Length; i++) 
        {
            if (PlayerPrefs.HasKey("Skin_" + i) == null) 
            {
                int result = (prices[i] == 0) ? 0 : 1;
                PlayerPrefs.SetInt("Skin_" + i, result);
                PlayerPrefs.Save();
            }
        }
    }
    public void BuyScin(int skinIndex)
    {
        PlayerPrefs.SetInt("Skin_" + skinIndex, 0);
        PlayerPrefs.Save();
    }

    public bool IsSkinHave(int skinIndex)
    {
        bool result = false;
        if (PlayerPrefs.GetInt("Skin_" + skinIndex) == 0)
        {
            result = true;
        }
        return result;
    }
}
