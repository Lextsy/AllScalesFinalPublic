using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlatformColliderScript : MonoBehaviour
{
    // This Script is to be placed on each platform side, which will cause the platform to act.
    public float magnitude = 0;
    public PlatformControllerScript platController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //Debug.Log(other.gameObject.name);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            platController.PlatformAct(magnitude);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            //Debug.Log(other.gameObject.name);
        }
    }
}
