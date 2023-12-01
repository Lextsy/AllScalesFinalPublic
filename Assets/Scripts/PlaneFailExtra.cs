using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneFailExtra : MonoBehaviour
{
    private Animator theAnimator;

    private void Awake()
    {
        theAnimator = GetComponent<Animator>();
    }
    public void PlaneFail()
    {
        theAnimator.SetTrigger("FailState");
    }
}
