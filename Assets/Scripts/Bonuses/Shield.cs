using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    private SoundScript soundScript;
    private Coins_Counter TextCoins;
    void Start()
    {
        TextCoins = FindObjectOfType<Coins_Counter>();
        soundScript = FindObjectOfType<SoundScript>();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Obstacles"))
        {
            soundScript.destroyObstacle();
            Destroy(other.transform.parent.gameObject);
        }
        //if (other.gameObject.layer == LayerMask.NameToLayer("Coins"))
        //{
        //    Destroy(other.gameObject);
        //    soundScript.coinSound();
        //    TextCoins.SetCOINS();
        //}
        //if (other.gameObject.layer == LayerMask.NameToLayer("Bonus"))
        //{
        //    soundScript.bonusSound();
        //}
    }
}
