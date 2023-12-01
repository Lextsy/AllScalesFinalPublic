using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnubisController : MonoBehaviour
{
    private Animator theAnimator;

    private void Start()
    {
        theAnimator = GetComponent<Animator>();
    }
    public void IntroduceAnubis()
    {
        theAnimator.SetTrigger("Start");
    }

    public void SlideAnubis(float value)
    {
        theAnimator.SetFloat("Blend",value);
    }
    public void ExitAnubis()
    {
        theAnimator.SetTrigger("Finish");
    }
}