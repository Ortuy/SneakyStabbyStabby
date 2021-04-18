using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Colour : MonoBehaviourPunCallbacks
{
    public Player player;


    public void OnTriggerEnter2D(Collider2D Player)
    {

        if (Player.tag == "Player")
        {
            player = Player.GetComponent<Player>();
            Debug.Log("yee");
            //player.colourChange = true;
            //player.R = 108;
            //player.G = 30;
            //player.B = 37;
            player.SetColor(190, 28, 157);
            
        }
    }
}
