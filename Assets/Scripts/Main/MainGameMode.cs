using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainGameMode : MonoBehaviour
{
    public Canvas[] canvasesArray;
    public bool HardMode;
    public TextMeshProUGUI textMode;
    void Start()
    {
        setActiveOne(0);
        if (PlayerPrefs.HasKey("CharecterInt") == null)
        {
            PlayerPrefs.SetInt("CharecterInt",0);
        }
        if (PlayerPrefs.HasKey("Coins") == false)
        {
            PlayerPrefs.SetInt("Coins", 0);
        }
        if (PlayerPrefs.HasKey("HardMode") == false)
        {
            PlayerPrefs.SetInt("HardMode", 0);
        }
        else
        {
            if (PlayerPrefs.GetInt("HardMode") == 0)
            {
                HardMode = false;
            }
            else 
            { 
                HardMode = true;
            }
        }
        changeModeText();
        gameHardMode();
    }

    private void gameHardMode()
    {
        if (HardMode) 
        {
            PlayerPrefs.SetInt("StartSpeed", 10);
            PlayerPrefs.SetInt("EndSpeed", 14);
            PlayerPrefs.SetFloat("SlideDuration", 0.75f);
            PlayerPrefs.SetInt("HardMode", 1);
        }
        else
        {
            PlayerPrefs.SetInt("StartSpeed", 6);
            PlayerPrefs.SetInt("EndSpeed", 8);
            PlayerPrefs.SetFloat("SlideDuration", 1f);
            PlayerPrefs.SetInt("HardMode", 0);
        }
        PlayerPrefs.Save();
    }
    public void changeMode()
    {
        HardMode = !HardMode;
        gameHardMode();
        changeModeText();
    }
    private void changeModeText()
    {
        if (!HardMode)
        {
            textMode.text = "Normal Mode";
        }
        else 
        {
            textMode.text = "Hard Mode";
        }
    }
    public void setActiveOne(int number)
    {
        for (int i = 0; i < canvasesArray.Length; i++)
        {
            if (i == number)
            {
                canvasesArray[i].gameObject.SetActive(true);
            }
            else
            {
                canvasesArray[i].gameObject.SetActive(false);
            }
        }
    }
}
