using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Gold : MonoBehaviourPunCallbacks
{
    public Player player;

    public void OnTriggerEnter2D(Collider2D Player)
    {

        if (Player.tag == "Player")
        {
            player.gold += 5; 
            Debug.Log("yee");
        }
    }
}
