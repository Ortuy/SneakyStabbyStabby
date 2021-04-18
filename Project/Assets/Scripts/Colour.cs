using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Colour : MonoBehaviourPunCallbacks
{
    public Player player;
    public Color colorToSet;

    public void OnTriggerEnter2D(Collider2D Player)
    {

        if (Player.tag == "Player")
        {
            var temp = Player.GetComponent<PhotonView>();
            Debug.Log("yee");
            //player.colourChange = true;
            //player.R = 108;
            //player.G = 30;
            //player.B = 37;
            temp.RPC("SetColor", RpcTarget.AllBuffered, colorToSet.r, colorToSet.g, colorToSet.b);
            
        }
    }
}
