using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetAnimator : MonoBehaviour
{
    FirstPersonMovement charecter;
    Animator animator;
    void Start()
    {
        charecter = FindObjectOfType<FirstPersonMovement>();
        animator = GetComponent<Animator>();
        charecter.SetAnimator(animator);
    }

    
    void Update()
    {
        
    }
}
