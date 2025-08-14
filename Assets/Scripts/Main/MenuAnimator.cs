using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAnimator : MonoBehaviour
{
    public Animator animator;
    public int animtionCount;

    public bool randomAnim;
    public List<int> randomAnimations;
    void Start()
    {
        if (!randomAnim)
        {
            animator.SetInteger("Int", animtionCount);
        }
        else
        {
            animator.SetInteger("Int", randomAnimations[Random.Range(0,randomAnimations.Count)]);
        }
    }
}
