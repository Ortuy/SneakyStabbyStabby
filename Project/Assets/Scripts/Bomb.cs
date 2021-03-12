﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class Bomb : MonoBehaviourPunCallbacks
{
    public Rigidbody2D rb;
    public GameObject rotatingItem;
    public Transform gasExitPos;
    public GameObject gasObject;
    [PunRPC]
    public void DestroyObject()
    {
        Destroy(this.gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        PhotonView target = collision.gameObject.GetComponent<PhotonView>();
        if (target != null)
        {
            if (target.tag == "Player")
            {
                GameObject obj = PhotonNetwork.Instantiate(gasObject.name, new Vector2(gasExitPos.transform.position.x, gasExitPos.transform.position.y), rotatingItem.transform.rotation, 0);
            }

            this.GetComponent<PhotonView>().RPC("DestroyObject", RpcTarget.AllBuffered);
        }
    }
}
