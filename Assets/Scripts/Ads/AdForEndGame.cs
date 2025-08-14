using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdForEndGame : MonoBehaviour
{
    public int DeathCountToAd = 6;
    public Ads adManager;

    private string deathCountSave = "DeathCountToAd";
    private int nowDeath;

    void Start()
    {
        if (PlayerPrefs.HasKey(deathCountSave) == false)
        {
            PlayerPrefs.SetInt(deathCountSave, 0);
        }
        nowDeath = PlayerPrefs.GetInt(deathCountSave);
        adManager.LoadInterstitialAd();
    }
    public void DeathAd()
    {
        if(nowDeath < DeathCountToAd)
        {
            nowDeath++;
            PlayerPrefs.SetInt(deathCountSave, nowDeath);
        }
        else
        {
            nowDeath = 0;
            PlayerPrefs.SetInt(deathCountSave, nowDeath);

            adManager.ShowInterstitialAd();
        }
        PlayerPrefs.Save();
    }

}
