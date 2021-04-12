using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class WaitRoomPortal : MonoBehaviourPunCallbacks
{
    public GameObject StartPortal;
    public GameObject Player;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Teleport();
        }
    }


    public void Teleport()
    {

        Player.transform.position = new Vector2(StartPortal.transform.position.x, StartPortal.transform.position.y);
    }
}
