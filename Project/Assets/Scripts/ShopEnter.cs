using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class ShopEnter : MonoBehaviourPunCallbacks
{
    public Player player;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        PhotonView target = collision.gameObject.GetComponent<PhotonView>();
        if (collision.tag == "Player")
        {
            player = collision.GetComponent<Player>();
            player.Shop.SetActive(true);
        }

    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            player = collision.GetComponent<Player>();
            player.Shop.SetActive(false);
        }

    }


}
