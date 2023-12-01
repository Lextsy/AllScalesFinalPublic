using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.Utilities;

public class PlatformControllerScript : MonoBehaviour
{
    [SerializeField] Animator platformAnimator;
    public float tiltSpeed = .001f;
    public float platformHealth = 1.0f;

    public GameDirector theGameDirector;

    void Start()
    {
        platformAnimator = GetComponent<Animator>();
        theGameDirector = GameObject.Find("GameDirectorObject").GetComponent<GameDirector>();
    }

    public void PlatformAct(float amountToAct)
    {
        var currentblend = platformAnimator.GetFloat("Blend");
        var newblend = Mathf.Clamp(currentblend + amountToAct * Time.deltaTime * tiltSpeed,0,1);
        platformAnimator.SetFloat("Blend", newblend);
    }

    public void PlatformTakeDamage(float damageTaken)
    {
        platformHealth -= damageTaken;
        if (platformHealth <= 0)
        {
            //Game Fail State
            theGameDirector.GameFailState();
        }
    }
}
