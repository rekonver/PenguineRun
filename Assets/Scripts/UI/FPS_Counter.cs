using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FPS_Counter : MonoBehaviour
{
    public int fps;
    public TextMeshProUGUI FPSCounterText;

    void Start()
    {
        InvokeRepeating("GetFPS",1,1);
    }
    void GetFPS()
    {
        fps = (int)(1f / Time.unscaledDeltaTime);
        FPSCounterText.text = "FPS: " + fps.ToString();
    }
}
