using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Colour : MonoBehaviourPunCallbacks
{
    public Player player;
    public Color colorToSet;

    public void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Player")
        {
            var temp = collision.GetComponent<PhotonView>();
            player.ColorSelect.SetActive(true);
            temp.RPC("SetColor", RpcTarget.AllBuffered, colorToSet.r, colorToSet.g, colorToSet.b);
            
        }
    }
}
