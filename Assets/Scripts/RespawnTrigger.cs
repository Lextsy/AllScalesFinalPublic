using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class RespawnTrigger : MonoBehaviour
{
    private float Threshold;
    public PlayerMovement player;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            player.TeleportPlayer(new Vector3 (0.0f, 5.0f, 0.0f));
        }
    }
}