using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneLevelProgressAnimatorScript : MonoBehaviour
{
    private Animator theAnimator;
    private GameDirector theGameDirector;

    private void Awake()
    {
        theAnimator = GetComponent<Animator>();
        theGameDirector = GameObject.Find("GameDirectorObject").GetComponent<GameDirector>();
    }
    private void FixedUpdate()
    {
        SlideLevel();
    }
    public void SlideLevel()
    {
        theAnimator.SetFloat("Blend",theGameDirector.RemainingTime());
    }
}
