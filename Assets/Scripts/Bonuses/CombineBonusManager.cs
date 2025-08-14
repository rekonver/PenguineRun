using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombineBonusManager : MonoBehaviour
{
    private float timerMinus = 0.1f;

    [Header("Shield Settings")]
    public GameObject shieldPrefab;
    public Transform shieldSpawnPoint;
    public float shieldTime;
    private GameObject playerShield;
    private Animator shieldAnimator;
    private float shieldDuration;


    [Header("Wings Settings")]
    public GameObject wingsPrefab;
    public Transform wingsSpawnPoint;
    public float wingsTime;
    private GameObject playerWings;
    private Animator wingsAnimator;
    private float wingsDuration;

    [Header("Magnet Settings")]
    public GameObject magnetPrefab;
    public Transform magnetSpawnPoint;
    public Magnet magnetScript;
    public float magnetTime;
    private GameObject playerMagnet;
    private Animator magnetAnimator;
    private float magnetDuration;


    public Jump jumpScript;

    private void Start()
    {
        //shieldFunc();
        //wingsFunc();
        //magnetFunc();
    }

    public void shieldFunc()
    {
        if (playerShield == null)
        {
            playerShield = Instantiate(shieldPrefab, shieldSpawnPoint.position, shieldSpawnPoint.rotation, shieldSpawnPoint);
            shieldAnimator = playerShield.GetComponent<Animator>();
            CancelInvoke("RepeatingShield");
        }

        shieldDuration = shieldTime;
        InvokeRepeating("RepeatingShield", 0f, timerMinus);
    }

    private void RepeatingShield()
    {
        if (shieldDuration > 0)
        {
            shieldDuration -= timerMinus;
        }
        else
        {
            CancelInvoke("RepeatingShield");
            shieldDuration = 0;
            if (playerShield != null)
            {
                if (shieldAnimator != null)
                {
                    shieldAnimator.SetBool("death", true);
                }
            }
        }
    }

    public void wingsFunc()
    {
        jumpScript.maxAirJump = 1;
        jumpScript.airJumpCount = 1;

        if (playerWings == null)
        {
            playerWings = Instantiate(wingsPrefab, wingsSpawnPoint.position, wingsSpawnPoint.rotation, wingsSpawnPoint);
            wingsAnimator = playerWings.GetComponent<Animator>();
            CancelInvoke("RepeatingWings");
        }

        wingsDuration = wingsTime;
        InvokeRepeating("RepeatingWings", 0f, timerMinus);
    }

    private void RepeatingWings()
    {
        if (wingsDuration > 0)
        {
            wingsDuration -= timerMinus;
            if (wingsAnimator != null)
            {
                wingsAnimator.SetBool("death", false);
            }
        }
        else
        {
            CancelInvoke("RepeatingWings");
            wingsDuration = 0;
            jumpScript.maxAirJump = 0;
            if (wingsAnimator != null)
            {
                wingsAnimator.SetBool("death", true);
            }
        }
    }
    public void wingsAnimatorFunc(bool isFall, int flyJumpCount)
    {
        if (wingsAnimator != null)
        {
            if (isFall && flyJumpCount > 0)
            {
                wingsAnimator.SetBool("isFalling", true);
            }
            else
            {

                wingsAnimator.SetBool("isFalling", false);
            }
        }
    }

    public void magnetFunc()
    {
        if (playerMagnet == null)
        {
            playerMagnet = Instantiate(magnetPrefab, magnetSpawnPoint.position, magnetSpawnPoint.rotation, magnetSpawnPoint);
            magnetAnimator = playerMagnet.GetComponent<Animator>();
            CancelInvoke("RepeatingMagnet");
        }
        magnetScript.magnetActive = true;
        magnetDuration = magnetTime;
        InvokeRepeating("RepeatingMagnet", 0f, timerMinus);
    }

    private void RepeatingMagnet()
    {
        if (magnetDuration > 0)
        {
            magnetDuration -= timerMinus;
        }
        else
        {
            CancelInvoke("RepeatingMagnet");
            magnetDuration = 0;
            magnetScript.magnetActive = false;
            if (playerMagnet != null)
            {
                if (magnetAnimator != null)
                {
                    magnetAnimator.SetBool("death", true);
                }
            }
        }
    }
}
