using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TogleAnimation : MonoBehaviour
{
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();   
    }

    public void Toggle(string ToTogle)
    {
        bool currentValue = animator.GetBool(ToTogle);
        animator.SetBool(ToTogle, !currentValue);
    }
}
