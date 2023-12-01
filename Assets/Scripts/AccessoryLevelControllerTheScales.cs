using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccessoryLevelControllerTheScales : MonoBehaviour
{
    [SerializeField]private GameObject colliderToDisable1;
    [SerializeField]private GameObject colliderToDisable2;

    public void DisableExtraColliders()
    {
        colliderToDisable1.GetComponent<Collider>().isTrigger = false;
        colliderToDisable2.GetComponent<Collider>().isTrigger = false;
    }
}
