using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPlayer : MonoBehaviour
{
    public Coins_Counter TextCoins;
    public FirstPersonMovement player;
    public CombineBonusManager bonusManager;
    public SoundScript soundScript;

    private void Start()
    {
        if (TextCoins == null) 
        { 
            TextCoins = FindObjectOfType<Coins_Counter>();
        }
        if (player == null)
        {
            player = FindObjectOfType<FirstPersonMovement>();
        }
        if (soundScript == null)
        {
            soundScript = FindObjectOfType<SoundScript>();
        }
        if(bonusManager == null)
        {
            bonusManager = FindObjectOfType<CombineBonusManager>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Coins"))
        {
            TextCoins.SetCOINS();
            soundScript.coinSound();
            Destroy(other.gameObject);
        }
        if (other.CompareTag("BoostHorizontal"))
        {
            player.horizontalBoostTrigger = true;
        }
        if (other.CompareTag("BoostForward"))
        {
            player.forwardBoostTrigger = true;
        }
        if (other.CompareTag("Shield"))
        {
            bonusManager.shieldFunc();
            soundScript.bonusSound();
            Destroy(other.gameObject);
        }
        if (other.CompareTag("Wings"))
        {
            bonusManager.wingsFunc();
            soundScript.bonusSound();
            Destroy(other.gameObject);
        }
        if (other.CompareTag("Magnet"))
        {
            bonusManager.magnetFunc();
            soundScript.bonusSound();
            Destroy(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("BoostHorizontal"))
        {
            player.horizontalBoostTrigger = false;
        }
        if (other.CompareTag("BoostForward"))
        {
            player.forwardBoostTrigger = false;
        }
    }
}
