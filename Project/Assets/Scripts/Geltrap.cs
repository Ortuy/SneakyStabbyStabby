﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Geltrap : MonoBehaviourPunCallbacks
{
    public Rigidbody2D rb;
    public bool shine = true;

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
                target.RPC("Shine", RpcTarget.AllBuffered, shine);


            }

            this.GetComponent<PhotonView>().RPC("DestroyObject", RpcTarget.AllBuffered);
        }
    }
}
