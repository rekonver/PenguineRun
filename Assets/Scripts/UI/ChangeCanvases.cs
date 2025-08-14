using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCanvases : MonoBehaviour
{
    public Canvas[] canvasesArray;
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
    void Start()
    {
        setActiveOne(0);
    }
}
