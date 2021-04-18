using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Gold : MonoBehaviourPunCallbacks
{
    public Player player;
    public int goldAmount;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        PhotonView target = collision.gameObject.GetComponent<PhotonView>();


       if (collision.tag == "Player")
       {
          player = collision.GetComponent<Player>();
          player.gold += goldAmount;
          Debug.Log("yee");

       }


        Destroy(this);
    }
}
